using UnityEngine;
using UnityEngine.UI;
using System.Collections;


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

    public void ShowOptionMenu(Vector2 position)
    {
        optionMenu.SetActive(true);
        optionMenu.transform.position = position;
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

        // Convert mouse position to world position.
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Move the dog to the target position first
        dog.SetTargetPosition(worldPosition);

        // Wait until the dog reaches the target position
        StartCoroutine(WaitForDogToStop(worldPosition));
    }

    // Coroutine to wait for the dog to reach the target
    IEnumerator WaitForDogToStop(Vector2 targetPosition)
    {
        // Wait while the dog is moving
        while (!dog.isStopped())
        {
            yield return null; // Wait for one frame
        }

        // Now the dog has stopped moving, check for fetchable objects
        CheckForNearbyFetchable();
    }



    void CheckForNearbyFetchable()
    {
        // Get all objects with the "Fetch" tag in the scene
        GameObject[] fetchables = GameObject.FindGameObjectsWithTag("Fetch");

        // Iterate through each fetchable item
        foreach (GameObject fetchable in fetchables)
        {
            // Calculate the distance between the dog and the fetchable object
            float distance = Vector2.Distance(dog.transform.position, fetchable.transform.position);

            // If the fetchable is within range, pick it up
            if (distance <= fetchableRange)
            {
                Debug.Log("Fetchable item within range: " + fetchable.name);

                // Set it as the current fetchable object
                currentFetchable = fetchable;

                // Disable the object's physics interactions (if it has any), and attach it to the dog
                Rigidbody2D rb = currentFetchable.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.isKinematic = true; // Disable physics to make it follow the dog
                }

                // Set the object's position relative to the dog and parent it to the dog
                currentFetchable.transform.SetParent(dog.transform); // Parent it to the dog's transform
                currentFetchable.transform.localPosition = Vector3.zero; // Position the object at the dog's transform

                currentFetchable.SetActive(true); // Ensure the object is visible after being picked up

                Debug.Log("Object picked up and now following the dog: " + currentFetchable.name);

                return; // Exit once we find the first fetchable object within range
            }
        }

        Debug.Log("No fetchable objects within range.");
    }
}