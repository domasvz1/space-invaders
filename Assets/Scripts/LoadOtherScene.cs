using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOtherScene : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        // Load the passed scene in the Inspector
        SceneManager.LoadScene(sceneName);
    }

    public void RestartSame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        //Debug.Log("This will quit the application");
        Application.Quit();
    }
}
