using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    Scene scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene.buildIndex, LoadSceneMode.Single);
    }

    public void Back()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

}
