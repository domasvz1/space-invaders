using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleDataToFile : MonoBehaviour {

    public GameObject userNameSubmitButton;
    public GameObject userNameInput;
    public string username;
    public int score;

    public void SendDataToFile()
    {
        // Can we done all sorts of handling here from checking lenth and so on, right now its only this
        if(userNameInput.GetComponent<InputField>().text.ToString() != "")
        {
            DataHandler dh = GameObject.FindGameObjectWithTag("GameEvents").GetComponent<DataHandler>();
            score = GameObject.FindGameObjectWithTag("GameEvents").GetComponent<GameEventController>().score;
            username = userNameInput.GetComponent<InputField>().text.ToString();
            dh.SortHighscoresArray(score, username);
            gameObject.SetActive(false);
            userNameInput.SetActive(false);
        }
        else
        {
            userNameInput.GetComponent<InputField>().text = "Casual Player";
        }
    }
}
