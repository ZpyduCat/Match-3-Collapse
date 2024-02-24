using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_Text nameInputPlacholderText;
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

        if(string.IsNullOrEmpty(nameInputField.text))
        {
            nameInputPlacholderText.color = Color.red;
            nameInputPlacholderText.text = "Введите имя здесь";

            return;
        }
        else if(nameInputField.text.Length >= 20)
        {
            nameInputField.text = string.Empty;
            nameInputPlacholderText.color = Color.red;
            nameInputPlacholderText.text = "Имя не может быть больше 20 букв";

            return;
        }

        PlayerData player = new PlayerData();
        player.name = nameInputField.text;
        player.score = this.player.GetScore();
        RecordsStorage.Singletone.AddPlayerData(player);

        isSaved = true;
        recordButton.interactable = false;
        nameInputField.interactable = false;
        nameInputField.text = "Записано";
    }

}
