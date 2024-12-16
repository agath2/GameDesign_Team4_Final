using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnTrigger : MonoBehaviour
{
    private Transform playerPos;
    private string sceneName;

    void Start()
    {
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
        sceneName = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        if (transform.position.y >= playerPos.position.y)
        {
            // Call PlayerDied to reset level coins before reloading the scene
            if (CoinManager.instance != null)
            {
                CoinManager.instance.PlayerDied();  // Reset level coins
            }

            // Reload the scene to respawn the player
            SceneManager.LoadScene(sceneName);
            Debug.Log("Player fell below the screen. Respawning...");
        }
    }
}
