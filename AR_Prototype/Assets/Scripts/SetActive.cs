using UnityEngine;

public class EnableGameObject : MonoBehaviour
{
    public GameObject targetObject; // Reference to the GameObject you want to enable
    private bool isEnabled = false; // Flag to track if the targetObject is currently enabled

    // Call this method to enable the target GameObject and start the timer
    public void EnableObject()
    {
        // Check if the targetObject is not already enabled
        if (!isEnabled)
        {
            // Enable the target GameObject
            targetObject.SetActive(true);
            isEnabled = true;

        }
    }

    // Call this method to disable the target GameObject
 

    
}
