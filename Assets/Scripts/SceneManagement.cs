using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void EnterGame ()
    {
        GameSaveManager.SaveGame();
        SceneManager.LoadScene(1);
    }

    public void Exit ()
    {
        GameSaveManager.SaveGame();
        Debug.Log("Quit!!");
        Application.Quit();
    }
    public void BackToWelcome ()
    {
        GameSaveManager.SaveGame();
        SceneManager.LoadScene(0);
    }
    public void ToItemShop () {
        GameSaveManager.SaveGame();
        SceneManager.LoadScene(2);
    }
    public void ToSocials () {
        GameSaveManager.SaveGame();
        SceneManager.LoadScene(3);
    }
    public void ToClassicGameMode () {
        GameSaveManager.SaveGame();
        SceneManager.LoadScene(4);
    }
    public void ToLeveledGameMode () {
        GameSaveManager.SaveGame();
        SceneManager.LoadScene(5);
    }
    public void ToClassicGameStart () {
        GameSaveManager.SaveGame();
        SceneManager.LoadScene(6);
    }
    public void ToLeveledGameStart () {
        GameSaveManager.SaveGame();
        SceneManager.LoadScene(7);
    }
    public void ToClassicGameEnd () {
        GameSaveManager.SaveGame();
        SceneManager.LoadScene(8);
    }
    public void ToLeveledGameEndGood () {
        GameSaveManager.SaveGame();
        SceneManager.LoadScene(9);
    }
    public void ToLeveledGameEndBad () {
        GameSaveManager.SaveGame();
        SceneManager.LoadScene(10);
    }
    public void ToTrainingMenu () {
        GameSaveManager.SaveGame();
        SceneManager.LoadScene(11);
    }
    public void ToTrainingGameStart () {
        GameSaveManager.SaveGame();
        SceneManager.LoadScene(12);
    }

    void OnApplicationQuit()
    {
        GameSaveManager.SaveGame();
    }
}
