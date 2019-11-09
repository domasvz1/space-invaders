using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEventController : MonoBehaviour {

    private const string enemyTag = "Enemy";
    public GameObject Enemy;
    private bool visited = false;

    [SerializeField]
    public GameObject[] endGameUIObjects;

    public List<GameObject> leftEnemyShips;
    public int timeLeft;
    public Text countdownText;
    public Text scoreText;
    public AudioSource gameOverSound;
    public AudioSource inGameSound;

    public bool gameOver = false;
    public int score = 0, wave = 0;
    private const int levelWavesCount = 4;
    private float enemySpeed = 0.2f, increasedSpeedWithWave = 0.5f;
    private readonly int firstLineCount = 4, secondLineCount = 5;

    //  This constant declaers the movemnt speed of player in the begining of the game when the timer is ticking
    private const float movePlayerSpeed = 0.02f;

    // There's little exception for locked player Icon in the begining
    // All the Images that need to shown at startng UI and then disappear
    public GameObject Player;

    // Use this for initialization
    void Start()
    {
        // When the ship arises, the movemnt of the player is locked
        Player.GetComponent<PlayerController>().enabled = false;

        // We need our GameEvent object active to make the countdown
        ChangeObjectState(gameObject, true); 

        // The logic here, we have the list of elements we want to be only active in Game
        SetGameObjectsState(endGameUIObjects, false);

        // Invoking Couroutine, hiding UI elements, revealing gameobjects
        StartCoroutine("CountDown");        
    }
	
	void FixedUpdate ()
    {
        scoreText.text = "Highscore: " + score;

        // Need to get all the exisitng Enemies
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(enemyTag))
        {
            item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y - (Time.deltaTime * enemySpeed), item.transform.position.z);
        }
	}

    private void Update()
    {
        if (!gameOver)
        {
            if (timeLeft <= 0)
            {
                // When the time is up, we check are there any enemies in the map
                if (GameObject.FindGameObjectsWithTag(enemyTag).Length == 0 && wave < levelWavesCount)
                {
                    // We invoke enemy spawn 
                    SpawnEnemyWave();
                    wave += 1; // Increase the
                    enemySpeed += increasedSpeedWithWave;
                }
                else // If the count waves ha passes we spawn the boss
                {
                    SpawnBoss();
                }


                if (!visited)
                {
                    // Stopping coroutine, hidinh text, revealing objects
                    StopCoroutine("CountDown");
                    countdownText.GetComponent<Text>().enabled = false;

                    // After the coroutine has ended the movemnt of the player is enabed again
                    Player.GetComponent<PlayerController>().enabled = true;

                    // Since we have a global array for enemy object, we can pass it to the state changer fucntion
                    visited = true;
                    // Start the ingame music
                    inGameSound.Play();
                    inGameSound.loop = true;
                }
            }

            else
            {
                Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + movePlayerSpeed, Player.transform.position.z);
                // Moved logic from Timer to here, it makes more sense to have all timing logic handled not seperately
                countdownText.text = ("Level Begins In " + timeLeft);
            }
        }
        else
            GameOver();

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
        // Stop ingame sound and start playing the rockingGameOver sound
        inGameSound.Stop();
        gameOverSound.Play();
        gameOverSound.volume = 0.7f;

        // Stop the update cycle
        gameOver = true;
        // Lets check if there's any object left undestroyed
        string[] tagsToCheck = new string[]{ enemyTag, "CustomPlayerBullet", "CustomEnemyBullet" };
        SetGameObjectsState(endGameUIObjects, true);
        foreach (string tag in tagsToCheck)
        {
            FreeLeftObjects(GameObject.FindGameObjectsWithTag(tag));
        }
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

    private void SpawnEnemyLine(float moveX, float moveY, int enemyNumberInline, float xPosition)
    {
        // The first one will pawn substracting the moveX
        float yPosition = 14.0f - moveY; // only move down on how much we need to move it
        for (int i = 0; i < enemyNumberInline; i++)
        {
            xPosition += moveX;
            Vector3 spawnPosition = new Vector3(xPosition, yPosition, -2);
            Enemy.GetComponent<EnemyController>().repeatRate += 1 + i;
            Instantiate(Enemy, spawnPosition, Quaternion.Euler(180f, 0, 0));
        }
    }

    // Then we go spawning lines of waves individually
    private void SpawnEnemyWave()
    {
        SpawnEnemyLine(2.2f, 0f, secondLineCount, -8.9f);
        SpawnEnemyLine(2.2f, 2f, firstLineCount, -7.79f);
    }

    private void SpawnBoss()
    {

    }

}
