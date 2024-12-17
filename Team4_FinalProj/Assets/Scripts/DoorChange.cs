using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorChange : MonoBehaviour
{
    public GameObject doorOpened;
    public GameObject doorClosed;
    public GameObject doorLocked;
    public string NextLevel = "StartMen"; // Adjust to your next level's name
    public AudioSource DoorOpen;
    public AudioSource DoorUnlockedOpen;
    public bool isLocked = true;

    // Track if the key has been picked up
    public bool hasKey = false;

    void Start()
    {
        doorOpened.SetActive(false);
        doorClosed.SetActive(!isLocked);
        doorLocked.SetActive(isLocked);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding with the door is the "Key"
        if (other.gameObject.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(other.gameObject);  // Destroy the key (since it's collected)
            Debug.Log("Key collected!");
            // StartCoroutine(OpenDoorAfterDelay());
        }

        if (other.gameObject.CompareTag("Player"))  // Assuming the player has the tag "Player"
        {
            if (hasKey && isLocked)  // If the player has the key and the door is locked
            {
                StartCoroutine(OpenDoorAfterDelay());
                Debug.Log("Door opened!");
            }
            else if (!hasKey)
            {
                Debug.Log("You need a key to enter.");
            }
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))  // Assuming the player has the tag "Player"
        {
            if (hasKey && isLocked)  // If the player has the key and the door is locked
            {
                StartCoroutine(OpenDoorAfterDelay());
                Debug.Log("trigger stay hasKey && isLocked");
            }
            else if (!isLocked)
            {
                StartCoroutine(OpenDoorAfterDelay());
                Debug.Log("Door's not locked!");
            }
            else if (!hasKey)
            {
                Debug.Log("You need a key to enter.");
            }
        }
    }

    IEnumerator OpenDoorAfterDelay()
    {
        Rigidbody2D playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        playerRB.constraints = RigidbodyConstraints2D.FreezePosition;
        // Only allow door animation and sound if neither door sounds are already playing
        if (!DoorOpen.isPlaying && !DoorUnlockedOpen.isPlaying)
        {
            // If the door is locked
            if (isLocked)
            {
                doorLocked.SetActive(false);
                doorClosed.SetActive(true);
                DoorOpen.Play();
                yield return new WaitForSeconds(2f);  // Wait for the door opening sound
            }
            else
            {
                DoorUnlockedOpen.Play();
            }

            doorClosed.SetActive(false);
            doorOpened.SetActive(true);

            yield return new WaitForSeconds(2f);  // Wait for the door to fully open

            // Add the level coins to the total coins in the CoinManager
            CoinManager.instance.EndLevel();  // Save current level coins to total coins

            // Reset levelCoins for the next level (it's already done by EndLevel())
            Debug.Log("Door opened, saving coins and transitioning...");

            isLocked = false;  // Unlock the door after it opens
            SceneManager.LoadScene(NextLevel);  // Transition to the next level
            Debug.Log("Level transition initiated!");
        }
    }
}
