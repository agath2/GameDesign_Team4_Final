using UnityEngine;
using System.Collections.Generic;

public class DestinationSelector : MonoBehaviour
{
    public List<GameObject> destinations = new List<GameObject>();
    private int currentIndex = 0;
    public bool isSelecting = false;
    public RightClickOptions currentOptionMenu; // Reference to the currently active options menu instance

    private void Start()
    {
        GameObject[] allDestinations = GameObject.FindGameObjectsWithTag("Clickable");
        ClearHighlights();
    }
    void Update()
    {
        // Activate Selection Mode
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ActivateSelectionMode();
        }

        // Only proceed if in selection mode
        if (isSelecting)
        {
            // Cycle through destinations with arrow keys
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SelectNextDestination();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SelectPreviousDestination();
            }

            // Confirm selection and open options menu
            if (Input.GetKeyDown(KeyCode.E)) // 'Go' button
            {
                OpenOptionsMenu();
            }
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
