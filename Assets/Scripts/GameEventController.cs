using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventController : MonoBehaviour {

    public GameObject Enemy;
    
    private readonly int enemySize = 10;


    // Use this for initialization
    void Start()
    {
     
        List<GameObject> EnemysList = new List<GameObject>(enemySize);

        for (int i = 0; i < enemySize; i++)
        {
            Enemy.SetActive(true);
            int newi = i * 1;
            Vector3 spawnPosition = new Vector3(-7f + newi, 7, -2);
            Instantiate(Enemy, spawnPosition, Quaternion.identity);
        }
        //Now we have a list full of enemies
        // We can set their transform positions here, since they are in
        //Enemy.SetActive(true);
        //Vector3 spawnPosition = new Vector3(-2.5f, 8+ (i * 5), -2);
        //Instantiate(Enemy, spawnPosition, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
