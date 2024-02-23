using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private float musicVolume = 0.2f;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private float gameSoundVolume = 0.8f;
    [SerializeField] private Slider gameSoundSlider;

    void Awake()
    {
        gameSoundSlider.value = SaveManager.Singletone.saveData.gameSoundVolume;
        musicSlider.value = SaveManager.Singletone.saveData.musicVolume;
    }

    public void ChangeMusicVolume()
    {
        musicVolume = musicSlider.value;
        AudioManager.Singletone.musicAudioSource.volume = musicVolume;
    }

    public void ChangeGameSoundVolume()
    {
        gameSoundVolume = gameSoundSlider.value;
        AudioManager.Singletone.gameSoundsAudioSource.volume = gameSoundVolume;
    }

    public void SaveAndClose()
    {
        SaveManager.Singletone.saveData.gameSoundVolume = gameSoundVolume;
        SaveManager.Singletone.saveData.musicVolume = musicVolume;
        SaveManager.Singletone.Save();

        gameObject.SetActive(false);
    }
}
