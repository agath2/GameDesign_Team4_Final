using UnityEngine;
using UnityEngine.UI; // For accessing legacy UI elements

public class ButtonSelector : MonoBehaviour
{
    public Button legacyUIButton; // Reference to the legacy UI Button component

    void Update()
    {
        // Check if "joystick button 0" (A button on most controllers) is pressed
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            if (legacyUIButton != null)
            {
                // Simulate the button click by invoking the OnClick event
                legacyUIButton.onClick.Invoke();
                Debug.Log($"{legacyUIButton.name} clicked via controller input!");
            }
            else
            {
                Debug.LogError("Legacy UI Button is not assigned in the Inspector!");
            }
        }
    }
}
