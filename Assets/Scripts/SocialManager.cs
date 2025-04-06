using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class SocialManager : MonoBehaviour
{
    public TMP_Text highScoreText;
    public TMP_Text highestLevelText;
    public TMP_Text trainingHighScoreText;
    public TMP_Text userNameText;
    public TMP_Text userIDText;
    public Image userNameChange;
    public Image addFriendMenu;
    public Transform friendsListContents;
    public GameObject friendPrefab;
    
    private void Awake()
    {
        userNameChange.gameObject.SetActive(false);
        addFriendMenu.gameObject.SetActive(false);

        int highScore = GameInit.Highscore;
        highScoreText.text = "Slicing Classic Highscore: " + highScore.ToString();

        int highestLevel = GameInit.HighestLevel;
        if (highestLevel == 0) 
        {
            highestLevelText.text = "Slicing Highest Level: 1";
        }
        else
        {
            highestLevelText.text = "Slicing Highest Level: " + highestLevel.ToString();
        }

        int trainingHighscore = GameInit.TrainingHighscore;
        trainingHighScoreText.text = "Grip Training Highscore: " + trainingHighscore.ToString();

        String userName = GameInit.UserName;
        userNameText.text = "User Name: " + userName;

        int userID = GameInit.UserID;
        userIDText.text = "User ID: " + userID.ToString();

        FriendsListManage();
    }

    public void OpenEditUserName()
    {
        userNameChange.gameObject.SetActive(true);
    }

    public void CloseEditUserName()
    {
        userNameChange.gameObject.SetActive(false);
        String userName = GameInit.UserName;
        userNameText.text = "User Name: " + userName;
    }

    public void FriendsListManage()
    {
        List<string> newFriendsList = GameInit.FriendsList;
        foreach (string friend in newFriendsList)
        {
            Debug.Log(friend);
            GameObject newFriend = Instantiate(friendPrefab, friendsListContents);

            TMP_Text nameText = newFriend.GetComponentInChildren<TMP_Text>();
            nameText.text = friend;
        }
    }
    public void OpenAddFriend()
    {
        addFriendMenu.gameObject.SetActive(true);
    }
    public void CloseAddFriend()
    {
        foreach (Transform child in friendsListContents)
        {
            Destroy(child.gameObject);
        }
        addFriendMenu.gameObject.SetActive(false);
        FriendsListManage();
    }

    void OnApplicationQuit()
    {
        GameSaveManager.SaveGame();
    }
}
