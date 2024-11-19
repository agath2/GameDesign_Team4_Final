using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1;  // The value of each coin, default to 1
    
    // This method is called when a collision with a trigger occurs
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Add to the player's coin count
            CoinManager.instance.AddCoins(coinValue);
            
            // Destroy the coin (make it disappear)
            Destroy(gameObject);
        }
    }
}