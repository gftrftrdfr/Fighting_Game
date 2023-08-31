using UnityEngine;
using UnityEngine.SceneManagement;
public class ConfirmScene : MonoBehaviour
{
    private string sceneName;
    public string SceneName
    {
        set
        {
            this.sceneName = value;
        }  
    }
    public void ChangeScene()
    {
        if(string.IsNullOrEmpty(sceneName))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex + 1);
            return;
        }
        PlayerPrefs.SetString("sceneName",sceneName);
        SceneManager.LoadScene("CharacterSelection");
    }
}
