using UnityEngine;
using TMPro;

public class SelectBoxManager : MonoBehaviour
{
    public TMP_Text selectedLevel;

    public void HandleInputData(int value) 
    {
        int selectedLevelCalc = GameInit.HighestLevel + 1 - value;

        selectedLevel.text = selectedLevelCalc.ToString();
    }
}
