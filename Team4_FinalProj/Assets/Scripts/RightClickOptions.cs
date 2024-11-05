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
    private GameObject currentFetchable; // Store the current fetchable object.
    private bool isNearFetchable = false; // Flag to check if the dog is near a fetchable object.

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

    void ShowOptionMenu(Vector2 position)
    {
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

    // Method to handle the fetch logic when the fetch button is clicked
    void OnFetchClicked()
    {
        Debug.Log("Fetch clicked");

        // Convert mouse position to world position.
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Move the dog to the target position.
        dog.SetTargetPosition(worldPosition);

        // If there's a fetchable object nearby, pick it up.
        if (isNearFetchable && currentFetchable != null)
        {
            // Disable the object's physics interactions (if it has any), and attach it to the dog.
            Rigidbody2D rb = currentFetchable.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = true; // Disable physics to make it follow the dog.
            }

            // Set the object's position relative to the dog and parent it to the dog.
            currentFetchable.transform.SetParent(dog.transform); // Parent it to the dog's transform.

            // Position the object at the dog's transform.
            currentFetchable.transform.localPosition = Vector3.zero;

            currentFetchable.SetActive(true); // Ensure the object is visible after being picked up.

            Debug.Log("Object picked up and now following the dog: " + currentFetchable.name);
        }

        // Hide the option menu after the fetch action.
        optionMenu.SetActive(false);
    }

    // Method to set the fetchable object when the dog is near it
    public void SetFetchableObject(GameObject fetchable)
    {
        isNearFetchable = true;
        currentFetchable = fetchable;
    }

    // Method to clear the fetchable object when the dog moves away
    public void ClearFetchableObject()
    {
        isNearFetchable = false;
        currentFetchable = null;
    }
}
