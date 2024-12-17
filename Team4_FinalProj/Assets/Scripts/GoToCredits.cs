using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene switching
using System.Collections;

public class SceneCountdownTrigger : MonoBehaviour
{
    public string sceneToLoad; // Name of the scene to load
    private bool isCountingDown = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player entered the area
        if (other.CompareTag("Player") && !isCountingDown)
        {
            isCountingDown = true; // Prevent multiple triggers
            StartCoroutine(StartCountdown());
        }
    }

    private IEnumerator StartCountdown()
    {
        Debug.Log("Countdown started!");
        int countdown = 15;

        while (countdown > 0)
        {
            Debug.Log("Time remaining: " + countdown);
            yield return new WaitForSeconds(1f); // Wait for 1 second
            countdown--;
        }

        Debug.Log("Countdown complete. Loading scene: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad); // Load the specified scene
    }
}
