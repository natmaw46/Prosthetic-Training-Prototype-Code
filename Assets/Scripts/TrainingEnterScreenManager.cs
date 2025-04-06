using TMPro;
using UnityEngine;

public class TrainingEnterScreenManager : MonoBehaviour
{
    public TMP_Text trainingHighscoreText;
    private void Awake() {
        int trainingHighscore = GameInit.TrainingHighscore;
        trainingHighscoreText.text = "Highscore:  " + trainingHighscore.ToString();
    }

    public void SetEasy() {
        GameInit.fruitSize = 1.1f;
    }

    public void SetNormal() {
        GameInit.fruitSize = 1.6f;
    }

    public void SetDifficult() {
        GameInit.fruitSize = 2.0f;
    }

    void OnApplicationQuit()
    {
        GameSaveManager.SaveGame();
    }
}
