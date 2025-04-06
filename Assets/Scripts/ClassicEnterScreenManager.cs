using TMPro;
using UnityEngine;

public class ClassicEnterScreenManager : MonoBehaviour
{
    public TMP_Text highScoreText;
    private void Awake() {
        int highScore = GameInit.Highscore;
        highScoreText.text = "Highscore:  " + highScore.ToString();
    }
    void OnApplicationQuit()
    {
        GameSaveManager.SaveGame();
    }
}
