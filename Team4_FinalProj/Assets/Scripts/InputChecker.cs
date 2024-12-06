using UnityEngine;
using TMPro;

public class InputChecker : MonoBehaviour
{
     public TextMeshProUGUI keyboardText;  // Text for keyboard input
     public TextMeshProUGUI controllerText; // Text for controller input

    private string lastInputMethod = "Keyboard"; // Tracks the last detected input method

    void Start()
    {
        // Initialize the display based on the input type
        UpdateTextDisplay();
    }

    void Update()
    {
        // Check for changes in the input method
        string currentInputMethod = DetectInputMethod();
        if (currentInputMethod != lastInputMethod)
        {
            lastInputMethod = currentInputMethod;
            UpdateTextDisplay();
        }
    }

    private string DetectInputMethod()
    {
        // Detect if a controller is connected
        foreach (string joystick in Input.GetJoystickNames())
        {
            if (!string.IsNullOrEmpty(joystick))
            {
                // If controller input is detected, return "Controller"
                if (Input.GetKey("joystick button0") || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    return "Controller";
                }
            }
        }

        // Fallback to keyboard input if no controller input is detected
        if (Input.anyKey)
        {
            return "Keyboard";
        }

        // Default to the last known input method if no input is detected
        return lastInputMethod;
    }

    private void UpdateTextDisplay()
    {
        // Update visibility of the text objects
        if (lastInputMethod == "Keyboard")
        {
            keyboardText.gameObject.SetActive(true);
            controllerText.gameObject.SetActive(false);
        }
        else if (lastInputMethod == "Controller")
        {
            keyboardText.gameObject.SetActive(false);
            controllerText.gameObject.SetActive(true);
        }
    }
}
