using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeveledEnterScreenManager : MonoBehaviour
{
    public TMP_Text highestLevelText;
    public TMP_Text selectedLevelText;
    public TMP_Dropdown dropdown;
    public int fontSize;
    public int dropdownHeight;


    private void Awake() {

        int highestLevel = GameInit.HighestLevel;
        
        if (highestLevel == 0) {
            
            highestLevelText.text = "Highest Level:  1" ;
            selectedLevelText.text = "1";
        } 
        else 
        {
            highestLevelText.text = "Highest Level:  " + highestLevel.ToString();
            selectedLevelText.text = (highestLevel + 1).ToString();
        }

        GenerateDropdownOptions(highestLevel + 1);
        CustomizeDropdown();
    }

    private void GenerateDropdownOptions(int levelOptions)
    {
        dropdown.ClearOptions();

        for (int i = levelOptions; i >= 1; i--)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData("Level " + i));
        }

        dropdown.value = 0;
        dropdown.RefreshShownValue();
    }

    private void CustomizeDropdown()
    {
        // var template = dropdown.template;
        
        // TMP_Text itemText = template.GetComponentInChildren<TMP_Text>();
        // itemText.fontSize = fontSize;
        
        // RectTransform item = template.Find("Viewport/Content/Item")?.GetComponent<RectTransform>();
        // item.sizeDelta = new Vector2(item.sizeDelta.x, fontSize + 10);
        
        // // RectTransform check = template.Find("Viewport/Content/Item/Item Checkmark")?.GetComponent<RectTransform>();
        // // item.sizeDelta = new Vector2(fontSize, fontSize);
        var template = dropdown.template;
        
        // Modify item font size
        TMP_Text itemText = template.GetComponentInChildren<TMP_Text>();
        itemText.fontSize = fontSize;

        // Adjust the item size based on font size
        RectTransform item = template.Find("Viewport/Content/Item")?.GetComponent<RectTransform>();
        if (item != null)
        {
            item.sizeDelta = new Vector2(item.sizeDelta.x, fontSize + 20); // Adjust height based on font size
        }

        // Now make the dropdown content scrollable
        RectTransform contentRect = template.Find("Viewport/Content").GetComponent<RectTransform>();
        ScrollRect scrollRect = template.GetComponentInParent<ScrollRect>();
        Debug.Log(scrollRect);
        if (scrollRect != null) 
        {
            scrollRect.scrollSensitivity = 20f;
        } 
        if (contentRect != null)
        {
            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, contentRect.sizeDelta.y + 20);
        }
    }

    public void OnLevelSelected()
    {
        int selectedLevelCalc = GameInit.HighestLevel + 1 - dropdown.value;
        GameInit.SelectedLevel = selectedLevelCalc;
    }

    void OnApplicationQuit()
    {
        GameSaveManager.SaveGame();
    }
}
