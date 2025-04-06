using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LeveledEndScreenTextManager : MonoBehaviour
{
    public TMP_Text currentLevelText;
    public TMP_Text highestLevelText;
    public TMP_Text coinOwnedText;
    public TMP_Text coinEarnedText;
    public int defaultCoinsEarnedPerLevel;
    
    private void Awake()
    {
        int currentLevel = GameInit.SelectedLevel;
        currentLevelText.text = "Level: " + currentLevel.ToString();

        int highestLevel = GameInit.HighestLevel;
        if (highestLevel == 0) {
            highestLevelText.text = "Highest Level:  1";
        } else {
            highestLevelText.text = "Highest Level:  " + highestLevel.ToString();
        }
        

        int coinsEarned = defaultCoinsEarnedPerLevel;
        GameInit.CoinsOwned += defaultCoinsEarnedPerLevel;
        coinEarnedText.text = coinsEarned.ToString();

        int coinAmount = GameInit.CoinsOwned;
        coinOwnedText.text = coinAmount.ToString();
    }

    public void NextLevel() 
    {
        GameInit.SelectedLevel = GameInit.SelectedLevel + 1;
    }

    void OnApplicationQuit()
    {
        GameSaveManager.SaveGame();
    }
}
