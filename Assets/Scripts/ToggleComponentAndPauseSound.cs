using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ToggleGameObjectAndMuteSound : MonoBehaviour
{
    [Header("Settings")]
    public GameObject gameObjectToDisable;  // The GameObject to disable
    public AudioSource audioSource1;        // The first AudioSource to mute
    public AudioSource audioSource2;        // The second AudioSource to mute
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
            audioSource1.mute = true; // Mute the first audio source
        }

        if (audioSource2 != null)
        {
            audioSource2.mute = true; // Mute the second audio source
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
                audioSource1.mute = false; // Unmute the first audio source
            }

            if (audioSource2 != null)
            {
                audioSource2.mute = false; // Unmute the second audio source
            }
        }
    }
}
