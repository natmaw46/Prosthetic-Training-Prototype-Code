using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class GameManager : MonoBehaviour
{
    public int difficultyLevel;
    public TMP_Text scoreText;

    private Blade blade;
    private FruitSpawner fruitSpawner;

    private int score;
    private int scoreConsecutive;
    private int scoreConsecutiveHeal = 50;
    private int life;

    public Image crossImage1;
    public Image crossImage2;
    public Image crossImage3;   
    public Sprite crossFilledSprite;
    public Sprite crossEmptySprite;
    public TMP_Text gameOverText;
    public TMP_Text ready;
    public TMP_Text set;
    public TMP_Text go;
    public Image collisionImage;
    public Image pauseMenu;
    public UnityEngine.UI.Button pauseBtn;
    public InGameAudioManager inGameAudioManager;
    public TMP_Text comboText;

    private void Awake()
    {
        GameInit.DifficultyLevel = difficultyLevel;

        blade = FindObjectOfType<Blade>();
        fruitSpawner = FindObjectOfType<FruitSpawner>();
    }

    private void Start()
    {
        gameOverText.enabled = false;
        comboText.enabled = false;
        ready.enabled = false;
        set.enabled = false;
        go.enabled = false;
        collisionImage.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        NewGame();   
    }

    private void NewGame()
    {        
        blade.enabled = true;
        fruitSpawner.enabled = true;
        pauseBtn.enabled = true;
        
        GameInit.Currentscore = 0;
        score = 0;
        scoreConsecutive = 0;
        scoreText.text = score.ToString();
        life = 3;

        StartCoroutine(ReadySetGo());
        ClearScene();
    }

    public IEnumerator ReadySetGo() 
    {
        ready.enabled = true;
        inGameAudioManager.PlayReadySetSound();

        yield return new WaitForSecondsRealtime(1f);

        ready.enabled = false;
        set.enabled = true;
        inGameAudioManager.PlayReadySetSound();

        yield return new WaitForSecondsRealtime(1f);

        set.enabled = false;
        go.enabled = true;
        inGameAudioManager.PlayReadySetSound();

        yield return new WaitForSecondsRealtime(1f);

        go.enabled = false;
        inGameAudioManager.PlayGoSound();
    }

    public void Pause()
    {
        pauseMenu.gameObject.SetActive(true);
        pauseBtn.enabled = false;
        blade.enabled = false;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.gameObject.SetActive(false);
        pauseBtn.enabled = true;
        blade.enabled = true;
        Time.timeScale = 1f;
    }

    private void ClearScene()
    {
        Fruits[] fruits = FindObjectsOfType<Fruits>();

        foreach (Fruits fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }
    }

    public void IncreaseScore()
    {
        inGameAudioManager.PlaySlicingSound();
        score++;
        scoreConsecutive++;
        scoreText.text = score.ToString();

        Heal();
    }

    public void Heal()
    {
        if (scoreConsecutive >= scoreConsecutiveHeal) {
            if (life == 2) 
            {
                scoreConsecutive = 0;
                life += 1;
                crossImage1.sprite = crossEmptySprite;  
            } 
            else if (life == 1) 
            {
                scoreConsecutive = 0;
                life += 1;
                crossImage2.sprite = crossEmptySprite;  
            } 
        }
    }

    public void Explode()
    {
        inGameAudioManager.PlayBombSound();

        blade.enabled = false;
        fruitSpawner.enabled = false;
        pauseBtn.enabled = false;

        StartCoroutine(ExplodeSequence());
    }

    private IEnumerator ExplodeSequence()
    {        
        Fruits[] fruits = FindObjectsOfType<Fruits>();

        foreach (Fruits fruit in fruits) {

            Vector3 randomDirection = new Vector3(
                Random.Range(-1f, 1f),  // Random X
                Random.Range(-1f, 1f),  // Random Y
                Random.Range(-1f, 1f)   // Random Z
            ).normalized;  // Normalized to keep magnitude = 1

            // Random Vector3 position (example within a 10x10x10 area)
            Vector3 randomPosition = new Vector3(
                Random.Range(-5f, 5f),  
                Random.Range(-5f, 5f),  
                Random.Range(-5f, 5f)
            );

            fruit.Slice(randomDirection, randomPosition, 50f, true);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb bomb in bombs) {

            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            // rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;  

            crossImage1.sprite = crossFilledSprite;  
            crossImage2.sprite = crossFilledSprite;  
            crossImage3.sprite = crossFilledSprite;
        }

        yield return new WaitForSecondsRealtime(1f);

        inGameAudioManager.PlayGameOverSound();
        gameOverText.enabled = true;

        GameInit.Currentscore = score;
        yield return new WaitForSecondsRealtime(3f);

        GameSaveManager.SaveGame();
        SceneManager.LoadScene(8);
    }

    public void MinusLife(Vector3 missPosition) 
    {
        scoreConsecutive = 0;

        if (life == 3) 
        {
            crossImage1.sprite = crossFilledSprite;  
        }
        else if (life == 2) 
        {
            crossImage2.sprite = crossFilledSprite;  
        } 
        else if (life == 1) 
        {
            crossImage3.sprite = crossFilledSprite;
            blade.enabled = false;
            fruitSpawner.enabled = false;

            StartCoroutine(GameOverSequence());
        } 
        else
        {
            return;
        } 

        StartCoroutine(ShowEffect(missPosition));
        life -= 1;  
    }

    private IEnumerator ShowEffect(Vector3 position)
    {
        inGameAudioManager.PlayFruitDropSound();
        collisionImage.transform.position = Camera.main.WorldToScreenPoint(position);
        collisionImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        collisionImage.gameObject.SetActive(false);
    }

    private IEnumerator GameOverSequence()
    {
        Fruits[] fruits = FindObjectsOfType<Fruits>();

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        pauseBtn.enabled = false;

        float elapsed = 0f;
        float duration = 1.5f;

        while ((bombs.Length != 0 || fruits.Length != 0) && elapsed < duration) {

            fruits = FindObjectsOfType<Fruits>();

            bombs = FindObjectsOfType<Bomb>();
            
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        inGameAudioManager.PlayGameOverSound();
        gameOverText.enabled = true;
        GameInit.Currentscore = score;

        yield return new WaitForSecondsRealtime(3f);
        
        GameSaveManager.SaveGame();
        SceneManager.LoadScene(8);
    }

    public void AddComboBonus(int slicedFruits)
    {
        int bonusPoints = slicedFruits * 2 + (slicedFruits - 3); // Example: 3 fruits = 5 extra, 4 = 10, etc.
        score += bonusPoints;
        scoreConsecutive += bonusPoints;

        Debug.Log("Combo! Sliced " + slicedFruits + " fruits! Bonus: " + bonusPoints + " points");

        comboText.text = "+" + slicedFruits + " Combo !!";

        scoreText.text = score.ToString();
        Heal();

        StartCoroutine(ComboEffect());
    }

    private IEnumerator ComboEffect()
    {
        Vector3 position = blade.transform.position;

        inGameAudioManager.PlayComboSound();
        comboText.transform.position = Camera.main.WorldToScreenPoint(position);
        comboText.enabled = true;

        yield return new WaitForSeconds(0.5f);

        comboText.enabled = false;
    }

    private void PlayComboSound()
    {
        // Implement this if you have a sound effect for combos
    }

    void OnApplicationQuit()
    {
        GameSaveManager.SaveGame();
    }
}
