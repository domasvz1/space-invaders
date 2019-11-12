﻿using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;

public class DataHandler : MonoBehaviour {

    private readonly string fileName = "scores.json";
    private string filePath;
    private const int placesCount = 5;

    GameData gameData = new GameData();

    // Use this for initialization
    void Start()
    {
        // Set the file path compatable with mobile devices
        filePath = Application.persistentDataPath + "/" + fileName;
        //ResetTopPlayers();
    }

	// Update is called once per frame
	void Update () {

    }

    public void SaveData()
    {
        PlayerScore sc = new PlayerScore();
        sc.name = "Arnold";
        sc.highscore = 5;
        // Find his place
        gameData.scoresList.Add(sc);
        // Calling the Json Wrapper
        JsonWrapper wrapper = new JsonWrapper
        {
            gameData = gameData
        };

        // Turn gameobject to a string in Json format, put it in Json wrapper and save it

        string fileContents = JsonUtility.ToJson(wrapper,true); // true parameter gives extra spacing
        File.WriteAllText(filePath, fileContents);

    }

    public void SortHighscoresArray(int incomingScore, string incomingName)
    {
        // Check where the content of the file is changed
        try
        {

            // Check to see if a file exists
            if (File.Exists(filePath))
            {
                // We read data to the wraper and return it here
                string fileContents = File.ReadAllText(filePath);
                JsonWrapper wrapper = JsonUtility.FromJson<JsonWrapper>(fileContents);
                gameData = wrapper.gameData;

                // Lets sort the scores array
                foreach (PlayerScore ps in gameData.scoresList)
                { 
                    if (incomingScore > ps.highscore)
                    {
                        string tempName = ps.name;
                        int tempScore = ps.highscore;
                        ps.name = incomingName;
                        ps.highscore = incomingScore;
                        incomingName = tempName;
                        incomingScore = tempScore;
                    }
                }

                // Calling the Json Wrapper
                wrapper = new JsonWrapper
                {
                    gameData = gameData
                };

                // Turn gameobject to a string in Json format, put it in Json wrapper and save it

                fileContents = JsonUtility.ToJson(wrapper, true); // true parameter gives extra spacing
                File.WriteAllText(filePath, fileContents);

            }
            else
            {
                Debug.Log("Unable to read data");
                // Resetting data to the brand new state
                gameData = new GameData();

            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public void InsertFreshData()
    {
        string[] randomNames = new string[5] { "John", "Peter", "George", "Itan", "Mike" };
        int[] randomPlaces = new int[5] { 5, 4, 3, 2, 1 };
        int randomScoreMeter = 1;

        // No need to insert and then sort we, simple add dummy data from highest to lowest here and we only sort the incoming Player data with other methods
        for (int i = placesCount; i >= 1; --i)
        {
            PlayerScore sc = new PlayerScore
            {
                name = randomNames[i - 1],
                highscore = (randomScoreMeter * 2) + (5 * i),
                place = randomPlaces[i-1]
            };
            // Simply add t the gameDat list to insert into .json file
            gameData.scoresList.Add(sc);
        }

        // Calling the Json Wrapper
        JsonWrapper wrapper = new JsonWrapper
        {
            gameData = gameData
        };

        // Turn gameobject to a string in Json format, put it in Json wrapper and save it

        string fileContents = JsonUtility.ToJson(wrapper, true); // true parameter gives extra spacing
        File.WriteAllText(filePath, fileContents);
    }

    public bool IsScoreInTopList(int checkedScore)
    {
        try
        {
            if (File.Exists(filePath))
            {
                // We read data to the wraper and return it here
                string fileContents = File.ReadAllText(filePath);
                JsonWrapper wrapper = JsonUtility.FromJson<JsonWrapper>(fileContents);
                gameData = wrapper.gameData;

                // Lets sort the scores array
                foreach (PlayerScore ps in gameData.scoresList)
                {
                    if (checkedScore > ps.highscore)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    public void ResetTopPlayers()
    {
        InsertFreshData(); // Insert five Players
    }

    public List<PlayerScore> GetTopFive()
    {
        try
        {
            if (File.Exists(filePath))
            {
                // We read data to the wraper and return it here
                string fileContents = File.ReadAllText(filePath);
                JsonWrapper wrapper = JsonUtility.FromJson<JsonWrapper>(fileContents);
                gameData = wrapper.gameData;
                return gameData.scoresList;
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }
}
