using UnityEngine;
using UnityEngine.UI;

public class RightClickOptions : MonoBehaviour
{

    public GameObject optionMenu; 
    public Button goHereButton; 
    public Button followButton; 
    public Button stayButton; 
    public Button fetchButton;
    private Vector2 mousePosition;
    public DogMovement dog;

    void Start()
    {
        // Hide the context menu at start
        optionMenu.SetActive(false);

        // Add button listeners
        goHereButton.onClick.AddListener(OnGoHereClicked);
        followButton.onClick.AddListener(OnFollowClicked);
        stayButton.onClick.AddListener(OnStayClicked);
        fetchButton.onClick.AddListener(OnFetchClicked);
        
    }

    void Update()
    {
        // Check for right mouse button click
        if (Input.GetMouseButtonDown(1)) // 1 is the right mouse button
        {
            mousePosition = Input.mousePosition;

            // Show the context menu at the mouse position
            ShowOptionMenu(mousePosition);
        }

        // Hide the menu when clicking outside of it
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            // Check if the click is outside the context menu
            if (!RectTransformUtility.RectangleContainsScreenPoint((RectTransform)optionMenu.transform, Input.mousePosition))
            {
                optionMenu.SetActive(false);
            }
        }
    }

    public void ShowOptionMenu(Vector2 position)
    {
        mousePosition = position;
        optionMenu.SetActive(true);
        optionMenu.transform.position = position; 
    }

    void OnGoHereClicked()
    {
        Debug.Log("Go Here clicked");

        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        dog.SetTargetPosition(worldPosition);
        optionMenu.SetActive(false); 
    }

    void OnFollowClicked()
    {
        Debug.Log("Follow clicked");
        dog.Follow();
        optionMenu.SetActive(false);
    }

    void OnStayClicked()
    {
        Debug.Log("Stay clicked");
        dog.Stay();
        optionMenu.SetActive(false);
    }

    void OnFetchClicked()
    {
        Debug.Log("Fetch clicked");
        optionMenu.SetActive(false);
    }

    

}
