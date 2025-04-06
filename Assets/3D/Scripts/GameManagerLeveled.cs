using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class GameManagerLeveled : MonoBehaviour
{
    public TMP_Text timerText;

    private Blade blade;
    private FruitSpawner fruitSpawner;
    
    public float timer = 60f;
    private int life;

    public Image crossImage1;
    public Image crossImage2;
    public Image crossImage3;   
    public Sprite crossFilledSprite;
    public Sprite crossEmptySprite;
    public TMP_Text gameOverText;
    public TMP_Text gameClearedText;
    public TMP_Text ready;
    public TMP_Text set;
    public TMP_Text go;
    public Image collisionImage;
    public Image pauseMenu;
    public UnityEngine.UI.Button pauseBtn;
    public InGameAudioManager inGameAudioManager;

    private bool isGameOver = false;

    private void Awake()
    {
        GameInit.DifficultyLevel = GameInit.SelectedLevel;
        blade = FindObjectOfType<Blade>();
        fruitSpawner = FindObjectOfType<FruitSpawner>();
    }

    private void Start()
    {
        gameOverText.enabled = false;
        gameClearedText.enabled = false;
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
        timerText.text = timer.ToString();
        life = 3;

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

        inGameAudioManager.PlayGoSound();
        go.enabled = false;

        while (timer > 0 && !isGameOver)
        {
            timerText.text = Mathf.Ceil(timer).ToString();
            timer -= Time.deltaTime;
            yield return null;
        }

        if (!isGameOver) {
            timerText.text = "0";
            fruitSpawner.enabled = false;

            Fruits[] fruits = FindObjectsOfType<Fruits>();

            Bomb[] bombs = FindObjectsOfType<Bomb>();

            while (bombs.Length != 0 || fruits.Length != 0) {

                fruits = FindObjectsOfType<Fruits>();

                bombs = FindObjectsOfType<Bomb>();

                yield return null;
            }
            
            if (!isGameOver) {
                pauseBtn.enabled = false;

                blade.enabled = false;

                int currentLevel = GameInit.SelectedLevel;
                if (GameInit.HighestLevel < currentLevel) {
                    GameInit.HighestLevel = currentLevel;
                }

                inGameAudioManager.PlayGameFinishedSound();
                gameClearedText.enabled = true;

                yield return new WaitForSeconds(3f);
                
                GameSaveManager.SaveGame();
                SceneManager.LoadScene(9);
            }
        }
    }

    public void PlaySliceSound() 
    {
        inGameAudioManager.PlaySlicingSound();
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

    public void Explode()
    {
        inGameAudioManager.PlayBombSound();
        isGameOver = true;

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

        yield return new WaitForSeconds(1f);

        gameOverText.enabled = true;
        inGameAudioManager.PlayGameOverSound();

        yield return new WaitForSeconds(3f);

        GameSaveManager.SaveGame();
        SceneManager.LoadScene(10);
    }

    public void MinusLife(Vector3 missPosition) 
    {
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
        
        inGameAudioManager.PlayFruitDropSound();
        StartCoroutine(ShowEffect(missPosition));
        life -= 1;  
    }

    private IEnumerator ShowEffect(Vector3 position)
    {
        collisionImage.transform.position = Camera.main.WorldToScreenPoint(position);
        collisionImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        collisionImage.gameObject.SetActive(false);
    }

    private IEnumerator GameOverSequence()
    {
        isGameOver = true;

        StartCoroutine(FruitsBombsStillLeft());

        gameOverText.enabled = true;
        inGameAudioManager.PlayGameOverSound();

        yield return new WaitForSeconds(3f);
        
        GameSaveManager.SaveGame();
        SceneManager.LoadScene(10);
    }

    private IEnumerator FruitsBombsStillLeft() 
    {
        Fruits[] fruits = FindObjectsOfType<Fruits>();

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        float elapsed = 0f;
        float duration = 1.5f;

        while ((bombs.Length != 0 || fruits.Length != 0) && elapsed < duration) {

            fruits = FindObjectsOfType<Fruits>();

            bombs = FindObjectsOfType<Bomb>();
            
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
        
        pauseBtn.enabled = false;
    }

    void OnApplicationQuit()
    {
        GameSaveManager.SaveGame();
    }
}
