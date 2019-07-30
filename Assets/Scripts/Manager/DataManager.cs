using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
[CreateAssetMenu(fileName = "DataManagerSO", menuName = "DataManager")]
public class DataManager : ScriptableObject
{

    private string nameRute = Application.persistentDataPath + "/datos.dat";
    private List<int> globalKarma = new List<int>();
    private List<int> localKarma = new List<int>();


    public void Quit()
    {
        SaveDates();
        Application.Quit();
    }

    public void Load()
    {
        LoadDates();
    }

    public void Save()
    {
        SaveDates();
    }

    public void AddToList(int _num)
    {
        globalKarma.Add(_num);
        localKarma.Add(_num);
    }

    public void NextLevel()
    {
        localKarma.Clear();
    }

    public void ResetData()
    {
        //Reiniciar cosas
        Save();
    }

    public List<int> GetGlobalKarma()
    {
        return globalKarma;
    }

    public List<int> GetLocalKarma()
    {
        return localKarma;
    }

    private void SaveDates()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(nameRute);

        DatesToSave datos = new DatesToSave();

        datos.globalKarma = globalKarma; //cambiar datos para guardar

        bf.Serialize(file, datos);
        file.Close();
    }

    private void LoadDates()
    {
        if (File.Exists(nameRute))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(nameRute, FileMode.Open);

            DatesToSave datos = (DatesToSave)bf.Deserialize(file);
            globalKarma = datos.globalKarma; //Coger Datos

            file.Close();
        }
        else
        {
            ResetData();
        }
    }
}

class DatesToSave
{
    public List<int> globalKarma = new List<int>();
}

