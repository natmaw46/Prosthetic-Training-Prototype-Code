using System;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    public static int Highscore;
    public static int Currentscore; // not user save
    public static int HighestLevel;
    public static int SelectedLevel; // not user save
    public static int CoinsOwned;
    public static int DifficultyLevel; // not user save
    public static int TrainingHighscore;
    public static int TrainingScore; // not user save
    public static float fruitSize = 1.1f; // not user save
    public static String UserName = "FRUITMASTER123";
    public static int UserID = 1093745287;
    public static List<string> FriendsList = new List<string>   {   "newplayer12344",
                                                                    "newplayer56789",
                                                                    "gamer_king99",
                                                                    "pro_sniper99"
                                                                };
                                                        
    public static List<string> PlayersList = new List<string>   {   "newplayer12344",
                                                                    "newplayer56789",
                                                                    "gamer_king99",
                                                                    "shadow_ninja07",
                                                                    "firestormX",
                                                                    "coolguy_77",
                                                                    "legend_warrior",
                                                                    "mystic_fighter",
                                                                    "speedster_neo",
                                                                    "dark_knight_45",
                                                                    "ultra_gamer_x",
                                                                    "pro_sniper99",
                                                                    "not_friend_yet",
                                                                    "not_friend_yet2",
                                                                    "not_friend_yet3",
                                                                    "not_friend_yet4"
                                                                }; // in database

    public static List<int> BackgroundOwned = new List<int> { 1 };
    public static int BackgroundSelected = 1;

    public static List<int> IconOwned = new List<int> { 1 };
    public static int IconSelected = 1;
}
