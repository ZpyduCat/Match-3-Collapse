using UnityEngine;
using UnityEngine.SceneManagement;

public class BootScene : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private RecordsStorage recordsStorage;
    [SerializeField] private SaveManager saveManager;

    private void Awake()
    {
        saveManager.Init();
        audioManager.Init();
        recordsStorage.Init();

        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
