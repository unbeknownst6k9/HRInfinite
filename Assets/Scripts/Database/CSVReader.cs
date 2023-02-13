using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * purpose: this is a helper class to read CSV file and return data as an array
 */
namespace DatabaseHelper{
    public class CSVReader: MonoBehaviour
    {
        public static string[] getFileContent(TextAsset file)
        {
            string[] data = file.text.Split(new char[] { '\n' });
            return data;
        }

        /*
         * TODO: create read CSV method with file path input. create read methods that return:
         * first name list
         * last name list
         * skill list based on given enum skillArea
         * university name list
         */
    }
}
