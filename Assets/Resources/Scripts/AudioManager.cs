using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    static AudioManager instance;

    public AudioSource BGMSource, SFXSource;

    // Singelton to keep instance alive through all scenes
    void Awake()
    {
        AudioManager[] audioManagers = FindObjectsOfType<AudioManager>();

        foreach (AudioManager audioManager in audioManagers)
        {
            if (audioManager != this)
            {
                Destroy(audioManager.gameObject);
            }
        }

        Instance = this;

        DontDestroyOnLoad(transform.root.gameObject);
    }

    // Called whenever a scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        // Plays different music in different scenes
        switch (scene.name)
        {
            case "Menu":
                System.Random rnd = new System.Random();
                PlayBGM("Menu" + rnd.Next(1, 5));
                break;
            default:
                PlayBGM(scene.name);
                break;
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void PlayBGM(string soundName)
    {
        AudioClip bgm = Resources.Load<AudioClip>($"Audio/BGM/{soundName}");
        if (bgm == null)
        {
            BGMSource.Stop();
        }
        else
        {
            BGMSource.loop = true;
            BGMSource.clip = bgm;
            BGMSource.Play();
        }

    }

    public void PlaySFX (string champName,string soundName)
    {
        AudioClip sfx = Resources.Load<AudioClip>($"Audio/SFX/Characters/{champName}/{soundName}");
        if (sfx == null)
        {

        }
        else
        {
            Debug.Log($"{champName}/SFX/{soundName}");
            //SFXSource.PlayOneShot(sfx);
        }

    }
    public void PlaySFX(string soundName)
    {
        AudioClip sfx = Resources.Load<AudioClip>($"Audio/SFX/UI/{soundName}");
        if (sfx == null)
        {

        }
        else
        {
            
            SFXSource.PlayOneShot(sfx);
        }

    }
}
