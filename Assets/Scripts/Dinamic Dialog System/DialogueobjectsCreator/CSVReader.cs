using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.VisualBasic;
using System.IO;
using System;
using UnityEditor;

public static class CSVReader 
{
    // read data and return it as a list of strings
    public static List<string> ReadData (string _objectPath)
    {
        // 1 Read data form the csv file and save it as a string
        // 2 parse the string and extract the data of each ROW getting this way the id  and the diferent lenguages (externaly)

        // file acces violation
        StreamReader streamReader = new StreamReader(_objectPath);
        List<string> streamLines = new List<string>();
        streamLines = SeparateIntoLInes(streamReader);

        return streamLines;
    }
    public static List<string> SeparateIntoLInes(StreamReader _streamReader)
    {
        List<string> returnedList = new List<string>();

        // iteration of the object returned line by line
        while (_streamReader.Peek() >= 0)
        {
            returnedList.Add(_streamReader.ReadLine());
        }

        return returnedList;
    }
    


    public static void WritteData()
    {
        throw new NotImplementedException();

    }

}
