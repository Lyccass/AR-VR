using UnityEngine;

public class GrayscaleSwipeController : MonoBehaviour
{
    public float swipeThreshold = 50f; // Minimum swipe distance threshold
    public GrayscaleAdjuster grayscaleAdjuster; // Reference to the GrayscaleAdjuster script

    private Vector2 touchDownPosition;
    private Vector2 touchUpPosition;

    void Update()
    {
        // Check for swipe input using touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch

            if (touch.phase == TouchPhase.Began)
            {
                touchDownPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) // Changed to include continuous feedback during swipe
            {
                // Calculate swipe distance
                float swipeDistance = Mathf.Abs(touch.position.y - touchDownPosition.y);

                // Check if swipe distance meets the threshold
                if (swipeDistance > swipeThreshold)
                {
                    // Determine swipe direction
                    Vector2 swipeDirection = touch.position - touchDownPosition;

                    if (Mathf.Abs(swipeDirection.y) > Mathf.Abs(swipeDirection.x))
                    {
                        // Vertical swipe detected
                        AdjustGrayscaleIntensity(touch.position.y);
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchUpPosition = touch.position;
            }
        }
    }

    // Adjust grayscale intensity based on swipe position
    void AdjustGrayscaleIntensity(float swipeY)
    {
        // Calculate the normalized grayscale intensity between 0 and 1 based on swipe position
        float normalizedIntensity = Mathf.Clamp01((swipeY - touchDownPosition.y) / Screen.height);

        // Map the normalized intensity to the desired range (0.1 to 1.0)
        float intensity = Mathf.Lerp(0.1f, 1.0f, normalizedIntensity);

        // Set the grayscale intensity
        grayscaleAdjuster.SetGrayscaleIntensity(intensity);
    }
}
