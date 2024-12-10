using UnityEngine;
using System.Collections.Generic;

public class DestinationSelector : MonoBehaviour
{
    public List<GameObject> destinations = new List<GameObject>();
    private int currentIndex = 0;
    public bool isSelecting = false;
    public RightClickOptions currentOptionMenu; // Reference to the currently active options menu instance
    private bool canMoveHorizontally = true;

    private void Start()
    {
        GameObject[] allDestinations = GameObject.FindGameObjectsWithTag("Clickable");
        ClearHighlights();
    }

    void Update()
    {
        // Toggle selection mode on or off with the Fire1 button
        if (Input.GetButtonDown("Fire1"))
        {
            if (isSelecting)
            {
                DeactivateSelectionMode();
            }
            else
            {
                ActivateSelectionMode();
            }
        }

        // Only proceed if in selection mode
        if (isSelecting)
        {
            HandleSelectionInput();
        }
    }

    private void DeactivateSelectionMode()
    {
        // Exit selection mode and clear highlights
        isSelecting = false;
        ClearHighlights();
    }

    private void HandleSelectionInput()
    {
        Debug.Log("in here");
        // Handle horizontal input for cycling through destinations
        float horizontalInput = Input.GetAxis("Horizontal");

        if (canMoveHorizontally)
        {
            if (horizontalInput > 0.5f)
            {
                SelectNextDestination();
                canMoveHorizontally = false;
            }
            else if (horizontalInput < -0.5f)
            {
                SelectPreviousDestination();
                canMoveHorizontally = false;
            }
        }
        else if (Mathf.Abs(horizontalInput) < 0.1f)
        {
            // Reset the flag when the horizontal input returns to neutral
            canMoveHorizontally = true;
        }

        // Confirm selection and open options menu with the Submit button
        if (Input.GetButtonDown("Submit"))
        {
            OpenOptionsMenu();
        }
    }

    private void ActivateSelectionMode()
    {
        // Find all game objects tagged as "Destination" on screen
        destinations.Clear(); // Clear any previous destinations
        GameObject[] allDestinations = GameObject.FindGameObjectsWithTag("Clickable");

        // Filter only visible destinations
        foreach (GameObject destination in allDestinations)
        {
            if (IsInView(destination.transform.position))
            {
                destinations.Add(destination);
            }
        }

        // Start selection at the first destination, if any
        if (destinations.Count > 0)
        {
            currentIndex = 0;
            HighlightCurrentDestination();
            isSelecting = true;
        }
    }

    private void SelectNextDestination()
    {
        // Deselect the current destination
        HighlightDestination(destinations[currentIndex], false);

        // Move to the next destination in the list
        currentIndex = (currentIndex + 1) % destinations.Count;

        // Highlight the new destination
        HighlightCurrentDestination();
    }

    private void SelectPreviousDestination()
    {
        // Deselect the current destination
        HighlightDestination(destinations[currentIndex], false);

        // Move to the previous destination in the list
        currentIndex = (currentIndex - 1 + destinations.Count) % destinations.Count;

        // Highlight the new destination
        HighlightCurrentDestination();
    }

    private void HighlightCurrentDestination()
    {
        // Highlight the destination at the current index
        HighlightDestination(destinations[currentIndex], true);
    }

    private void HighlightDestination(GameObject destination, bool highlight)
    {
        // Get the Renderer component of the destination
        Renderer renderer = destination.GetComponent<Renderer>();

        // Toggle the visibility based on the highlight state
        if (renderer != null)
        {
            renderer.enabled = highlight;
        }
    }

    private void OpenOptionsMenu()
    {
        GameObject selectedDestination = destinations[currentIndex];

        // Convert the destination's world position to screen position for UI alignment
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(selectedDestination.transform.position);
        currentOptionMenu.useSelector = true;
        currentOptionMenu.ShowOptionMenu(screenPosition);
        currentOptionMenu.useSelector = false;

        // Optionally: Exit selection mode
        isSelecting = false;
        ClearHighlights();
    }

    private void ClearHighlights()
    {
        // Remove highlights from all destinations
        foreach (GameObject destination in destinations)
        {
            HighlightDestination(destination, false);
        }
    }

    private bool IsInView(Vector3 position)
    {
        // Check if a position is currently viewable on screen
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}
