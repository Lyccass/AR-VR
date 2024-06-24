using UnityEngine;

public class DisableGameObject : MonoBehaviour
{
    public GameObject targetObject; // Reference to the GameObject you want to disable
    private bool isDisabled = false; // Flag to track if the targetObject is currently disabled

    // Call this method to disable the target GameObject
    public void DisableObject()
    {
        // Check if the targetObject is not already disabled
        if (!isDisabled)
        {
            // Disable the target GameObject
            targetObject.SetActive(false);
            isDisabled = true;
        }
    }

    // Call this method to enable the target GameObject
    public void EnableObject()
    {
        // Check if the targetObject is currently disabled
        if (isDisabled)
        {
            // Enable the target GameObject
            targetObject.SetActive(true);
            isDisabled = false;
        }
    }
}
