using UnityEngine;
using UnityEngine.SceneManagement;
public class MapSelector : MonoBehaviour
{
    public Sprite backgroundImage;
    public string sceneName;
    public void OpenScene()
    {
        FindObjectOfType<ChangeBackground>().ChangeImageBackground(backgroundImage);
        FindObjectOfType<ConfirmScene>().SceneName = sceneName;
        //SceneManager.LoadScene(sceneName);
    }
}
