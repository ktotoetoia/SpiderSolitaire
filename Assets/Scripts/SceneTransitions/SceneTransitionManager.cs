using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneTransitionManager
{
    public static bool IsLoading { get; private set; }

    public static AsyncOperation LoadSceneAsync(string sceneName, ISceneTransition transition)
    {
        if (IsLoading)
        {
            return null;
        }

        IsLoading = true;

        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(sceneName);

        sceneLoading.allowSceneActivation = false;
        transition.Activate(() => sceneLoading.allowSceneActivation = true);

        sceneLoading.completed += (a) => IsLoading = false;

        return sceneLoading;
    }
}