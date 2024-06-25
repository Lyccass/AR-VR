using UnityEngine;

public class DisableGameObjects : MonoBehaviour
{
    [Tooltip("List of GameObjects to check and possibly disable.")]
    public GameObject[] gameObjectsToCheck;

    // Method to disable all game objects in the list if they are not already disabled
    public void CheckAndDisableGameObjects()
    {
        foreach (GameObject gameObject in gameObjectsToCheck)
        {
            if (gameObject != null && gameObject.activeSelf) // Check if the GameObject is active
            {
                gameObject.SetActive(false); // Disable the GameObject
            }
        }
    }
}
