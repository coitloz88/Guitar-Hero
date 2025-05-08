using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    private static BackgroundMusicManager instance;
    private AudioSource audioSource;

    public AudioClip mainMenuBGM; // MainMenuScene과 LevelSelectScene의 배경 음악
    public AudioClip gameplayBGM; // GamePlayScene 전용 배경 음악
    public AudioClip resultBGM; // ResultScene 전용 배경 음악

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static BackgroundMusicManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("BackgroundMusicManager is not initialized!");
            }
            return instance;
        }
    }

    public void PlayMainMenuBGM()
    {
        if (audioSource.clip != mainMenuBGM || !audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.clip = mainMenuBGM;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlayGameplayBGM()
    {
        audioSource.Stop();
        audioSource.clip = gameplayBGM;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayResultBGM()
    {
        audioSource.Stop();
        audioSource.clip = resultBGM;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public float GetVolume()
    {
        return audioSource.volume;
    }
}
