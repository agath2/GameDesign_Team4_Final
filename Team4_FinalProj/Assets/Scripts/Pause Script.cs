using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class PanelController : MonoBehaviour
{
    private const string introScene = "StartScene"; // Your intro scene name
    public GameObject panel; // Reference to the panel
    private bool isPanelActive = false;

    public void TogglePanel()
    {
        isPanelActive = !isPanelActive;
        panel.SetActive(isPanelActive);

        // Pause or resume the game time based on panel state
        Time.timeScale = isPanelActive ? 0 : 1;
    }

    public void ResumeGame()
    {
        // Hide the panel and resume time
        isPanelActive = false;
        panel.SetActive(false);
        Time.timeScale = 1; // Resume the game
    }
    // Method to restart the game
    public void GoBackToStart()
    {
        SceneManager.LoadScene(introScene); // Load the intro scene
    }

    // Method to quit the game
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}