using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1;  // The value of each coin, default to 1  
    // public AudioSource pickupSound;  // The sound to play upon pickup
    public bool isPickedUp = false;
    
    // This method is called when a collision with a trigger occurs
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPickedUp)
        {
            isPickedUp = true;
            Debug.Log("Coin picked up");
            // Add to the player's coin count
            CoinManager.instance.AddCoins(coinValue);
            GetComponent<Collider2D>().enabled = false;
            
            // Play the pickup sound at the coin's position
                // pickupSound.Play();
            // AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            // Destroy the coin (make it disappear)
            Destroy(gameObject);
        }
    }
}