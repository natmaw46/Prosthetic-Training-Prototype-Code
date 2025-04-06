using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int Highscore;
    public int Currentscore;
    public int HighestLevel;
    public int SelectedLevel;
    public int CoinsOwned;
    public int DifficultyLevel;
    public int TrainingHighscore;
    public int TrainingScore;
    public float fruitSize;
    public string UserName;
    public int UserID;
    public List<string> FriendsList;
    public List<string> PlayersList;
    public List<int> BackgroundOwned;
    public int BackgroundSelected;
    public List<int> IconOwned;
    public int IconSelected;
}

public class GameSaveManager : MonoBehaviour
{
    private static string savePath => Application.persistentDataPath + "/gameData.json";

    public static void SaveGame()
    {
        GameData data = new GameData
        {
            Highscore = GameInit.Highscore,
            Currentscore = GameInit.Currentscore,
            HighestLevel = GameInit.HighestLevel,
            SelectedLevel = GameInit.SelectedLevel,
            CoinsOwned = GameInit.CoinsOwned,
            DifficultyLevel = GameInit.DifficultyLevel,
            TrainingHighscore = GameInit.TrainingHighscore,
            TrainingScore = GameInit.TrainingScore,
            fruitSize = GameInit.fruitSize,
            UserName = GameInit.UserName,
            UserID = GameInit.UserID,
            FriendsList = GameInit.FriendsList,
            PlayersList = GameInit.PlayersList,
            BackgroundOwned = GameInit.BackgroundOwned,
            BackgroundSelected = GameInit.BackgroundSelected,
            IconOwned = GameInit.IconOwned,
            IconSelected = GameInit.IconSelected
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game Saved: " + savePath);
    }

    public static void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            GameData data = JsonUtility.FromJson<GameData>(json);

            GameInit.Highscore = data.Highscore;
            GameInit.Currentscore = data.Currentscore;
            GameInit.HighestLevel = data.HighestLevel;
            GameInit.SelectedLevel = data.SelectedLevel;
            GameInit.CoinsOwned = data.CoinsOwned;
            GameInit.DifficultyLevel = data.DifficultyLevel;
            GameInit.TrainingHighscore = data.TrainingHighscore;
            GameInit.TrainingScore = data.TrainingScore;
            GameInit.fruitSize = data.fruitSize;
            GameInit.UserName = data.UserName;
            GameInit.UserID = data.UserID;
            GameInit.FriendsList = data.FriendsList ?? new List<string>();
            GameInit.PlayersList = data.PlayersList ?? new List<string>();
            GameInit.BackgroundOwned = data.BackgroundOwned ?? new List<int>();
            GameInit.BackgroundSelected = data.BackgroundSelected;
            GameInit.IconOwned = data.IconOwned ?? new List<int>();
            GameInit.IconSelected = data.IconSelected;

            Debug.Log("Game Loaded Successfully!");
        }
        else
        {
            Debug.LogWarning("No Save File Found. Loading Defaults...");
        }
    }
}