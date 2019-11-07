using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class GameEventController : MonoBehaviour {
    private const string enemyTag = "Enemy";
    public GameObject Enemy;
    private readonly int enemySize = 5;

    [SerializeField]
    public GameObject[] objectsToChnageState;
    public GameObject Player;
    public List<GameObject> leftEnemyShips;

    // Use this for initialization
    void Start()
    {
        SetGameObjectState(objectsToChnageState, false);

        // This start should be held as enumerator [level logic should be implemented here]

        List<GameObject> EnemysList = new List<GameObject>(enemySize);

        // TO DO in card implement some random shotting rates here
        for (int i = 0; i < enemySize; i++)
        {
            Enemy.SetActive(true);
            int newi = i * 2;
            Vector3 spawnPosition = new Vector3(-5f + newi, 7, -2);
            Instantiate(Enemy, spawnPosition, Quaternion.Euler(180f, 0, 0));
        }

    }
	
	// Update is called once per frame
	void Update () {

	}

    // Need to set active two buttons here (Quit and Restart) since the logic of the game, elswhere should be also stopped
    public void GameOver()
    {
        SetGameObjectState(objectsToChnageState, true);
        FreeLeftObjects(GameObject.FindGameObjectsWithTag(enemyTag));
        ChangeObjectState(Player, false);

        // Would be enough to set Player object as unactive here
    }

    private void SetGameObjectState(GameObject[] objectsArray, bool state)
    {
        foreach (GameObject item in objectsArray)
        {
            item.SetActive(state);
        }  
    }

    private void FreeLeftObjects(GameObject[] objectsArray)
    {
        foreach (GameObject item in objectsArray)
        {
            Destroy(item);
        }
    }

    private void ChangeObjectState(GameObject Object, bool state)
    {
        Object.SetActive(state);
    }

}
