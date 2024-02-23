using System.Collections.Generic;
using UnityEngine;

public class RecordsStorage : MonoBehaviour
{
    public static RecordsStorage Singletone;
    public List<PlayerData> playersData;
    public float gameSoundVolume;
    public float musicVolume;
    private int maxRecords = 3;

    public void Init()
    {
        Singletone = this;
        SaveData data = SaveManager.Singletone.saveData;
        if(data == null || data.playersData == null)
            playersData = firstSavePlayersData();
        else
            playersData = data.playersData;

        SortRecords();
        DontDestroyOnLoad(gameObject);
    }

    private List<PlayerData> firstSavePlayersData()
    {
        List<PlayerData> playersData = new List<PlayerData>();

        PlayerData player1 = new PlayerData();
        player1.name = "Pro";
        player1.score = 30;

        PlayerData player2 = new PlayerData();
        player2.name = "Bob 2";
        player2.score = 20;

        PlayerData player3 = new PlayerData();
        player3.name = "bob";
        player3.score = 10;

        playersData.Add(player1);
        playersData.Add(player2);
        playersData.Add(player3);

        return playersData;
    }

    public void AddPlayerData(PlayerData playerData)
    {
        playersData.Add(playerData);
        SortRecords();

        SaveManager.Singletone.saveData.playersData = playersData;
        SaveManager.Singletone.Save();
    }

    public bool CheckNewRecord(int score)
    {
        if (playersData.Count < maxRecords)
            return true;

        if (playersData[maxRecords-1].score < score)
        {
            return true;
        }

        return false;
    }

    private void SortRecords()
    {
        playersData.Sort(SortByScore);

        while (playersData.Count > maxRecords)
        {
            playersData.RemoveAt(playersData.Count - 1);
        }
    }

    private int SortByScore(PlayerData playerDataA, PlayerData playerDataB)
    {
        return playerDataB.score.CompareTo(playerDataA.score);
    }
}