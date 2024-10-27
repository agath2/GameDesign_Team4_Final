using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f); // Scale when hovered
    private Vector3 originalScale;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale; // Store the original scale
    }

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
}
