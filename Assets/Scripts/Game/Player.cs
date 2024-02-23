using System;

public class Player
{
    public event Action OnScoreChangeEvent;
    public event Action OnHealtChangeEvent;
    public event Action OnTurnChangeEvent;

    private int score;
    private int healt;
    private int tapPrice;
    private int turn;

    public Player(int healt = 10, int tapPrice = 3)
    {
        this.healt = healt;
        this.tapPrice = tapPrice;
        this.score = 0;
        this.turn = 0;
    }

    public void AddScore(int addScore)
    {
        score += addScore;
        OnScoreChangeEvent?.Invoke();
    }

    public void AddHealt(int addHealt)
    {
        healt += addHealt;
        OnHealtChangeEvent?.Invoke();
    }

    public void RemoveHealt(int removeHealt)
    {
        healt -= removeHealt;
        if (healt < 0)
            healt = 0;
        OnHealtChangeEvent?.Invoke();
    }

    public void AddTurn(int addTurn = 1)
    {
        turn += addTurn;
        OnTurnChangeEvent?.Invoke();
    }

    public int GetTapPrice()
    {
        return tapPrice + turn;
    }

    public int GetHealt()
    {
        return healt;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetTurn()
    {
        return turn;
    }
}
