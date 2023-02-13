using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStruct;
using DatabaseHelper;
using CustomRandom;

/*
 * purpose: this class is to generate resume object and return it back to resume holder
 */
public class ResumeGenerator : MonoBehaviour
{
    #region Fields
    private Dataset dataFiles;
    private string[] firstNameList;
    private string[] lastNameList;
    private Dictionary<skillArea, string[]> skillList;
    private string[] universityList;
    //difficulty will be added to the game later on
    private int difficulty;
    private int emailNumber;
    //Base score
    private float intialScore = 0f;
    private float recordScore = 0f;
    #endregion


    /*
     * purose: constructor of ResumeGenerator class. 
     * The class will initialize all data before generateResume is called to avoid uneccessary memory usage and loading time from reloading the data from datasets
     * 
     */
    public ResumeGenerator(Dataset dataFiles, int difficulty)
    {
        if(dataFiles != null)
        {
            this.dataFiles = dataFiles;
            this.firstNameList = CSVReader.getFileContent(dataFiles.fistNameFile);
            this.lastNameList = CSVReader.getFileContent(dataFiles.LastNameFile);
            this.skillList = JsonReader.getSkills(dataFiles.SkillFile);
            this.universityList = CSVReader.getFileContent(dataFiles.UniversityFile);
            this.difficulty = difficulty;
        }
        else
        {
            Debug.LogError("Class: ResusmeGenerator, Datafile not found");
        }
        
    }

    public Resume generateResume()
    {
        Resume resume = new Resume();
        //resume.score = score;//clear score
        recordScore = intialScore;
        //resume.score = intialScore;
        resume.firstName = firstNameList[StaticRandomObject.Instance.getRandomInt(0, firstNameList.Length - 1)];
        resume.realFirstName = getRealName(resume.firstName, firstNameList);
        resume.lastName = lastNameList[StaticRandomObject.Instance.getRandomInt(0, lastNameList.Length - 1)];
        resume.realLastName = getRealName(resume.lastName, lastNameList);
        resume.age = StaticRandomObject.Instance.getRandomInt(15, 70);
        resume.realAge = getRealAge(resume.age);
        resume.email = getEmail(resume.firstName, resume.lastName);
        resume.realEmail = getRealEmail(resume.firstName, firstNameList, lastNameList, resume.lastName, resume.email);
        resume.Educations = getEducation(universityList, resume);
        resume.Skills = getSkills(skillList, resume);
        resume.score = recordScore;
        //Debug.Log("Resume score is: "+resume.score);
        return resume;
    }

    private string getRealName(string name, string[] nameList)
    {
        int pickName = StaticRandomObject.Instance.getRandomInt(0, 100);
        updateScore(0.1f);
        if(pickName < (difficulty * 10))
        {
            updateScore(-0.5f);
            string tempName = name;
            while(tempName == name)
            {
                tempName =  nameList[StaticRandomObject.Instance.getRandomInt(0, nameList.Length - 1)];
            }
            return tempName;
        }
        return name;
    }

    private int getRealAge(int age)
    {
        var pickAge = StaticRandomObject.Instance.getRandomInt(0, 100);
        updateScore(0.1f);
        if (pickAge < (difficulty * 10))
        {
            updateScore(-0.5f);
            return age - difficulty;
        }
        return age;
    }

    private string getEmail(string firstName, string lastName)
    {
        emailNumber = StaticRandomObject.Instance.getRandomInt(20, 2000);
        return firstName+lastName[0]+ emailNumber.ToString()+"@email.com";
    }

    private string getRealEmail(string firstName, string[] firstNameList, string[] lastNameList,string lastName, string email)
    {
        var pickEmail = StaticRandomObject.Instance.getRandomInt(0, 100);
        updateScore(0.2f);
        string newEmail = "";
        if (pickEmail < (difficulty * 10))
        {
            updateScore(-0.6f);
            pickEmail = StaticRandomObject.Instance.getRandomInt(20, 100);
            //the reason to seperate them is to have different multiplier for each part of the email
            //pick first name
            newEmail += (pickEmail > StaticRandomObject.Instance.getRandomInt(0, 70 - (difficulty * 10)) ? firstNameList[StaticRandomObject.Instance.getRandomInt(0, firstNameList.Length - 1)] : firstName);
            //pick last name
            newEmail += (pickEmail > StaticRandomObject.Instance.getRandomInt(0, 70 - (difficulty * 10)) ? lastNameList[StaticRandomObject.Instance.getRandomInt(0, lastNameList.Length - 1)][0] : lastName);
            //pick number
            newEmail += (pickEmail > StaticRandomObject.Instance.getRandomInt(0, 70 - (difficulty * 7)) ? StaticRandomObject.Instance.getRandomInt(20, 2000) : emailNumber);
            return newEmail + "@email.com";
        }
        return email;
    }

    /*
     * Purpose: Create education object. 
     * *Ignore education year for now
     */

    private Education[] getEducation(string[] universities, Resume resume)
    {
        Stack<Education> educations = new Stack<Education>();
        //get the nax number for how many education
        var limit = (resume.age<20)? 1: (int)Math.Round((double)(resume.age / 20));
        //draw the max number so that the number of eduation is not fixed by age
        limit = StaticRandomObject.Instance.getRandomInt(1, limit);
        
        for (int i = 0; i < limit; i++)
        {
            var edu = new Education();
            edu.name = universities[StaticRandomObject.Instance.getRandomInt(0, universities.Length - 1)];
            /*if(Array.Exists(objective.BLUniversity, delegate(string s) { return s == edu.name; }))
            {
                //Debug.Log("University "+edu.name+" is in Black List "+objective.BLUniversity.ToString());
                //updateScore(-0.2f);
            }*/
            educations.Push(edu);
        }
        return educations.ToArray();
    }

    private Skill[] getSkills(Dictionary<skillArea, string[]> skillList, Resume resume)
    {
        /* 1.get a UnityEngine.Random number of skill
         * 2. get a UnityEngine.Random field
         * 3. get a UnityEngine.Random skill in the field
         */
        Stack<Skill> skills = new Stack<Skill>();
        var skillsLimit = (resume.age < 20) ? 1 : (int)Math.Round((double)(resume.age / 20));
        skillsLimit = StaticRandomObject.Instance.getRandomInt(1, (skillsLimit+difficulty));
        for(int i = 0; i < skillsLimit;i++)
        {
            var newSkill = new Skill();
            //select area
            newSkill.skillArea = (skillArea)StaticRandomObject.Instance.getRandomInt(0, Enum.GetNames(typeof(skillArea)).Length - 1);
            /*if (!Array.Exists(objective.skillRequirement, delegate(skillArea a) { return a == newSkill.skillArea; }))
            {
                //Debug.Log("Skill "+newSkill.skillArea+" is not the list "+objective.skillRequirement.ToString());
                //updateScore(-0.1f);
            }*/
            //get skill name
            newSkill.skillName = skillList[newSkill.skillArea][StaticRandomObject.Instance.getRandomInt(0, skillList[newSkill.skillArea].Length - 1)];
            //get year
            newSkill.years = StaticRandomObject.Instance.getRandomInt(1, 8 + (int)Math.Round((double)(resume.age / 20)));
            skills.Push(newSkill);
        }
        return skills.ToArray();
    }

    private void updateScore(float s)
    {
        recordScore += s;
    }
}
