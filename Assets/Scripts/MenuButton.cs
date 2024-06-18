using UnityEngine;
using UnityEngine.UI;
using TMPro; // Include the TextMeshPro namespace
using System.Collections;

public class MenuButton : MonoBehaviour
{
    [Header("Menus")]
    public GameObject currentMenu;  // The menu that the button is currently on
    public GameObject targetMenu;   // The menu to activate when the button is pressed
    public int targetMenuZIndex;    // The z-index for the target menu

    [Header("Fade Settings")]
    public float fadeDuration = 1.5f; // Duration of the fade effect (set to 0.5 for faster transition)

    [Header("Audio Settings")]
    public AudioClip buttonClickSFX; // Sound effect to play on button click
    public AudioSource sfxSource; // Reference to the central SFX AudioSource

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        if (sfxSource != null && buttonClickSFX != null)
        {
            sfxSource.PlayOneShot(buttonClickSFX);
        }

        if (targetMenu != null)
        {
            targetMenu.SetActive(true);
            targetMenu.transform.SetSiblingIndex(targetMenuZIndex);
            StartCoroutine(FadeAndSwitch(currentMenu, targetMenu, fadeDuration));
        }
    }

    IEnumerator FadeAndSwitch(GameObject currentMenu, GameObject targetMenu, float duration)
    {
        CanvasGroup currentCanvasGroup = currentMenu.GetComponent<CanvasGroup>();
        if (currentCanvasGroup == null)
        {
            currentCanvasGroup = currentMenu.AddComponent<CanvasGroup>();
        }

        CanvasGroup targetCanvasGroup = targetMenu.GetComponent<CanvasGroup>();
        if (targetCanvasGroup == null)
        {
            targetCanvasGroup = targetMenu.AddComponent<CanvasGroup>();
        }

        TextMeshProUGUI[] currentTextElements = currentMenu.GetComponentsInChildren<TextMeshProUGUI>();
        TextMeshProUGUI[] targetTextElements = targetMenu.GetComponentsInChildren<TextMeshProUGUI>();

        targetCanvasGroup.alpha = 0f;
        foreach (var text in targetTextElements)
        {
            Color color = text.color;
            color.a = 0f;
            text.color = color;
        }

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float smoothStep = t * t * (3f - 2f * t); // Ease-in-out function
            currentCanvasGroup.alpha = Mathf.Clamp01(1 - smoothStep);
            targetCanvasGroup.alpha = Mathf.Clamp01(smoothStep);

            foreach (var text in currentTextElements)
            {
                Color color = text.color;
                color.a = Mathf.Clamp01(1 - smoothStep);
                text.color = color;
            }

            foreach (var text in targetTextElements)
            {
                Color color = text.color;
                color.a = Mathf.Clamp01(smoothStep);
                text.color = color;
            }

            yield return null;
        }

        currentCanvasGroup.alpha = 0f;
        foreach (var text in currentTextElements)
        {
            Color color = text.color;
            color.a = 0f;
            text.color = color;
        }

        targetCanvasGroup.alpha = 1f;
        foreach (var text in targetTextElements)
        {
            Color color = text.color;
            color.a = 1f;
            text.color = color;
        }

        if (currentMenu != null)
        {
            currentMenu.SetActive(false);
        }
    }
}
