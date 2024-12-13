using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1;  // The value of each coin, default to 1
    public AudioClip pickupSound;  // The sound to play upon pickup
    
    // This method is called when a collision with a trigger occurs
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Add to the player's coin count
            CoinManager.instance.AddCoins(coinValue);
            
            // Play the pickup sound at the coin's position
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            // Destroy the coin (make it disappear)
            Destroy(gameObject);
        }
    }
}