using UnityEngine;
using UnityEngine.EventSystems;

public class DefaultSelection : MonoBehaviour
{
    public GameObject defaultButton;

    void Start()
    {
        // Set the default selected button
        EventSystem.current.SetSelectedGameObject(defaultButton);
    }
}
