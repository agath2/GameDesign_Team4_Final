using UnityEngine;

public class WallToggleButton : MonoBehaviour
{
    public int channel = 0;
    public WallToggle wall; // Changed from a list to a single WallToggle
    public GameObject buttonUp;
    public GameObject buttonDown;
    public AudioSource ButtonPressAudio;
    public bool player_can_press = false;

    private bool pressed = false;
    private bool setup = true;

    void Start()
    {
        buttonUp.SetActive(true);
        buttonDown.SetActive(false);
    }

    void Update()
    {
        if (setup && wall != null)
        {
            // Set the wall's color based on the button's sprite color
            wall.SetColor(GetComponentInChildren<SpriteRenderer>().color);
            setup = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && (collision.gameObject.name == "Dog" || player_can_press))
        {
            Debug.Log("Button pushed");
            pressed = true;
            buttonUp.SetActive(false);
            buttonDown.SetActive(true);
            ButtonPressAudio.Play();

            // Toggle the state of the associated wall
            if (wall != null)
            {
                wall.ToggleState();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && (collision.gameObject.name == "Dog" || player_can_press))
        {
            Debug.Log("Button unpushed");
            pressed = false;
            buttonUp.SetActive(true);
            buttonDown.SetActive(false);
            ButtonPressAudio.Play();
        }
    }
}
