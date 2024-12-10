using UnityEngine;
using TMPro;

public class InputChecker : MonoBehaviour
{
    public TextMeshProUGUI keyboardText;   // Text for keyboard input
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
        // Check if any joystick is connected
        foreach (string joystick in Input.GetJoystickNames())
        {
            if (!string.IsNullOrEmpty(joystick))
            {
                // Check for any controller input (axis movement or button press)
                if (Input.GetAxis("Horizontal") != 0 ||
                    Input.GetAxis("Vertical") != 0 ||
                    Input.anyKeyDown) // Detect any button press
                {
                    return "Controller";
                }
            }
        }

        // Default to the last known input method if no input is detected
        return lastInputMethod;
    }

    private void UpdateTextDisplay()
    {
        // Update visibility of the text objects based on the input method
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
