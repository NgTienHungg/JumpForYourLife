using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class Audio
{
    public string name;
    public AudioClip clip;
    public bool loop;

    [Range(0f, 1f)]
    public float volume = 1f;

    [HideInInspector]
    public AudioSource source;
}

public class AudioManager : MonoBehaviour
{
    #region SINGLETON
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

    public Audio[] musics;
    public Audio[] sounds;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("OnMusic"))
            PlayerPrefs.SetInt("OnMusic", 1);

        if (!PlayerPrefs.HasKey("OnSound"))
            PlayerPrefs.SetInt("OnSound", 1);

        foreach (Audio audio in musics)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;
            audio.source.volume = audio.volume;
            audio.source.loop = audio.loop;
        }

        foreach (Audio audio in sounds)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;
            audio.source.volume = audio.volume;
            audio.source.loop = audio.loop;
        }
    }

    public void PlayMusic(string name)
    {
        if (PlayerPrefs.GetInt("OnMusic") == 0)
            return;

        Audio audio = Array.Find(musics, music => music.name == name);
        if (audio == null)
        {
            Debug.LogWarning("Can't find music with name: " + name);
            return;
        }

        audio.source.Play();
    }

    public void PauseMusic(string name)
    {
        Audio audio = Array.Find(musics, music => music.name == name);
        if (audio == null)
        {
            Debug.LogWarning("Can't find sound with name: " + name);
            return;
        }

        if (audio.source.isPlaying)
            audio.source.Pause();
    }

    public void StopMusic(string name)
    {
        Audio audio = Array.Find(musics, music => music.name == name);
        if (audio == null)
        {
            Debug.LogWarning("Can't find sound with name: " + name);
            return;
        }

        audio.source.Stop();
    }

    public void PauseAllMusic()
    {
        foreach (var music in musics)
            PauseMusic(music.name);
    }

    public void ContinuePlayMusic()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
            PlayMusic("Background");
        else if (SceneManager.GetActiveScene().name == "GamePlay")
            PlayMusic("Gameplay");
    }

    public void PlaySound(string name)
    {
        if (PlayerPrefs.GetInt("OnSound") == 0)
            return;

        Audio audio = Array.Find(sounds, sound => sound.name == name);
        if (audio == null)
        {
            Debug.LogWarning("Can't find sound with name: " + name);
            return;
        }
        audio.source.Play();
    }
}