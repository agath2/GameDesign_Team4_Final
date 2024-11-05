using UnityEngine;

public class FetchableTrigger : MonoBehaviour
{
    private RightClickOptions rightClickOptions;

    private void Start()
    {
        // Get the RightClickOptions component from the parent (the OptionMenuCanvas)
        rightClickOptions = FindObjectOfType<RightClickOptions>();
    }

    // Trigger detection when the dog enters the fetchable area
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that enters the trigger has the tag "Fetch"
        if (other.CompareTag("Fetch"))
        {
            // Notify the OptionMenuCanvas that it is near a fetchable object
            rightClickOptions.SetFetchableObject(other.gameObject);
            Debug.Log("Dog is near fetch-able object: " + other.name);
        }
    }

    // Trigger detection when the dog exits the fetchable area
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the dog leaves the fetchable area
        if (other.CompareTag("Fetch"))
        {
            // Notify the OptionMenuCanvas that the dog has left the fetchable object
            rightClickOptions.ClearFetchableObject();
            Debug.Log("Dog left the fetch-able object: " + other.name);
        }
    }
}
