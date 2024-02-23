using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    [SerializeField] private Field field;
    [SerializeField] private Game game;
    [SerializeField] private UIScore uiScore;

    public void Awake()
    {
        field.Init();
        game.Init();
        uiScore.Init(game.player);

        game.StartGame();
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
