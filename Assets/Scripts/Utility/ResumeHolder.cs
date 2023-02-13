using System;
using System.Collections.Generic;
using System.Threading;
using DataStruct;

/*
 * Purpose: This is a helper class to help handle the resume generate function
 */
public class ResumeHolder
{
    private ResumeGenerator resumeGenerator;
    private Stack<Resume> resumeStack;

    public ResumeHolder(Dataset data, int Difficulty)
    {
        resumeGenerator = new ResumeGenerator(data, Difficulty);
        resumeStack = new Stack<Resume>();
    }

    /*purpose: this method is an experiment for multi-threading in C#
     * 
     */
    public void generateResume(int amount)
    {
        Thread generator1 = new Thread(() =>
        {for(int i = 0; i < (int)Math.Round((double)amount/2) - 1; i++)
            {
                resumeStack.Push(resumeGenerator.generateResume());
                //Debug.Log("generator 1 at " + i);
            }
        });
        Thread generator2 = new Thread(() =>
        {
            for (int i = 0; i < (int)Math.Round((double)amount/2) + 1; i++)
            {
                resumeStack.Push(resumeGenerator.generateResume());
                //Debug.Log("generator 2 at " + i);
            }
        });
            generator1.Start();
            generator2.Start();
    }

    public Resume getNewResume()
    {
        if (resumeStack.Count > 0)
        {
            return resumeStack.Pop();
        }
        return new Resume();
    }

    public int getResumeLength()
    {
        return resumeStack.Count;
    }

    public static string calculateResumeScore (Resume resume, Objective currentObjective, string message)
    {
        if(resume.realAge < currentObjective.ageMin || resume.realAge > currentObjective.ageMax)
        {
            resume.score -= 0.4f;
        }
        for(int i = 0; i < resume.Educations.Length; i++)
        {
            if (Array.Exists<string>(currentObjective.BLUniversity, university => university == resume.Educations[i].name))
            {
                message += "Has University Black List";
                resume.score -= 0.2f;
            }
        }
        for(int i = 0;i < resume.Skills.Length; i++)
        {
            if(Array.Exists<skillArea>(currentObjective.skillRequirement, skill => skill == resume.Skills[i].skillArea))
            {
                message += "Has unmatch Skill";
                resume.score -= 0.1f;
            }
        }
        return message;
    }
}
