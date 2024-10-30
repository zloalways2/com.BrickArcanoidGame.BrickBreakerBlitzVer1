using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionController : MonoBehaviour
{
    public void LoadTransitionScene()
    {
        LoadSceneByName(ScenePathsAudioParameters.TransitionScene);
    }

    public void LoadGameplayScene()
    {
        LoadSceneByName(ScenePathsAudioParameters.GameplayScene);
    }

    private void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}