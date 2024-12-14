using UnityEngine;
using UnityEngine.UI; // For UI Text
using TMPro; // If using TextMeshPro (optional)

public class HintDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uiText; // TextMeshPro support

    [TextArea]
    [SerializeField] private string hintMessage = "{DogName} seems to be tired.";

    private string userInput;

    void Start()
    {
        // Get user-provided input stored in PlayerPrefs
        userInput = PlayerPrefs.GetString("UserInput");

        // Replace placeholder with user input in the hint message
        string finalHint = hintMessage.Replace("{DogName}", userInput);

        // Display the hint on the UI Text component
        if (uiText != null)
        {
            uiText.text = finalHint;
        }
        else
        {
            Debug.LogWarning("UI Text component is not assigned.");
        }
    }
}
