using UnityEngine;
using UnityEngine.SceneManagement;
public class MapSelector : MonoBehaviour
{
    public string sceneName;
    public void OpenScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
