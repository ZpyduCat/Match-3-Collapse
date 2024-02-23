using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Singletone;
    public AudioSource musicAudioSource;
    public AudioSource gameSoundsAudioSource;
    public void Init()
    {
        Singletone = this;

        musicAudioSource.volume = SaveManager.Singletone.saveData.musicVolume;
        gameSoundsAudioSource.volume = SaveManager.Singletone.saveData.gameSoundVolume;

        DontDestroyOnLoad(gameObject);
    }
}
