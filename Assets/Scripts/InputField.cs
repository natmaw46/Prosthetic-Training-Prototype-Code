using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using TMPro;

public class InputFieldHandler : MonoBehaviour
{
    [SerializeField] InputField inputField;
    public TMP_Text warnText;

    public void SaveInput()
    {
        string input = inputField.text;
        GameInit.UserName = input;
    }

    public void ValidateFriendInput()
    {
        string input = inputField.text;
        List<string> friendsList = GameInit.FriendsList;
        List<string> usersList = GameInit.PlayersList;
        if (usersList.Contains(input))
        {   
            if(friendsList.Contains(input))
            {
                warnText.text = "USER IS ALREADY YOUR FRIEND";
                warnText.color = Color.red;
            } 
            else 
            {
                friendsList.Add(input);

                warnText.text = "FRIEND ADDED";
                warnText.color = Color.green;
            }
        }
        else 
        {
            warnText.text = "USER DOES NOT EXIST";
            warnText.color = Color.red;
        }
    }
}