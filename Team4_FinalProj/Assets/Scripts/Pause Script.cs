using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PanelController : MonoBehaviour
{
    private const string introScene = "StartScene"; // Your intro scene name
    public GameObject panel; // Reference to the panel
    private bool isPanelActive = false;
    public AudioMixer mixer;
    public static float volumeLevel = 1.0f;
    private Slider sliderVolumeCtrl;

    void Awake(){
        panel.SetActive(true); // so slider can be set
        SetLevel (volumeLevel);
        GameObject sliderTemp = GameObject.FindWithTag("VolumeSlider");
        if (sliderTemp != null){
            sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
            sliderVolumeCtrl.value = volumeLevel;
        }
    }

    void Start(){
        panel.SetActive(false);
        isPanelActive = false;
    }

    public void SetLevel(float sliderValue){
        mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
        volumeLevel = sliderValue;
    }

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