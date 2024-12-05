using UnityEngine;

public class ShortenAudio : MonoBehaviour
{
    public AudioClip audioClip;  // Assign the audio clip in the inspector
    public float startTime = 1f; // Start time in seconds
    public float playDuration = 3f; // Duration to play in seconds

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        
        // Set the starting point of the audio
        audioSource.time = startTime;

        // Play the audio clip
        audioSource.Play();

        // Stop playback after the specified duration
        Invoke(nameof(StopAudio), playDuration);
    }

    private void StopAudio()
    {
        audioSource.Stop();
    }
}
