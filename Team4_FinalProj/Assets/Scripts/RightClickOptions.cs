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
    public bool useSelector = false;
    public AudioSource DogBarkCommandAudio;

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


    }

    public void ShowOptionMenu(Vector2 position)
    {
        // Set the menu to active and stop the dog from moving
        optionMenu.SetActive(true);
        isMenuActive = true;
        optionMenu.transform.position = position;
        mousePosition = position;
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

    // // Coroutine to wait for the dog to reach the target
    // IEnumerator WaitForDogToStop(Vector2 targetPosition)
    // {
    //     // Wait while the dog is moving
    //     while (!dog.isStopped())
    //     {
    //         yield return null; // Wait for one frame
    //     }

    //     // Now the dog has stopped moving, check for fetchable objects
    //     CheckForNearbyFetchable();
    // }

    // void CheckForNearbyFetchable()
    // {
    //     // Get all objects with the "Fetch" tag in the scene
    //     GameObject[] fetch = GameObject.FindGameObjectsWithTag("Fetch");

    //     // Get all objects with the "Key" tag
    //     GameObject[] keys = GameObject.FindGameObjectsWithTag("Key");

    //     // Combine both arrays into a single list
    //     GameObject[] fetchables = new GameObject[fetch.Length + keys.Length];
    //     fetch.CopyTo(fetchables, 0);
    //     keys.CopyTo(fetchables, fetch.Length);

    //     // Iterate through each fetchable item
    //     foreach (GameObject fetchable in fetchables)
    //     {
    //         // Calculate the distance between the dog and the fetchable object
    //         float distance = Vector2.Distance(dog.transform.position, fetchable.transform.position);

    //         // If the fetchable is within range, pick it up
    //         if (distance <= fetchableRange)
    //         {
    //             Debug.Log("Fetchable item within range: " + fetchable.name);

    //             // Set it as the current fetchable object
    //             currentFetchable = fetchable;

    //             // Disable the object's physics interactions (if it has any), and attach it to the dog
    //             Rigidbody2D rb = currentFetchable.GetComponent<Rigidbody2D>();
    //             if (rb != null)
    //             {
    //                 rb.isKinematic = true; // Disable physics to make it follow the dog
    //             }

    //             // Set the object's position relative to the dog and parent it to the dog
    //             currentFetchable.transform.SetParent(dog.transform); // Parent it to the dog's transform
    //             currentFetchable.transform.localPosition = Vector3.zero; // Position the object at the dog's transform

    //             currentFetchable.SetActive(true); // Ensure the object is visible after being picked up

    //             Debug.Log("Object picked up and now following the dog: " + currentFetchable.name);

    //             dog.Follow();

    //             return; // Exit once we find the first fetchable object within range
    //         }
    //     }

    //     Debug.Log("No fetchable objects within range.");
    // }

    void CloseOptionMenu()
    {
        optionMenu.SetActive(false);
        isMenuActive = false;
    }
}
