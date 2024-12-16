using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;  // Singleton instance for easy access

    // Player's total coin count (persistent across levels)
    public int totalCoins = 0;

    // Coins collected in the current level (reset when level is completed or player dies)
    public int levelCoins = 0;

    // Reference to the UI text element to display coins
    public TextMeshProUGUI coinText;

    // Audio source for coin pickup sound
    public AudioSource pickupSound;

    private void Awake()
    {
        // Singleton pattern: Ensure there's only one instance of CoinManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // This will persist across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy any duplicate instances
        }
    }

    // Method to add coins to the current level
    public void AddCoins(int amount)
    {
        levelCoins += amount;  // Add the coin amount to levelCoins
        UpdateCoinUI();        // Update the coin count displayed on the UI
        pickupSound.Play();    // Play the pickup sound when a coin is collected
    }

    // Method to reset the coins collected during the current level (called on death)
    public void PlayerDied()
    {
        levelCoins = 0;  // Reset the levelCoins to 0, but DO NOT reset totalCoins
        UpdateCoinUI();  // Update the UI to reflect the reset value
    }

    // Method to add the coins from the current level to the total (called at level completion)
    public void EndLevel()
    {
        totalCoins += levelCoins;  // Add the current level's coins to totalCoins
        levelCoins = 0;             // Reset the levelCoins after completing the level
        UpdateCoinUI();             // Update the UI to show the new total
    }

    // Method to update the coin UI
    private void UpdateCoinUI()
    {
        // Display the total coins, plus coins collected during the current level
        coinText.text = "x" + (totalCoins + levelCoins).ToString();
    }

    // Optional: Call this method when a new level starts to reset the level coins
    public void StartNewLevel()
    {
        levelCoins = 0;  // Reset level coins at the start of a new level
        UpdateCoinUI();  // Update UI to show the reset value
    }
}
