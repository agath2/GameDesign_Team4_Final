using UnityEngine;

public class FetchableTrigger : MonoBehaviour
{
    private RightClickOptions rightClickOptions;

    private void Start()
    {
        // Get the RightClickOptions component from the parent (the OptionMenuCanvas)
        rightClickOptions = FindObjectOfType<RightClickOptions>();
    }
}