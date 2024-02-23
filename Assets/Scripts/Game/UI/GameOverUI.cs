using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button recordButton;
    [SerializeField] private GameObject nameInput;
    private Player player;
    private bool isSaved;

    public void Show(Player player)
    {
        this.player = player;
        gameObject.SetActive(true);

        if (RecordsStorage.Singletone.CheckNewRecord(player.GetScore()))
            ShowRecordInput();
    }

    public void ShowRecordInput()
    {
        nameInput.SetActive(true);
    }

    public void SavePlayerRecord()
    {
        if (isSaved)
            return;

        PlayerData player = new PlayerData();
        player.name = nameInputField.text;
        player.score = this.player.GetScore();
        RecordsStorage.Singletone.AddPlayerData(player);

        isSaved = true;
        recordButton.interactable = false;
    }

}
