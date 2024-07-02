using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    #region Variables
    private static SceneHandler instance;
    public static SceneHandler Instance => instance;
    #endregion

    #region Singleton Implementation
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    #region Methods 
    #region Loading...
    //Function for loading scenes Synchronously..
    public void LoadScene(string sceneName)
    {
        Debug.Log(sceneName + " was loaded");
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    //Function to load Asynchronously..
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadAsyncScene(sceneName));
    }

    //Corroutine for loading scene Asynchronously.. 
    IEnumerator LoadAsyncScene(string sceneName)
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        //Waits till asynchronous scene loads fully...
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    #endregion

    #region Unloading...
    //Function to unload Asynchronously..
    public void UnloadScene(string sceneName)
    {
        StartCoroutine(UnloadAsyncScene(sceneName));
    }

    //Corroutine for unloading scene Asynchronously..
    IEnumerator UnloadAsyncScene(string sceneName)
    {
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneName);

        while (!asyncUnload.isDone)
        {
            yield return null;
        }

    }
    #endregion
    #endregion
}
