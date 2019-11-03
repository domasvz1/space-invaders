using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOtherScene : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Simply Changes The scene
    }

    public void QuitGame()
    {
        //Debug.Log("This will quit");
        Application.Quit();
    }
}
