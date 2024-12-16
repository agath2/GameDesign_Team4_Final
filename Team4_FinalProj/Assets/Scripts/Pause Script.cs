using UnityEngine;
using UnityEngine.SceneManagement;  // To manage scene loading
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuPanel;  // Reference to the pause menu panel
    private bool isPaused = false;     // Track if the game is paused
    public AudioMixer mixer;
    public static float volumeLevel = 1.0f;
    private Slider sliderVolumeCtrl;

    void Awake(){
        pauseMenuPanel.SetActive(true); // so slider can be set
        SetLevel (volumeLevel);
        GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
        if (sliderTemp != null){
            sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
            sliderVolumeCtrl.value = volumeLevel;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Make sure the pause menu is hidden at the start of the game
        pauseMenuPanel.SetActive(false);
        isPaused = false;
    }

    void Update(){
        if (Input.GetButtonDown("Cancel")){
            TogglePauseMenu();
        }
    }

    // Set volume level
    public void SetLevel(float sliderValue){
        mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
        volumeLevel = sliderValue;
    }

    // Function to toggle the pause menu
    public void TogglePauseMenu()
    {
        if (isPaused)
        {
            ResumeGame();  // If game is paused, resume
        }
        else
        {
            PauseGame();  // If game is running, pause
        }
    }

    // Pause the game and show the pause menu
    public void PauseGame()
    {
        isPaused = true;
        pauseMenuPanel.SetActive(true);  // Show the pause menu
        Time.timeScale = 0f;  // Pause the game (stops all game physics and time-based actions)
    }

    // Resume the game and hide the pause menu
    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuPanel.SetActive(false);  // Hide the pause menu
        Time.timeScale = 1f;  // Resume the game (restore normal time flow)
    }

    // Restart the game by reloading the current scene
    public void RestartGame()
    {
        Time.timeScale = 1f;  // Ensure the game time is resumed before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the current scene
        CoinManager.instance.StartNewLevel();
    }

    // Function to quit the game (optional)
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;  // Stop play mode in editor
        #else
            Application.Quit();  // Quit the game when built
        #endif
    }
}
