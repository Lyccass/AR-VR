using UnityEngine;

public class PitchChangingSound : MonoBehaviour
{
    public float pitchIncreaseRate = 0.1f; // Rate at which pitch increases per second
    public float maxPitch = 3f; // Maximum pitch value
    public float timeToMaxPitch = 10f; // Time taken to reach max pitch (seconds)

    private AudioSource audioSource;
    private float elapsedTime = 0f;

    // Public property to access current pitch value
    public float CurrentPitch { get { return audioSource.pitch; } }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Ensure the AudioSource doesn't play immediately
        audioSource.playOnAwake = false;
        // Set initial pitch to 1
        audioSource.pitch = 1f;
    }

    void Update()
    {
        if (audioSource.isPlaying)
        {
            if (elapsedTime < timeToMaxPitch)
            {
                // Increase elapsed time
                elapsedTime += Time.deltaTime;
                // Calculate the new pitch based on elapsed time
                float t = elapsedTime / timeToMaxPitch;
                float newPitch = Mathf.Lerp(1f, maxPitch, t);
                // Set the new pitch
                audioSource.pitch = newPitch;

                // Debug the current pitch
                Debug.Log("Current Pitch: " + audioSource.pitch);
            }
            else
            {
                // Ensure the pitch reaches max after the specified time
                audioSource.pitch = maxPitch;
            }
        }
    }
}