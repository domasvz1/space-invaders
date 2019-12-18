using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEventController : MonoBehaviour {

    private const string enemyTag = "Enemy", bossTag = "Boss",
        eBTag = "CustomPlayerBullet", pBTag = "CustomEnemyBullet",
        speedPickupTag = "PlayerSpeedPickup", shootingPicktupTag = "ShootingSpeedPickup", shieldPickupTag = "ShieldPickup",
        winningText = "You win";

    // Defined three main objects in this game, player, enemy spacehip which will be duplicated into more spaceships and boss
    public GameObject enemy, boss, player;
    private bool visited = false, topScored = false;

    [SerializeField]
    public GameObject[] endGameUIObjects;

    public List<GameObject> leftEnemyShips;
    public int timeLeft;
    public Text countdownText, scoreText;

    public GameObject userNameSubmitButton, userNameInput, endGameText;
    public AudioSource gameOverSound, gameWinnerSound, inGameSound;

    public bool gameOver = false;
    public int score = 0, wave = 0;
    public int wavesInLevel = 4;
    private float enemySpeed = 0.05f, initialScale = 2.0f;
    private readonly int firstLineCount = 4, secondLineCount = 5;

    // Pickup GameObjects
    public GameObject playerSpeedPickup, shootingSpeedPickup, shieldPickup;
    // Pickup Images in game when you have them
    public GameObject playerspeedImage, shootingspeedImage, shieldImage;
        
    public GameObject[] pickupImages;

    //  This constant declaers the movemnt speed of player in the begining of the game when the timer is ticking
    private const float movePlayerSpeed = 0.02f, increasedSpeedWithWave = 0.5f;

    private bool hasBossSpawned = false;

    // Use this for initialization
    public void Start()
    {
        pickupImages = new GameObject[] { playerspeedImage, shootingspeedImage, shieldImage };

        SetObjectsArrayState(pickupImages, false);

        // When the ship arises, the movemnt of the player is locked
        player.GetComponent<PlayerController>().enabled = false;

        // We need our GameEvent object active to make the countdown
        ChangeObjectState(gameObject, true); 

        // The logic here, we have the list of elements we want to be only active in Game
        SetObjectsArrayState(endGameUIObjects, false);

        // Winning and losing texts
        ChangeObjectState(endGameText, false);

        // User submiting username
        ChangeObjectState(userNameInput, false);
        ChangeObjectState(userNameSubmitButton, false);

        // Invoking Couroutine, hiding UI elements, revealing gameobjects
        StartCoroutine("CountDown");        
    }
	
	public void FixedUpdate ()
    {
        scoreText.text = "Highscore: " + score;

        // Need to get all the exisitng Enemies
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(enemyTag))
        {
            item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y - (Time.deltaTime * enemySpeed), item.transform.position.z);
        }

        if(hasBossSpawned)
        {
            foreach (GameObject boss in GameObject.FindGameObjectsWithTag(bossTag))
            {
                boss.transform.position = new Vector3(boss.transform.position.x, boss.transform.position.y - (Time.deltaTime * enemySpeed), boss.transform.position.z);
            }
        }
    }

    public void Update()
    {
        if (!gameOver)
        {
            if (timeLeft <= 0)
            {
                // When the time is up, we check are there any enemies in the map
                if (GameObject.FindGameObjectsWithTag(enemyTag).Length == 0 && wave < wavesInLevel)
                {
                    // We invoke enemy spawn 
                    SpawnEnemyWave();
                    wave += 1; // Increase the
                    enemySpeed += increasedSpeedWithWave;

                    switch (wave)
                    {
                        case 1:
                            // Next level invoking
                            ChangeObjectState(Instantiate(shootingSpeedPickup,
                                shootingSpeedPickup.transform.position,
                                Quaternion.Euler(180f, 0, 0)),
                                true);
                            break;
                        // After two waves lets simply spawn player speed pickup
                        case 2:
                            ChangeObjectState(Instantiate(playerSpeedPickup,
                                playerSpeedPickup.transform.position,
                                Quaternion.Euler(180f, 0, 0)),
                                true);
                            break;

                        case 3:
                            ChangeObjectState(Instantiate(shootingSpeedPickup,
                                shootingSpeedPickup.transform.position,
                                Quaternion.Euler(180f, 0, 0)),
                                true);
                            break;
                        // After 4 waves lets spawn shield pickup
                        case 4:
                            ChangeObjectState(Instantiate(shieldPickup,
                                shieldPickup.transform.position,
                                Quaternion.Euler(180f, 0, 0)),
                                true);
                            break;
                        default:
                            break;
                    }
                }
                // If the wave count reaches maximum we spawn the Boss Ship
                else if (wave >= wavesInLevel && GameObject.FindGameObjectsWithTag(enemyTag).Length == 0)
                { 
                    if(!hasBossSpawned)
                    { 
                        SpawnBoss();
                    }
                }

                if (!visited)
                {
                    // Stopping coroutine, hidinh text, revealing objects
                    StopCoroutine("CountDown");
                    countdownText.GetComponent<Text>().enabled = false;

                    // After the coroutine has ended the movemnt of the player is enabed again
                    player.GetComponent<PlayerController>().enabled = true;

                    // Since we have a global array for enemy object, we can pass it to the state changer fucntion
                    visited = true;
                    // Start the ingame music
                    inGameSound.Play();
                    inGameSound.loop = true;
                }
            }

            else
            {
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + movePlayerSpeed, player.transform.position.z);
                // Moved logic from Timer to here, it makes more sense to have all timing logic handled not seperately
                countdownText.text = ("Level Begins In " + timeLeft);
            }
        }
        else // Game over is here, but we always check if the player has collected the amount of points needed to push others players
        {
            DataHandler dh = GetComponent<DataHandler>();
            // If the score is higher than at least one the users
            if (dh.IsScoreInTopList(score) && !topScored)
            {
                ChangeObjectState(userNameInput, true);
                ChangeObjectState(userNameSubmitButton, true);
                topScored = true;
            }
        }
    }

    public IEnumerator CountDown()
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
        SetObjectsArrayState(endGameUIObjects, true);
        ChangeObjectState(endGameText, true);
        StartCoroutine(ScaleObject(endGameText));
        gameOver = true; // finish game cycle
        ClearLeftObjects();  // Clear possible active obejcts in the scene
    }

    public void Winning()
    {
        inGameSound.Stop();
        gameWinnerSound.Play();
        gameWinnerSound.volume = 0.8f;
        SetObjectsArrayState(endGameUIObjects, true);
        endGameText.GetComponent<Text>().text = winningText;
        endGameText.GetComponent<Text>().color = Color.green;
        ChangeObjectState(endGameText, true);
        StartCoroutine(ScaleObject(endGameText));
        gameOver = true;  // finish game cycle
        ClearLeftObjects(); // Clear possible active obejcts in the scene
    }

    public void SetObjectsArrayState(GameObject[] objectsArray, bool state)
    {
        foreach (GameObject item in objectsArray)
        {
            item.SetActive(state);
        }  
    }

    public static void FreeObjectsInArray(GameObject[] objectsArray)
    {
        // Even though there's no NullReferenceException returned, but just to be sure
        if (!objectsArray.Equals(null))
        {
            foreach (GameObject item in objectsArray)
            {
                Destroy(item);
            }
        }
    }

    public void ChangeObjectState(GameObject Object, bool state)
    {
        Object.SetActive(state);
    }

    public  void SpawnEnemyLine(float moveX, float moveY, int enemyNumberInline, float xPosition)
    {
        // The first one will pawn substracting the moveX
        float yPosition = 14.0f - moveY; // only move down on how much we need to move it
        for (int i = 0; i < enemyNumberInline; i++)
        {
            xPosition += moveX;
            Vector3 spawnPosition = new Vector3(xPosition, yPosition, -2);
            enemy.GetComponent<EnemyController>().repeatRate += 1 + i;
            Instantiate(enemy, spawnPosition, Quaternion.Euler(180f, 0, 0));
        }
    }

    // Then we go spawning lines of waves individually
    private void SpawnEnemyWave()
    {
        const float xCoord = 2.2f, yFirstWave = 0f, ySecondWave = 2.2f;
        SpawnEnemyLine(xCoord, yFirstWave, secondLineCount, -8.9f);
        SpawnEnemyLine(xCoord, ySecondWave, firstLineCount, -7.79f);
    }

    private void SpawnBoss()
    {
        Instantiate(boss, new Vector3(-2.3f, 13.6f, -2), Quaternion.Euler(180f, 0, 0));
        hasBossSpawned = true;
    }

    public void ClearLeftObjects()
    {
        string[] tagsToCheck = new string[] { enemyTag, pBTag, eBTag, speedPickupTag, shootingPicktupTag, shieldPickupTag };
        foreach (string tag in tagsToCheck)
        {
            FreeObjectsInArray(GameObject.FindGameObjectsWithTag(tag));
        }
    }

    private IEnumerator ScaleObject(GameObject go)
    {
        const float maxScale = 2.0f, minScale = 1.6f, pulsePause = 0.5f;
        // I'm using LogoScript in Menu and Instruction Scenes, so I play the continue playing music in both of them
        while (true)
        {
            if (initialScale.Equals(maxScale))
                initialScale = minScale;
            else if (initialScale.Equals(minScale))
                initialScale = maxScale;
            go.GetComponent<Text>().transform.localScale = new Vector3(initialScale, initialScale, initialScale);

            yield return new WaitForSeconds(pulsePause);
        }
    }
}