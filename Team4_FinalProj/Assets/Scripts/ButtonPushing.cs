using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class WallToggleButton : MonoBehaviour
{
    public int channel = 0;
    public List<WallToggle> wallList = new List<WallToggle>();
    public GameObject buttonUp;
    public GameObject buttonDown;

    private bool pressed = false;
    private bool setup = true;

    void Start()
    {
        buttonUp.SetActive(true);
        buttonDown.SetActive(false);

    }

    void Update()
    {
        if (setup)
        {
            foreach (WallToggle wall in wallList)
            {
                wall.SetColor(GetComponentInChildren<SpriteRenderer>().color);
            }
            setup = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Button pushed");
            pressed = true;
            buttonUp.SetActive(false);
            buttonDown.SetActive(true);
            foreach (WallToggle wall in wallList)
            {
                wall.ToggleState();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Button un pushed");
            pressed = false;
            buttonUp.SetActive(true);
            buttonDown.SetActive(false);
        }
    }
}
