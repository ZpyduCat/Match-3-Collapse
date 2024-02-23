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
        healtText.text = $"�����: {player.GetHealt()}";
    }

    private void OnScoreChange()
    {
        scoreText.text = $"���� ������: {player.GetScore()}";
    }

    private void OnTurnChange()
    {
        tapPriceText.text = $"��������� ����: {player.GetTapPrice()}";
        turnText.text = $"���: {player.GetTurn()}";
    }

}
