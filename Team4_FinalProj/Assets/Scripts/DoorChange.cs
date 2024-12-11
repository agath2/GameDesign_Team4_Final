using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorChange : MonoBehaviour
{
     public GameObject doorOpened;
     public GameObject doorClosed;
     public GameObject doorLocked;
    // public GameObject msgNeedKey;
    public string NextLevel = "StartMen";
    public AudioClip DoorOpen;
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
        // Check if the object colliding with the door is the "Fetch" (key)
        if (other.gameObject.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(other.gameObject);  // Destroy the key (since it's collected)
            Debug.Log("Key collected!");
        }
        else if (other.gameObject.CompareTag("Player"))  // Assuming the player has the tag "Player"
        {
            if (hasKey && isLocked)  // If the player has the key and the door is locked
            {
              //  GetComponent<AudioSource>().Play();
                // doorLocked.SetActive(false);
                // doorOpened.SetActive(true);
                // isLocked = false;
                // SceneManager.LoadScene(NextLevel);
                StartCoroutine(OpenDoorAfterDelay());
                Debug.Log("Door opened!");
            }
            else if (!hasKey)
            {
 
                Debug.Log("You need a key to enter.");
            }
            // else
            // {
            //     doorLocked.SetActive(false);
            //     doorOpened.SetActive(true);
            //     SceneManager.LoadScene(NextLevel);
            //     Debug.Log("Door is already open.");
            // }
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))  // Assuming the player has the tag "Player"
        {
            if (hasKey && isLocked)  // If the player has the key and the door is locked
            {
              //  GetComponent<AudioSource>().Play();
                // doorLocked.SetActive(false);
                // doorOpened.SetActive(true);
                // isLocked = false;
                // SceneManager.LoadScene(NextLevel);
                StartCoroutine(OpenDoorAfterDelay());
                Debug.Log("Door opened!");
            }
            else if (!hasKey)
            {
                Debug.Log("You need a key to enter.");
            }
            // else
            // {
            //     doorLocked.SetActive(false);
            //     doorOpened.SetActive(true);
            //     SceneManager.LoadScene(NextLevel);
            //     Debug.Log("Door is already open.");
            // }
        }
    }

    IEnumerator OpenDoorAfterDelay()
    {
        doorLocked.SetActive(false);
        doorClosed.SetActive(true);
        yield return new WaitForSeconds(1f);
        isLocked = false;
        doorClosed.SetActive(false);
        doorOpened.SetActive(true);
        SceneManager.LoadScene(NextLevel);
        Debug.Log("Door opened!");
    }

    // Optional: Hide message when player leaves the are
    }
