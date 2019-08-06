using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.VisualBasic;
using System.IO;



public static class CSVReader 
{

    // read data and return it as a list of strings
    public static List<string> ReadData (string _objectPath)
    {
        // 1 Read data form the csv file and save it as a string
        // 2 parse the string and extract the data of each ROW getting this way the id  and the diferent lenguages

        // file acces violation
        List<string> returnedList = new List<string>();
        StreamReader streamReader = new StreamReader(_objectPath);

        // iteration of the object returned line by line
        while (streamReader.Peek() >= 0)
        {
            returnedList.Add(streamReader.ReadLine());
        }

        return returnedList;

    }




}
