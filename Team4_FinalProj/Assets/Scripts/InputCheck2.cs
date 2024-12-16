using UnityEngine;
using TMPro;

public class InputCheck2 : MonoBehaviour
{
    public GameObject keyboardControls;   // Text for keyboard input
    public GameObject controllerControls; // Text for controller input

    private string lastInputMethod = "Keyboard"; // Tracks the last detected input method

    void Start()
    {
        // Initialize the display based on the input type
        UpdateDisplay();
    }

    void Update()
    {
        // Check for changes in the input method
        string currentInputMethod = DetectInputMethod();
        if (currentInputMethod != lastInputMethod)
        {
            lastInputMethod = currentInputMethod;
            UpdateDisplay();
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

    private void UpdateDisplay()
    {
        // Update visibility of the text objects based on the input method
        if (lastInputMethod == "Keyboard")
        {
            keyboardControls.SetActive(true);
            controllerControls.SetActive(false);
        }
        else if (lastInputMethod == "Controller")
        {
            keyboardControls.SetActive(false);
            controllerControls.SetActive(true);
        }
    }
}
