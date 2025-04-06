using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public TMP_Text coinOwned;
    private void Awake()
    {
        int coinAmount = GameInit.CoinsOwned;
        coinOwned.text = coinAmount.ToString();
    }
}
