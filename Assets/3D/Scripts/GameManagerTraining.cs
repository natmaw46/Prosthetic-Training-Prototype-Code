using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class GameManagerTraining : MonoBehaviour
{
    public TMP_Text timerText;

    private Gripper gripper;
    private FruitSpawnerTraining fruitSpawner;
    
    public float timer = 60f;
    private int score;
    public TMP_Text gameClearedText;
    public TMP_Text scoreText;
    public TMP_Text ready;
    public TMP_Text set;
    public TMP_Text go;
    public Image pauseMenu;
    public UnityEngine.UI.Button pauseBtn;
    public InGameAudioManager inGameAudioManager;

    private void Awake()
    {
        GameInit.DifficultyLevel = GameInit.SelectedLevel;
        gripper = FindObjectOfType<Gripper>();
        fruitSpawner = FindObjectOfType<FruitSpawnerTraining>();
    }

    private void Start()
    {
        gameClearedText.enabled = false;
        ready.enabled = false;
        set.enabled = false;
        go.enabled = false;
        pauseMenu.gameObject.SetActive(false);
        NewGame();   
    }

    private void NewGame()
    {        
        gripper.enabled = true;
        fruitSpawner.enabled = true;
        pauseBtn.enabled = true;
        
        GameInit.TrainingScore = 0;
        score = 0;
        timerText.text = timer.ToString();

        StartCoroutine(ReadySetGo());
        ClearScene();
    }

    public IEnumerator ReadySetGo() 
    {
        ready.enabled = true;
        inGameAudioManager.PlayReadySetSound();

        yield return new WaitForSeconds(1f);

        ready.enabled = false;
        set.enabled = true;
        inGameAudioManager.PlayReadySetSound();

        yield return new WaitForSeconds(1f);

        set.enabled = false;
        go.enabled = true;
        inGameAudioManager.PlayReadySetSound();

        yield return new WaitForSeconds(1f);

        go.enabled = false;
        inGameAudioManager.PlayGoSound();

        while (timer > 0)
        {
            timerText.text = Mathf.Ceil(timer).ToString();
            timer -= Time.deltaTime;
            yield return null;
        }

        timerText.text = "0";
        fruitSpawner.enabled = false;

        Fruits[] fruits = FindObjectsOfType<Fruits>();

        while (fruits.Length != 0) {

            fruits = FindObjectsOfType<Fruits>();

            yield return null;
        }
        
        pauseBtn.enabled = false;

        gripper.enabled = false;

        GameInit.TrainingScore = score;
        
        if (GameInit.TrainingHighscore < score) {
            GameInit.TrainingHighscore = score;
        }

        gameClearedText.enabled = true;
        inGameAudioManager.PlayGameFinishedSound();

        yield return new WaitForSeconds(3f);
        
        GameSaveManager.SaveGame();
        SceneManager.LoadScene(13);
    }

    public void Pause()
    {
        pauseMenu.gameObject.SetActive(true);
        pauseBtn.enabled = false;
        gripper.enabled = false;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.gameObject.SetActive(false);
        pauseBtn.enabled = true;
        gripper.enabled = true;
        Time.timeScale = 1f;
    }

    private void ClearScene()
    {
        Fruits[] fruits = FindObjectsOfType<Fruits>();

        foreach (Fruits fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }
    }

    public void IncreaseScore()
    {
        inGameAudioManager.PlayPopSound();
        score++;
        scoreText.text = score.ToString();
    }

    void OnApplicationQuit()
    {
        GameSaveManager.SaveGame();
    }
}
