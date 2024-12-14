using UnityEngine;
using System.Collections;

public class PlayMusicWithFade2D : MonoBehaviour
{
    [SerializeField] private AudioSource musicPlayer; // Reference to the AudioSource
    [SerializeField] private float fadeDuration = 1.5f; // Duration of the fade effect
    private bool hasPlayed = false; // Ensure the music plays only once

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player triggered the event
        if (other.CompareTag("Player") && !hasPlayed)
        {
            StartCoroutine(FadeIn()); // Start fade-in effect
            hasPlayed = true; // Prevent it from playing again
        }
    }

    private IEnumerator FadeIn()
    {
        float startVolume = 0f;
        musicPlayer.volume = startVolume; // Start with zero volume
        musicPlayer.Play();

        while (musicPlayer.volume < 1f)
        {
            musicPlayer.volume += Time.deltaTime / fadeDuration; // Gradually increase volume
            yield return null;
        }

        musicPlayer.volume = 1f; // Ensure it ends at full volume
    }

    private IEnumerator FadeOut()
    {
        float startVolume = musicPlayer.volume;

        while (musicPlayer.volume > 0f)
        {
            musicPlayer.volume -= Time.deltaTime / fadeDuration; // Gradually decrease volume
            yield return null;
        }

        musicPlayer.Stop(); // Stop the audio when volume reaches zero
        musicPlayer.volume = startVolume; // Reset volume for future use
    }

    public void TriggerFadeOut()
    {
        StartCoroutine(FadeOut()); // Public method to trigger fade-out
    }
}
