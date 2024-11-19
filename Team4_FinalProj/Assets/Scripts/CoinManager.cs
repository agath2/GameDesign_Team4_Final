using UnityEngine;
using UnityEngine.UI;  // For UI components like Text

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;  // Singleton instance for easy access
    public int totalCoins = 0;          // The player's total coin count
    public Text coinText;               // Reference to the UI Text element to display coins

    private void Awake()
    {
        // Ensures there's only one instance of CoinManager (Singleton pattern)
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Method to add coins to the player's total
    public void AddCoins(int amount)
    {
        totalCoins += amount;         // Increase the coin count
        UpdateCoinUI();               // Update the coin count on UI
    }

    // Method to update the coin count on the UI
    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins x" + totalCoins.ToString();  // Display updated coin count
        }
    }
}
