using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEventController : MonoBehaviour {

    private const string enemyTag = "Enemy";
    public GameObject Enemy;
    private readonly int enemyShipsCount = 5;
    private bool visited = false;
    private GameObject[] enemyObjects;

    [SerializeField]
    public GameObject[] uiObjects;

    [SerializeField]
    public GameObject[] ingameObjects;

    public List<GameObject> leftEnemyShips;
    public int timeLeft;
    public Text countdownText;

    private float enemySpeed = 0.1f;

    // There's little exception for locked player Icon in the begining
    // All the Images that need to shown at startng UI and then disappear
    public GameObject playerLockedImage;

    // Use this for initialization
    void Start()
    {
        //Invoking locked picture to be visible untill the Coroutine is finished
        ChangeObjectState(playerLockedImage, true);

        // We need our GameEvent object active to make the countdown
        ChangeObjectState(gameObject, true); 

        // The logic here, we have the list of elements we want to be only active in Game
        SetGameObjectsState(ingameObjects, false);
        SetGameObjectsState(uiObjects, false);

        // Invoking Couroutine, hiding UI elements, revealing gameobjects
        StartCoroutine("CountDown");

        // The Spawn Of enemies
        enemyObjects = new GameObject[enemyShipsCount];
        for (int i = 0; i < enemyShipsCount; i++)
        {
            int newi = i * 2;
            Vector3 spawnPosition = new Vector3(-6.5f + newi, 7, -2);
            Enemy.GetComponent<EnemyController>().repeatRate += 1 + i;
            GameObject go =  Instantiate(Enemy, spawnPosition, Quaternion.Euler(180f, 0, 0));
            enemyObjects[i] = go;
        }
        SetGameObjectsState(enemyObjects, false);
        
    }
	
	void FixedUpdate ()
    {
        
        // Need to get all the exisitng Enemies
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(enemyTag))
        {
            item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y - (Time.deltaTime * enemySpeed), item.transform.position.z);
        }
	}

    private void Update()
    { 
        // Moved logic from Timer to here, it makes more sense to have all timing logic handled not seperately
        countdownText.text = ("Level Begins In " + timeLeft);

        if (timeLeft <= 0)
        {
            if (!visited)
            {
                // Stopping coroutine, hidinh text, revealing objects
                StopCoroutine("CountDown");
                countdownText.GetComponent<Text>().enabled = false;
                ChangeObjectState(playerLockedImage, false); // Hiding players icon
                SetGameObjectsState(ingameObjects, true);

                // Since we have a global array for enemy object, we can pass it to the state changer fucntion
                SetGameObjectsState(enemyObjects, true);
                visited = true;
            }
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

    // Need to set active two buttons here (Quit and Restart) since the logic of the game, elswhere should be also stopped
    public void GameOver()
    {
        // Lets check if there's any object left undestroyed
        string[] tagsToCheck = new string[]{ enemyTag, "CustomPlayerBullet", "CustomEnemyBullet" };
        SetGameObjectsState(uiObjects, true);
        foreach (string tag in tagsToCheck)
        {
            FreeLeftObjects(GameObject.FindGameObjectsWithTag(tag));
        }

        // Change ingame object to invisible state
        SetGameObjectsState(ingameObjects, false);
        ChangeObjectState(gameObject, false);
    }

    private void SetGameObjectsState(GameObject[] objectsArray, bool state)
    {
        foreach (GameObject item in objectsArray)
        {
            item.SetActive(state);
        }  
    }

    private void FreeLeftObjects(GameObject[] objectsArray)
    {
        // Even though there's no NullReferenceException returned, but just to be sure
        if (objectsArray != null)
        {
            foreach (GameObject item in objectsArray)
            {
                Destroy(item);
            }
        }
    }

    private void ChangeObjectState(GameObject Object, bool state)
    {
        Object.SetActive(state);
    }

}
