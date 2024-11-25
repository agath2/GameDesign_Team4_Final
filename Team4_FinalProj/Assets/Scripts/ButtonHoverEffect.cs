using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    private RectTransform rectTransform;
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f); // Scale when hovered
    private Vector3 originalScale;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale; // Store the original scale
    }

    // Pointer hover (mouse interaction)
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Scale up when the mouse enters
        rectTransform.localScale = hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Scale back to original when the mouse exits
        rectTransform.localScale = originalScale;
    }

    // Controller navigation (selected state)
    public void OnSelect(BaseEventData eventData)
    {
        // Scale up when the button is selected via controller or keyboard
        rectTransform.localScale = hoverScale;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        // Scale back to original when the button is deselected
        rectTransform.localScale = originalScale;
    }
}
