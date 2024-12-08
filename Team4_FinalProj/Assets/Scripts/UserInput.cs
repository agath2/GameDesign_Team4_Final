using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInput : MonoBehaviour
{
    public TMP_InputField userInputField;
    private string userInput;

    void Start()
    {
        if (userInputField != null)
        {
            // Add listener to handle input submission
            userInputField.onEndEdit.AddListener(SubmitInput);
        }
    }

    void SubmitInput(string input)
    {
        userInput = input;
        Debug.Log("User Input: " + userInput);
        PlayerPrefs.SetString("UserInput", userInput);
    }
}
