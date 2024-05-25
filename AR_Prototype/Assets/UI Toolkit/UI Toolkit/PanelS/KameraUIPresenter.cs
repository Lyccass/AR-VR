using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class KameraUIPresenter : MonoBehaviour, IPointerClickHandler
{
    // Reference to the GameObject you want to enable
    public GameObject nextGameObject;

    public void OnEnable()
    {
        // Get the root visual element of the UI Document
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Debug.Log("Root Visual Element: " + root.name);

        // Find the button with the specified name
        Button hamburger = root.Q<Button>("LogoHamburgermenu");
        if (hamburger != null)
        {
            Debug.Log("Button Found: " + hamburger.name);

            // Add a click event handler to the button
            hamburger.clicked += () =>
            {
                HandleButtonClick();
            };
        }
        else
        {
            Debug.LogWarning("Button not found");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HandleButtonClick();
    }

    private void HandleButtonClick()
    {
        Debug.Log("Button Clicked");

        // Activate the next GameObject
        if (nextGameObject != null)
        {
            nextGameObject.SetActive(true);
            Debug.Log("Next GameObject Enabled: " + nextGameObject.name);
        }
        else
        {
            Debug.LogWarning("Next GameObject not set");
        }
    }
}
