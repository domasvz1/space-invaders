using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int timeLeft;
    public Text countdownText;
    // All the game objects that need to be frozen before start
    public GameObject PlayerObject, GameEventObject, Prefab;


    // All the Images that need to shown at startng UI and then disappear
    public Image PlayerImage;

    //public Image  ViperImage, TaxiImage, SanchezImage;

    // This script sets all the scripts with the movements to activ eafter the certain time
    private void Start()
    {
        StartCoroutine("CountDown");
        SetTheCondition(false,true);
    }

    // Update is called once per frame
    private void Update()
    {
        countdownText.text = ("Level Begins In " + timeLeft);

        if (timeLeft <= 0)
        {
            StopCoroutine("CountDown");
            countdownText.GetComponent<Text>().enabled = false;
            SetTheCondition(true, false);
        }
    }

    IEnumerator CountDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }

    void SetTheCondition(bool gameObjectsCondition, bool condition)
    {
        // Arrays to fit in all Objects and image so that we wouldn't need to duplicated code

        // -- Take the listing from here
        GameObject[] GameObjectsArray = { PlayerObject, GameEventObject };

        //Image[] ImagesArray = { PlayerImage, ViperImage, TaxiImage, SanchezImage };
        Image[] ImagesArray = { PlayerImage };


        // Not both array are always the same zie
        foreach (GameObject obj in GameObjectsArray)
        {
            obj.SetActive(gameObjectsCondition);
        }

        foreach (Image img in ImagesArray)
        {
            img.enabled = condition;
        }

        // Scripts
        GameEventObject.GetComponent<GameEventController>().enabled = gameObjectsCondition;
        Prefab.SetActive(gameObjectsCondition);
    }
}