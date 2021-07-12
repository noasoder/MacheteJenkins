using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static class SceneLoader
{
    private class SceneLoaderMono : MonoBehaviour { };
    public static Action m_onLoaderCB;

    private static AsyncOperation m_loadingOperation;
    public static void LoadScene(string sceneName, bool useLoadingScene)
    {
        if (useLoadingScene)
        {
            m_onLoaderCB = () => {
                GameObject loader = new GameObject();
                loader.AddComponent<SceneLoaderMono>().StartCoroutine(LoadSceneASync(sceneName));

            };

            SceneManager.LoadScene("LoadingScene");
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    private static IEnumerator LoadSceneASync(string sceneName)
    {
        yield return null;
        m_loadingOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!m_loadingOperation.isDone)
        {
            yield return null;
        }
    }
    public static void LoaderCB()
    {
        m_onLoaderCB?.Invoke();
    }
    public static float GetLoadingProgress()
    {
        if (m_loadingOperation != null)
            return m_loadingOperation.progress;
        else
            return -1;
    }
}
