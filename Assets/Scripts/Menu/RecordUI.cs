using UnityEngine;
using TMPro;

public class RecordUI : MonoBehaviour
{
    [SerializeField] private TMP_Text[] playerTexts;
    private void Awake()
    {
        RecordsStorage storage = RecordsStorage.Singletone;

        for (int i = 0; i < playerTexts.Length; i++)
        {
            if(i < storage.playersData.Count)
            {
                playerTexts[i].text = $"{i+1}. {storage.playersData[i].name} - {storage.playersData[i].score}";
            }
        }
    }
}
