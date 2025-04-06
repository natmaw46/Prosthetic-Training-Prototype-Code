using UnityEngine;
using UnityEngine.UI;

public class EnterScreen : MonoBehaviour
{
    public Image trainScreen1;
    public Image trainScreen2;
    public Image sliceScreen1;
    public Image sliceScreen2;

    private void Awake()
    {
        GameSaveManager.LoadGame();
        trainScreen1.gameObject.SetActive(false);
        trainScreen2.gameObject.SetActive(false);
        sliceScreen1.gameObject.SetActive(false);
        sliceScreen2.gameObject.SetActive(false);
    }

    public void Page1Open() 
    {
        trainScreen1.gameObject.SetActive(true);
        trainScreen2.gameObject.SetActive(false);
    }
    public void Page1Close() 
    {
        trainScreen1.gameObject.SetActive(false);
    }

    public void Page2Open() 
    {
        trainScreen1.gameObject.SetActive(false);
        trainScreen2.gameObject.SetActive(true);
        sliceScreen1.gameObject.SetActive(false);
    }
    public void Page3Open() 
    {
        trainScreen2.gameObject.SetActive(false);
        sliceScreen1.gameObject.SetActive(true);
        sliceScreen2.gameObject.SetActive(false);
    }
    public void Page4Open() 
    {
        sliceScreen1.gameObject.SetActive(false);
        sliceScreen2.gameObject.SetActive(true);
    }
    public void Page4Close() 
    {
        sliceScreen2.gameObject.SetActive(false);
    }

    void OnApplicationQuit()
    {
        GameSaveManager.SaveGame();
    }
}
