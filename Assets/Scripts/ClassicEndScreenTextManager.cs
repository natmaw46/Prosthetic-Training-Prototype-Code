using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ClassicEndScreenTextManager : MonoBehaviour
{
    public TMP_Text currentScoreText;
    public TMP_Text highScoreText;
    public TMP_Text coinOwnedText;
    public TMP_Text coinEarnedText;
    
    private void Awake()
    {
        int currentScore = GameInit.Currentscore;
        currentScoreText.text = "Score: " + currentScore.ToString();

        int highScore = GameInit.Highscore;
        if (currentScore > highScore) {
            GameInit.Highscore = currentScore;
        }
        highScore = GameInit.Highscore;
        highScoreText.text = "Highscore:  " + highScore.ToString();

        int coinsEarned = currentScore / 2;
        GameInit.CoinsOwned += coinsEarned;
        coinEarnedText.text = coinsEarned.ToString();

        int coinAmount = GameInit.CoinsOwned;
        coinOwnedText.text = coinAmount.ToString();
    }

    void OnApplicationQuit()
    {
        GameSaveManager.SaveGame();
    }
}
