using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * purpose: This class helps
 */
[CreateAssetMenu(fileName ="New DataSet", menuName ="createNewDataSet")]
public class Dataset : ScriptableObject
{
    public TextAsset fistNameFile;
    public TextAsset LastNameFile;
    public TextAsset SkillFile;
    public TextAsset UniversityFile;
}
