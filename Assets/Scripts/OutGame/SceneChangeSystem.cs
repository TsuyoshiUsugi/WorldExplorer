using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeSystem : MonoBehaviour
{

    /// <summary>
    /// Sceneを遷移する
    /// </summary>
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
