using UnityEngine;
using UnityEngine.SceneManagement; // Required to load scenes

public class EndSceneTrigger : MonoBehaviour
{
    [SerializeField] private string cutsceneSceneName; // Name of the next scene to load

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            LoadCutsceneScene();
        }
    }

    // Function to load the cutscene scene
    private void LoadCutsceneScene()
    {
        SceneManager.LoadScene(cutsceneSceneName);
    }
}
