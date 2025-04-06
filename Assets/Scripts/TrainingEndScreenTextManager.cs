using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TrainingEndScreenTextManager : MonoBehaviour
{
    public TMP_Text currentScoreText;
    public TMP_Text highScoreText;
    public TMP_Text coinOwnedText;
    public TMP_Text coinEarnedText;
    
    private void Awake()
    {
        int currentScore = GameInit.TrainingScore;
        currentScoreText.text = "Score: " + currentScore.ToString();

        int highScore = GameInit.TrainingHighscore;
        highScoreText.text = "Highscore:  " + highScore.ToString();

        int coinsEarned = currentScore;
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
