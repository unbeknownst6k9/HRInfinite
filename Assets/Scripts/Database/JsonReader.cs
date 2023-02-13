using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStruct;

/*
 * Purpose: Database helper class to help read Json file
 */
namespace DatabaseHelper{
    public class JsonReader
    {
        public static Dictionary<skillArea, string[]> getSkills(TextAsset skillFile)
        {
            JsonSkill tempSkill = importJson<JsonSkill>(skillFile);
            Dictionary<skillArea, string[]> skillDictionary = new Dictionary<skillArea, string[]>();
            skillDictionary.Add(skillArea.Art, tempSkill.Art);
            skillDictionary.Add(skillArea.Tech, tempSkill.Tech);
            skillDictionary.Add(skillArea.Marketing, tempSkill.Marketing);
            skillDictionary.Add(skillArea.Management, tempSkill.Management);
            return skillDictionary;
        }


        /*
         * this is a helper function to import jsonfile through textasset object with different types of object
         */
        private static T importJson<T>(TextAsset jsonFile)
        {
            return JsonUtility.FromJson<T>(jsonFile.text);
        }

        /*
         * helper sub-class for reading JSON file
         */
        [System.Serializable]
        private struct JsonSkill
        {
            public string[] Art;
            public string[] Tech;
            public string[] Marketing;
            public string[] Management;
        }
    }
}