using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopManager : MonoBehaviour
{
    public Button BGStyle1;
    public Button BGStyle2;
    public Button BGStyle3;
    public Button BGStyle4;
    public Button BGStyle5;
    public Button BGStyle6;
    public Button BGStyle7;
    public Button BGStyle8;
    public Button BGStyle9;
    
    public TMP_Text BGText1;
    public TMP_Text BGText2;
    public TMP_Text BGText3;
    public TMP_Text BGText4;
    public TMP_Text BGText5;
    public TMP_Text BGText6;
    public TMP_Text BGText7;
    public TMP_Text BGText8;
    public TMP_Text BGText9;

    public Button IconStyle1;
    public Button IconStyle2;
    public Button IconStyle3;
    public Button IconStyle4;
    public Button IconStyle5;
    public Button IconStyle6;
    public Button IconStyle7;
    public Button IconStyle8;
    
    public TMP_Text IconText1;
    public TMP_Text IconText2;
    public TMP_Text IconText3;
    public TMP_Text IconText4;
    public TMP_Text IconText5;
    public TMP_Text IconText6;
    public TMP_Text IconText7;
    public TMP_Text IconText8;
    public Button ConfirmBtn;
    public Button CancelBtn;

    public Image confirmBuy;
    public TMP_Text warnText;
    public TMP_Text coinOwned;


    private Dictionary<int, Button> buttonDictionary;
    private Dictionary<int, TMP_Text> textDictionary;
    private Dictionary<int, Button> buttonIconDictionary;
    private Dictionary<int, TMP_Text> textIconDictionary;

    private int bgIDSelectedToBuy;
    private int iconIDSelectedToBuy;
    private int whatToBuy;

    private void Awake()
    {
        confirmBuy.gameObject.SetActive(false);
        buttonDictionary = new Dictionary<int, Button>
        {
            { 1, BGStyle1 },
            { 2, BGStyle2 },
            { 3, BGStyle3 },
            { 4, BGStyle4 },
            { 5, BGStyle5 },
            { 6, BGStyle6 },
            { 7, BGStyle7 },
            { 8, BGStyle8 },
            { 9, BGStyle9 }
        };

        textDictionary = new Dictionary<int, TMP_Text>
        {
            { 1, BGText1 },
            { 2, BGText2 },
            { 3, BGText3 },
            { 4, BGText4 },
            { 5, BGText5 },
            { 6, BGText6 },
            { 7, BGText7 },
            { 8, BGText8 },
            { 9, BGText9 }
        };

        buttonIconDictionary = new Dictionary<int, Button>
        {
            { 1, IconStyle1 },
            { 2, IconStyle2 },
            { 3, IconStyle3 },
            { 4, IconStyle4 },
            { 5, IconStyle5 },
            { 6, IconStyle6 },
            { 7, IconStyle7 },
            { 8, IconStyle8 }
        };

        textIconDictionary = new Dictionary<int, TMP_Text>
        {
            { 1, IconText1 },
            { 2, IconText2 },
            { 3, IconText3 },
            { 4, IconText4 },
            { 5, IconText5 },
            { 6, IconText6 },
            { 7, IconText7 },
            { 8, IconText8 }
        };
    
        UpdateButtons();
        UpdateIconButtons();
    }

    private void UpdateButtons()
    {
        foreach (var kvp in buttonDictionary)
        {
            int bgID = kvp.Key;
            Button button = kvp.Value;
            TMP_Text buttonText = textDictionary[bgID];

            button.onClick.RemoveAllListeners();

            if (GameInit.BackgroundSelected == bgID)
            {
                buttonText.text = "Selected";
                buttonText.color = Color.green;
                button.interactable = false;
            }
            else if (GameInit.BackgroundOwned.Contains(bgID))
            {
                buttonText.text = "Select";
                buttonText.color = Color.yellow;
                button.interactable = true;
                button.onClick.AddListener(() => SelectBackground(bgID));
            }
            else
            {
                buttonText.text = "Buy";
                buttonText.color = Color.red;
                button.interactable = true;
                button.onClick.AddListener(() => BuyBackground(bgID));
            }
        }
    }

    private void UpdateIconButtons()
    {
        foreach (var kvp in buttonIconDictionary)
        {
            int iconID = kvp.Key;
            Button button = kvp.Value;
            TMP_Text buttonText = textIconDictionary[iconID];

            button.onClick.RemoveAllListeners();

            if (GameInit.IconSelected == iconID)
            {
                buttonText.text = "Selected";
                buttonText.color = Color.green;
                button.interactable = false;
            }
            else if (GameInit.IconOwned.Contains(iconID))
            {
                buttonText.text = "Select";
                buttonText.color = Color.yellow;
                button.interactable = true;
                button.onClick.AddListener(() => SelectIcon(iconID));
            }
            else
            {
                buttonText.text = "Buy";
                buttonText.color = Color.red;
                button.interactable = true;
                button.onClick.AddListener(() => BuyIcon(iconID));
            }
        }
    }

    private void SelectIcon(int iconID)
    {
        GameInit.IconSelected = iconID;
        UpdateIconButtons();
    }

    private void BuyIcon(int iconID)
    {
        whatToBuy = 2;
        iconIDSelectedToBuy = iconID;
        warnText.text = "Are you sure you want to use 250 coins to purchase this item?";
        confirmBuy.gameObject.SetActive(true);
    }

    private void SelectBackground(int bgID)
    {
        GameInit.BackgroundSelected = bgID;
        UpdateButtons();
    }

    private void BuyBackground(int bgID)
    {
        whatToBuy = 1;
        bgIDSelectedToBuy = bgID;
        warnText.text = "Are you sure you want to use 500 coins to purchase this item?";
        confirmBuy.gameObject.SetActive(true);
    }

    public void CancelBuyBG()
    {
        confirmBuy.gameObject.SetActive(false);
    }

    public void ConfirmBuy()
    {
        ConfirmBtn.interactable = false;
        CancelBtn.interactable = false;
        if (whatToBuy == 1)
        {
            if (GameInit.CoinsOwned < 500) 
            {
                warnText.text = "Insufficent Coins!!";
                warnText.fontSize = 84;

                StartCoroutine(ResetWarningText());

                return;
                
            }
            GameInit.CoinsOwned = GameInit.CoinsOwned - 500;
            GameInit.BackgroundOwned.Add(bgIDSelectedToBuy);
            UpdateButtons();
            
            warnText.text = "Purchase Success!!";
            warnText.fontSize = 84;

            StartCoroutine(ResetWarningText());
            return;
        }
        else if (whatToBuy == 2)
        {
            if (GameInit.CoinsOwned < 250) 
            {
                warnText.text = "Insufficent Coins!!";
                warnText.fontSize = 84;

                StartCoroutine(ResetWarningText());

                return;
                
            }
            GameInit.CoinsOwned = GameInit.CoinsOwned - 250;
            GameInit.IconOwned.Add(iconIDSelectedToBuy);
            UpdateIconButtons();
            
            warnText.text = "Purchase Success!!";
            warnText.fontSize = 84;

            StartCoroutine(ResetWarningText());
            return;
        }        
    }

    private IEnumerator ResetWarningText()
    {
        yield return new WaitForSeconds(2f);
        confirmBuy.gameObject.SetActive(false);
        warnText.fontSize = 48;
        CoinsDisplay();
        ConfirmBtn.interactable = true;
        CancelBtn.interactable = true;
    }

    private void CoinsDisplay()
    {
        int coinAmount = GameInit.CoinsOwned;
        coinOwned.text = coinAmount.ToString();
    }

    void OnApplicationQuit()
    {
        GameSaveManager.SaveGame();
    }
}