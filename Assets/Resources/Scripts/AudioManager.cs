using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;

    // Drag in the .mp3 files here, in the editor
    public AudioClip[] BGM;

    public AudioSource BGMSource, SFXSource;

    // Singelton to keep instance alive through all scenes
    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);

        // Hooks up the 'OnSceneLoaded' method to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Called whenever a scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        // Plays different music in different scenes
        switch (scene.name)
        {
            case "Menu":
                System.Random rnd = new System.Random();
                BGMSource.enabled = false;
                BGMSource.clip = BGM[rnd.Next(1,5)];
                BGMSource.enabled = true;
                break;
            case "SelectMap":
                BGMSource.enabled = false;
                BGMSource.clip = BGM[7];
                BGMSource.enabled = true;
                break;
            case "CharacterSelection":
                BGMSource.enabled = false;
                BGMSource.clip = BGM[8];
                BGMSource.enabled = true;
                break;
            case "Halloween":
                BGMSource.enabled = false;
                BGMSource.clip = BGM[9];
                BGMSource.enabled = true;
                break;
            case "Fairy":
                BGMSource.enabled = false;
                BGMSource.clip = BGM[10];
                BGMSource.enabled = true;
                break;
            case "Desert":
                BGMSource.enabled = false;
                BGMSource.clip = BGM[11];
                BGMSource.enabled = true;
                break;
            default:
                BGMSource.enabled = false;
                BGMSource.clip = BGM[0];
                BGMSource.enabled = true;
                break;
        }
    }
    // Start is called before the first frame update

    public void PlaySFX(string champName,string soundName)
    {
        AudioClip sfx = Resources.Load<AudioClip>($"{champName}/SFX/{soundName}");
        if (sfx == null)
        {

        }
        else
        {
            Debug.Log($"{champName}/SFX/{soundName}");
            //SFXSource.PlayOneShot(sfx);
        }

    }
}
