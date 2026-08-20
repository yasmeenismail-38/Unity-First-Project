using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public int coins = 0;
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        Instance = this;
    }

    public void AddCoin()
    {
        coins++;
        coinText.text = "Coins: " + coins;
    }
}