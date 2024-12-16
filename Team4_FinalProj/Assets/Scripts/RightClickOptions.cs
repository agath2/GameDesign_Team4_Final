using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class RightClickOptions : MonoBehaviour
{
    public GameObject optionMenu;
    public Button goHereButton;
    public Button followButton;
    public Button stayButton;
    public Button fetchButton;
    private Vector2 mousePosition;
    public DogMovement dog;
    private GameObject currentFetchable;
    public float fetchableRange = 4f;
    public bool useSelector = false;
    public AudioSource DogBarkCommandAudio;
    private EventSystem eventSystem;       // Reference to the Event System

    public bool isMenuActive = false;

    void Start()
    {
        // Hide the context menu at start
        optionMenu.SetActive(false);

        // Add button listeners
        goHereButton.onClick.AddListener(OnGoHereClicked);
        followButton.onClick.AddListener(OnFollowClicked);
        stayButton.onClick.AddListener(OnStayClicked);
        fetchButton.onClick.AddListener(OnFetchClicked);
        eventSystem = EventSystem.current;  // Get the current Event System


    }

    public void ShowOptionMenu(Vector2 position)
    {
        // Set the menu to active and stop the dog from moving
        optionMenu.SetActive(true);
        isMenuActive = true;
        optionMenu.transform.position = position;
        mousePosition = position;
        if (goHereButton != null && eventSystem != null)
        {
            eventSystem.SetSelectedGameObject(goHereButton.gameObject);
        }
    }

    void Update()
    {
        if (!useSelector)
        {
            // Check for right mouse button click
            if (Input.GetMouseButtonDown(1)) // 1 is the right mouse button
            {
                mousePosition = Input.mousePosition;
                Debug.Log("MEE");
                // Show the context menu at the mouse position
                ShowOptionMenu(mousePosition);
            }

            // Hide the menu when clicking outside of it
            if (Input.GetMouseButtonDown(0)) // Left mouse button
            {
                // Check if the click is outside the context menu
                if (!RectTransformUtility.RectangleContainsScreenPoint((RectTransform)optionMenu.transform, Input.mousePosition))
                {
                    CloseOptionMenu();
                }
            }
        }
    }

    public void OnGoHereClicked()
    {
        Debug.Log("Go Here clicked");
        DogBarkCommandAudio.Play();
        Vector2 worldPosition;
        if (!useSelector)
        {
            worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            dog.SetTargetPosition(worldPosition);
            optionMenu.SetActive(false);
        }
        else
        {
            worldPosition = optionMenu.transform.position;
            dog.SetTargetPosition(worldPosition);
            optionMenu.SetActive(false);
        }
        dog.SetTargetPosition(worldPosition);
        CloseOptionMenu();
    }

    public void OnFollowClicked()
    {
        Debug.Log("Follow clicked");
        dog.Follow();
        DogBarkCommandAudio.Play();
        CloseOptionMenu();
    }

    public void OnStayClicked()
    {
        Debug.Log("Stay clicked");
        dog.Stay();
        DogBarkCommandAudio.Play();
        CloseOptionMenu();
    }

    public void OnFetchClicked()
    {
        Vector2 worldPosition;
        if (!useSelector)
        {
            Debug.Log("Fetch clicked");

            // Convert mouse position to world position.
            worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        }
        else
        {
            Debug.Log("Fetch clicked");

            // Convert mouse position to world position.
            worldPosition = optionMenu.transform.position;
        }
        DogBarkCommandAudio.Play();
        CloseOptionMenu();
        dog.SetTargetPosition(worldPosition);
        dog.StartFetchCoroutine(worldPosition);
        // StartCoroutine(WaitForDogToStop(worldPosition));
    }



    void CloseOptionMenu()
    {
        optionMenu.SetActive(false);
        isMenuActive = false;
    }
}
