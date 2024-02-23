using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Singletone;
    public SaveData saveData { private set; get; }
    private SaveToBinaryFile saveToBinaryFile;

    public void Init()
    {
        Singletone = this;
        saveToBinaryFile = new SaveToBinaryFile();
        Load();

        DontDestroyOnLoad(gameObject);
    }

    public void Save()
    {
        saveToBinaryFile.Save(saveData);
    }

    public void Load()
    {
        saveData = saveToBinaryFile.Load();
        if (saveData == null)
            saveData = new SaveData();
    }
}