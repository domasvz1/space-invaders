using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Refresh : MonoBehaviour {


    public GameObject[] textList;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        int i = 0;
        DataHandler dh = gameObject.GetComponent<DataHandler>();
        List<PlayerScore> topPlayers = new List<PlayerScore>();
        topPlayers = dh.GetTopFive();

        GameObject[] gol = GameObject.FindGameObjectsWithTag("Scores");

        foreach (var item in topPlayers)
        {
            gol[i].GetComponent<Text>().text = item.name;
            i++;
        }
    }
}
