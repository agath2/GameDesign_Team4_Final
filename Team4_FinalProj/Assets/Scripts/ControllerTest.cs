using UnityEngine;

public class ControllerTest : MonoBehaviour
{
    void Update()
    {
        // Log axes values
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Debug.Log($"Horizontal: {horizontal}, Vertical: {vertical}");

        // Log buttons (check joystick buttons)
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKey($"joystick button {i}"))
            {
                Debug.Log($"Joystick Button {i} pressed");
            }
        }
    }
}
