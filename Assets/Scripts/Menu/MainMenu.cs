using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settings;
    public GameObject leaders;

    void Awake()
    {
        CloseSettings();
        CloseLeaders();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void OpenLeaders()
    {
        leaders.SetActive(true);
    }
    public void CloseLeaders()
    {
        leaders.SetActive(false);
    }

    public void OpenSettings()
    {
        settings.SetActive(true);
    }

    public void CloseSettings()
    {
        settings.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Game closed");
        Application.Quit();
    }

}
