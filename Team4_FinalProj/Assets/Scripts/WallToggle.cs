using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class WallToggle : MonoBehaviour
{
    public int channel = 0;
    public bool startOn = true;
    public bool toggledOn = true;
    public GameObject wallUp;
    public GameObject wallDown;

    void Start()
    {
        wallUp.SetActive(true);
        wallDown.SetActive(false);

        GameObject[] buttonArr = GameObject.FindGameObjectsWithTag("WallToggleButton");
        foreach (GameObject button in buttonArr)
        {
            if (button.GetComponent<WallToggleButton>().channel == channel)
            {
                button.GetComponent<WallToggleButton>().wallList.Add(this);
            }
        }

        if (!startOn) { ToggleState(); }

   
    }

    public void ToggleState()
    {
        toggledOn = !toggledOn;
        if (toggledOn)
        {
            wallUp.SetActive(true);
            wallDown.SetActive(false);
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            wallUp.SetActive(false);
            wallDown.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
            Debug.Log("Wall gone");
        }
    }

    public void SetColor(Color col)
    {
        GetComponentInChildren<SpriteRenderer>().color = col;
    }
}