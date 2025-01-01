using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    private const string ExceptionHandlerSceneName = "ExceptionHandler";
    private const string MainSceneName = "MainScene";
    private const string Camera = "Camera";
    private const string Boostrap = "Bootstrap";

    private void Start()
    {
        if (!IsSceneLoaded(ExceptionHandlerSceneName))
        {
            SceneManager.LoadScene(ExceptionHandlerSceneName, LoadSceneMode.Additive);
        }
        
        if (!IsSceneLoaded(Camera))
        {
            SceneManager.LoadScene(Camera, LoadSceneMode.Additive);
        }
        
        ReloadMainScene();
    }

    private bool IsSceneLoaded(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    private void ReloadMainScene()
    {
        Scene bootstrapScene = SceneManager.GetSceneByName(Boostrap);
        if (IsSceneLoaded(MainSceneName))
        {
            SceneManager.UnloadSceneAsync(MainSceneName).completed += (operation) =>
            {
                SceneManager.LoadScene(MainSceneName, LoadSceneMode.Additive);
                SceneManager.UnloadSceneAsync(bootstrapScene);
            };
        }
        else
        {
            SceneManager.LoadScene(MainSceneName, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(bootstrapScene);
        }
    }
}
