using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DataStruct;
using DatabaseHelper;

/*Purpose: This class generates objective for each round
 */
public class ObjectiveGenerator
{
    private Dataset data;
    private int difficulty;

    public ObjectiveGenerator(Dataset data, int difficulty)
    {
        this.data = data;
        this.difficulty = difficulty;
    }

    public Objective generateObjective()
    {
        var newObjective = new Objective();
        newObjective.BLUniversity = createUniversityBL();
        newObjective.skillRequirement = getSkillAreas();
        newObjective.peopleToHire = peopleToHire();
        newObjective.peopleHired = 0;
        newObjective =  getAge(newObjective);
        return newObjective;
    }

    private string[] createUniversityBL()
    {
        List<string> universities = CSVReader.getFileContent(data.UniversityFile).ToList<string>();
        int BLNumber = Random.Range(0, 3) + difficulty;
        var tempBL = new string[BLNumber];
        int tempItem = 0;
        for(int i = 0; i < BLNumber; i++)
        {
            tempItem = Random.Range(0, universities.Count - 1);
            tempBL[i] = universities[tempItem];
            universities.RemoveAt(tempItem);

        }
        return tempBL;
    }

    private skillArea[] getSkillAreas()
    {
        int numberOfField = Random.Range(1, 2);
        switch (numberOfField)
        {
            case 2:
                skillArea[] skillAreas = new skillArea[] { (skillArea)Random.Range(0,3), (skillArea)Random.Range(0, 3) };
                while(skillAreas[0] == skillAreas[1])
                {//if there are two same area then re-select another area
                    skillAreas[1] = (skillArea)Random.Range(0, 3);
                }
                return skillAreas;
            default:
                //if there's only 1 skill area then return a skillArea array with 1 random area
                return new skillArea[] { (skillArea)Random.Range(0, 3) };
        }
    }

    /*This methods set the number of people that needed to be hired
     */
    private int peopleToHire()
    {
        return Random.Range(3 + (difficulty * 2), 9 + (difficulty * 2));
    }

    /*This method sets the max min age for objective
     */
    private Objective getAge(Objective objective)
    {
        int maxAge = Random.Range(30, 60);
        int minAge = Random.Range(18, 30);
        maxAge = (maxAge - minAge < Mathf.RoundToInt((float)(difficulty * 1.5))) ? maxAge + (9-difficulty) : maxAge;
        objective.ageMax = maxAge;
        objective.ageMin = minAge;
        return objective;
    }

}
