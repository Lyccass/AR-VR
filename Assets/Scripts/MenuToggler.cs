using UnityEngine;
using TMPro; // Include the TextMeshPro namespace
using System.Collections;

public class MenuToggler : MonoBehaviour
{
    [Header("Menus")]
    public GameObject currentMenu;  // The menu that is currently active
    public GameObject targetMenu;   // The menu to activate after the interval
    public int targetMenuZIndex;    // The z-index for the target menu

    [Header("Fade Settings")]
    public float fadeDuration = 1.5f; // Duration of the fade effect

    [Header("Toggle Settings")]
    public float toggleInterval = 5f; // Interval in seconds to toggle menus

    private void Start()
    {
        StartCoroutine(ToggleMenus());
    }

    IEnumerator ToggleMenus()
    {
        while (true)
        {
            yield return new WaitForSeconds(toggleInterval);

            if (targetMenu != null)
            {
                targetMenu.SetActive(true);
                targetMenu.transform.SetSiblingIndex(targetMenuZIndex);
                StartCoroutine(FadeAndSwitch(currentMenu, targetMenu, fadeDuration));

                // Swap currentMenu and targetMenu for the next cycle
                GameObject tempMenu = currentMenu;
                currentMenu = targetMenu;
                targetMenu = tempMenu;
            }
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
