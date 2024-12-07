using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Appear : MonoBehaviour
{
    // Reference to the Canvas you want to show
    public GameObject mainPanel;
    public float fadeDelay = 5f;
    private CanvasGroup canvasGroup;

    void Start()
    {
        // Get the CanvasGroup from the mainPanel (if it exists)
        canvasGroup = mainPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            // If no CanvasGroup is attached, add one
            canvasGroup = mainPanel.AddComponent<CanvasGroup>();
        }
        // Start with the canvas hidden (alpha set to 0)
        canvasGroup.alpha = 0;
        mainPanel.SetActive(false);
    }

    // Called when another Collider2D enters the trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        mainPanel.SetActive(true); // Show the canvas
        StartCoroutine(Fade(canvasGroup, fadeDelay, true)); // Fade In
        Debug.Log("Show");
    }

    // Called when the Collider2D exits the trigger zone
    private void OnTriggerExit2D(Collider2D other)
    {
        StartCoroutine(FadeAndHide(canvasGroup, fadeDelay)); // Fade Out and Hide
        Debug.Log("Hide");
    }

    IEnumerator Fade(CanvasGroup canvasGroup, float fadeTime, bool fadeIn)
    {
        float elapsedTime = 0.0f;
        float targetAlpha = fadeIn ? 1f : 0f;

        // Store initial alpha value of CanvasGroup
        float startAlpha = canvasGroup.alpha;

        while (elapsedTime < fadeTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime;

            // Interpolate alpha value based on elapsed time
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeTime);
        }

        // Ensure final alpha is set (in case of precision issues)
        canvasGroup.alpha = targetAlpha;
    }

    // New Coroutine to handle fading out and then disabling the mainPanel
    IEnumerator FadeAndHide(CanvasGroup canvasGroup, float fadeTime)
    {
        // Perform the fade-out
        yield return StartCoroutine(Fade(canvasGroup, fadeTime, false));

        // After fade-out, hide the panel
        mainPanel.SetActive(false);
    }
}
