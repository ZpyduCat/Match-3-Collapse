using UnityEngine;
using TMPro;

public class UIScore : MonoBehaviour
{
    [SerializeField] private TMP_Text healtText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text tapPriceText;
    [SerializeField] private TMP_Text turnText;
    private Player player;

    public void Init(Player player)
    {
        this.player = player;
        this.player.OnHealtChangeEvent += OnHealtChange;
        this.player.OnScoreChangeEvent += OnScoreChange;
        this.player.OnTurnChangeEvent += OnTurnChange;

        OnHealtChange();
        OnScoreChange();
        OnTurnChange();
    }

    private void OnHealtChange()
    {
        healtText.text = $"Жизни: {player.GetHealt()}";
    }

    private void OnScoreChange()
    {
        scoreText.text = $"Очки игрока: {player.GetScore()}";
    }

    private void OnTurnChange()
    {
        tapPriceText.text = $"Стоимость хода: {player.GetTapPrice()}";
        turnText.text = $"Ход: {player.GetTurn()}";
    }

}
