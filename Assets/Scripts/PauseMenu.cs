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
        Time.timeScale = 1;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(scene.buildIndex, LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void Back()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        Time.timeScale = 1;
    }

}
