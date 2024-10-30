using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderControllerNext : MonoBehaviour
{
    private void Awake()
    {
        InitiateSceneLoad(ScenePathsAudioParameters.MainMenuStage);
    }

    private void InitiateSceneLoad(string targetScene)
    {
        StartCoroutine(ExecuteSceneLoad(targetScene));
    }

    private IEnumerator ExecuteSceneLoad(string sceneName)
    {
        var loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        loadingOperation.allowSceneActivation = false;

        while (!loadingOperation.isDone)
        {
            if (loadingOperation.progress >= 0.9f)
            {
                loadingOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}