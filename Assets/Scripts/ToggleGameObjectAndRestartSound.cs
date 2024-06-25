using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ToggleGameObjectAndRestartSound : MonoBehaviour
{
    [Header("Settings")]
    public GameObject gameObjectToDisable;  // The GameObject to disable
    public AudioSource audioSource1;        // The first AudioSource to stop and restart
    public AudioSource audioSource2;        // The second AudioSource to stop and restart
    public GameObject imageTarget;          // The ImageTarget GameObject

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }

        // Subscribe to Vuforia events
        var observerBehaviour = imageTarget.GetComponent<ObserverBehaviour>();
        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from Vuforia events
        var observerBehaviour = imageTarget.GetComponent<ObserverBehaviour>();
        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }

    void OnButtonClick()
    {
        if (gameObjectToDisable != null)
        {
            gameObjectToDisable.SetActive(false); // Disable the GameObject
        }

        if (audioSource1 != null)
        {
            audioSource1.Stop(); // Stop the first audio source
        }

        if (audioSource2 != null)
        {
            audioSource2.Stop(); // Stop the second audio source
        }
    }

    void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED)
        {
            if (gameObjectToDisable != null && !gameObjectToDisable.activeSelf)
            {
                gameObjectToDisable.SetActive(true); // Re-enable the GameObject
            }

            if (audioSource1 != null)
            {
                audioSource1.Play(); // Start playing the first audio source again
            }

            if (audioSource2 != null)
            {
                audioSource2.Play(); // Start playing the second audio source again
            }
        }
    }
}
