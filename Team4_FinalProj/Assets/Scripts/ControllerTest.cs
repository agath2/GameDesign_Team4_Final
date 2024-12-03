using UnityEngine;

public class ControllerTest : MonoBehaviour
{
    void Update()
    {
        // Check buttons
        for (int i = 0; i < 20; i++)
        {
            string buttonName = "joystick button " + i;

            if (Input.GetKeyDown(buttonName))
            {
                Debug.Log($"Button {i} pressed");
            }

            if (Input.GetKeyUp(buttonName))
            {
                Debug.Log($"Button {i} released");
            }
        }

        

        // Debug all button states
        DebugAllInputs();
    }

    void DebugAllInputs()
    {
        for (KeyCode key = KeyCode.JoystickButton0; key <= KeyCode.JoystickButton19; key++)
        {
            if (Input.GetKeyDown(key))
            {
                Debug.Log($"{key} pressed");
            }
        }
    }
}
