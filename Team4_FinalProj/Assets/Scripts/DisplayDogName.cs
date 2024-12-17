using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayDogName : MonoBehaviour
{
    public TextMeshProUGUI dogNameText;

    void Start()
    {
        dogNameText.text = PlayerPrefs.GetString("UserInput");
    }
}
