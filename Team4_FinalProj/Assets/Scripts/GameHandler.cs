using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    private const string levelScene = "Level1"; // Your single level scene name
    private const string introScene = "StartScene"; // Your intro scene name
    private const string howToPlayScene = "HowToPlay"; // Your how-to-play scene name

    void Start()
    {
        // Optional: Initialize any settings or state if needed
    }

    // Method to start the game from the intro scene
    public void StartGame()
    {
        SceneManager.LoadScene(introScene); // Load the intro scene
    }

    // Method to transition to the how-to-play scene
    public void GoToHowToPlay()
    {
        SceneManager.LoadScene(howToPlayScene); // Load the how-to-play scene
    }

    // Method to go back to the start scene
    public void GoBackToStart()
    {
        SceneManager.LoadScene(introScene); // Load the intro scene
    }

    // Method to transition from the intro to the level
    public void StartLevel()
    {
        SceneManager.LoadScene(levelScene); // Load the single level scene
    }

    // Method to restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene(levelScene); // Reload the level scene
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
