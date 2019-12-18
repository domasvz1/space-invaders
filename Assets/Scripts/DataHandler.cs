using UnityEngine;
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
        ResetTopPlayers();
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
                // -- True parameter gives extra spacing
                fileContents = JsonUtility.ToJson(wrapper, true);
                File.WriteAllText(filePath, fileContents);

            }
            else
            {
                Debug.Log("Unable to read data");
                // Resetting data to the brand new state
                gameData = new GameData();

            }
        }
        // This could be handleded better in the future
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public void InsertFreshData()
    {
        string[] randomNames = new string[5] { "John", "Peter", "George", "Itan", "Mike" };
        // Since there's only 5 seats
        int[] availablePlaces = new int[5] {5, 4, 3, 2, 1};
        int randomScoreMeter = 1;
        const int lastOne = 1;

        // No need to insert and then sort we, simple add dummy data from highest to lowest here and we only sort the incoming Player data with other methods
        for (int i = placesCount; i >= lastOne; --i)
        {
            PlayerScore sc = new PlayerScore
            {
                name = randomNames[i - lastOne],
                highscore = (randomScoreMeter * 2) + (5 * i),
                place = availablePlaces[i- lastOne]
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
                // Read data to the wraper and return it here
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
