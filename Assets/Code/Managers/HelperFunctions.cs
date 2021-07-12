using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelperFunctions : MonoBehaviour
{
    public void QuitApplication()
    {
        Application.Quit();
    }
    public void LoadSceneWithLoadingScreen(string sceneName)
    {
        SceneLoader.LoadScene(sceneName, true);
    }
    public void LoadScene(string sceneName)
    {
        SceneLoader.LoadScene(sceneName, false);
    }
    public void SetPaused(bool isPaused)
    {
        GlobalManager.Instance.SetPaused(isPaused);
    }
}
