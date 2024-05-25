using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class UIButtonController : MonoBehaviour
{
    // Reference to the GameObject to disable
    public GameObject disableGameObject;

    // Reference to the GameObject to enable
    public GameObject enableGameObject;

    // Name of the button associated with this script
    public string buttonName;

    // Duration of the transition in seconds
    public float transitionDuration = 0.5f;

    void Start()
    {
        // Get the root visual element of the UI Document
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        // Find the button in the UI Document
        Button button = root.Q<Button>(buttonName);

        // Add a click event handler to the button
        if (button != null)
        {
            button.clicked += () =>
            {
                // Check if both GameObjects are assigned
                if (disableGameObject != null && enableGameObject != null)
                {
                    // Disable the current GameObject
                    if (disableGameObject.activeSelf)
                    {
                        disableGameObject.SetActive(false);
                    }

                    // Enable the target GameObject and animate its transition
                    enableGameObject.SetActive(true);
                    enableGameObject.transform.DOScale(1f, transitionDuration);
                    enableGameObject.transform.DOLocalMove(Vector3.zero, transitionDuration);
                }
                else
                {
                    Debug.LogWarning("One or both GameObjects are not assigned.");
                }
            };
        }
        else
        {
            Debug.LogWarning("Button not found for GameObject: " + gameObject.name);
        }
    }
}
