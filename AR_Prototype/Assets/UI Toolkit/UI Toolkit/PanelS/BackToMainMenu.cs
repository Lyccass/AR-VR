using UnityEngine;
using UnityEngine.UIElements;

public class BackToMainMenu : MonoBehaviour
{
    // Reference to the UIDocument containing the buttons
    public UIDocument uiDocument;

    // Reference to the MainMenu GameObject
    public GameObject mainMenu;

    void Start()
    {
        // Find the MainMenu GameObject
        mainMenu = GameObject.Find("MainMenu");

        // Check the button reference
        CheckButton();
    }

    void OnEnable()
    {
        // Check the button reference when the script is enabled
        CheckButton();
    }

    void OnDisable()
    {
        // Unsubscribe from the button's click event handler when the script is disabled
        if (uiDocument != null && uiDocument.rootVisualElement != null) // Ensure uiDocument and its rootVisualElement are not null
        {
            Button backButton = uiDocument.rootVisualElement.Q<Button>("Button"); // Get the button from the rootVisualElement
            if (backButton != null)
            {
                backButton.clicked -= GoBackToMainMenu;
            }
        }
    }

    void CheckButton()
    {
        if (uiDocument != null)
        {
            // Get the root visual element of the UIDocument
            VisualElement root = uiDocument.rootVisualElement;

            // Find the button with the name "Button"
            Button backButton = root.Q<Button>("Button");

            // Add a click event handler to the button
            if (backButton != null)
            {
                Debug.Log("Button Found: " + backButton.name);

                // Subscribe to the clicked event using RegisterCallback
                backButton.RegisterCallback<ClickEvent>(evt => GoBackToMainMenu());

                // Debug log to confirm the click event handler is attached
                Debug.Log("Click event handler attached to " + backButton.name);
            }
            else
            {
                Debug.LogWarning("Button not found");
            }
        }
        else
        {
            Debug.LogWarning("UIDocument not assigned");
        }
    }


    void GoBackToMainMenu()
    {
        // Activate the MainMenu GameObject
        if (mainMenu != null)
        {
            mainMenu.SetActive(true);
            Debug.Log("Back to MainMenu");
        }
        else
        {
            Debug.LogWarning("MainMenu GameObject not found");
        }

        // Deactivate the UIDocument
        gameObject.SetActive(false);
    }
}
