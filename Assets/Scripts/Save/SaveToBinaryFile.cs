using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveToBinaryFile
{
    private string filePath;

    public SaveToBinaryFile()
    {
        filePath = Application.persistentDataPath + "/SaveData.dat";
    }

    //Сохранение данных для рекордов
    public void Save(SaveData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filePath);

        bf.Serialize(file, data);
        file.Close();
    }

    //Загрузка данных для рекордов
    public SaveData Load()
    {
        //Если файла ещё нету то тогда создает начальный файл
        if (!File.Exists(filePath))
        {
            return null;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(filePath, FileMode.Open);

        SaveData data = (SaveData)bf.Deserialize(file);

        file.Close();

        return data;
    }
}