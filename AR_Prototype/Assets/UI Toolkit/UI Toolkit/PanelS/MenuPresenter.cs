using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MenuPresenter : MonoBehaviour, IPointerClickHandler
{
    // Reference to the GameObjects to enable/disable
    public GameObject aboutDepressionTarget;
    public GameObject personalStorysTarget;
    public GameObject exitTarget;
    public GameObject sendHelpTarget;

    public void OnEnable()
    {
        // Get the root visual element of the UI Document
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Debug.Log("Root Visual Element: " + root.name);

        // Find the buttons with the specified names
        Button aboutDepressionButton = root.Q<Button>("aboutDepressionButton");
        Button personalStorysButton = root.Q<Button>("personalStorysButton");
        Button exitButton = root.Q<Button>("exitButton");
        Button sendHelpButton = root.Q<Button>("sendHelpButton");

        // Add click event handlers to the buttons
        if (aboutDepressionButton != null)
        {
            Debug.Log("Button Found: " + aboutDepressionButton.name);
            aboutDepressionButton.clicked += () => { HandleButtonClick(aboutDepressionTarget); };
        }
        else
        {
            Debug.LogWarning("About Depression Button not found");
        }

        if (personalStorysButton != null)
        {
            Debug.Log("Button Found: " + personalStorysButton.name);
            personalStorysButton.clicked += () => { HandleButtonClick(personalStorysTarget); };
        }
        else
        {
            Debug.LogWarning("Personal Storys Button not found");
        }

        if (exitButton != null)
        {
            Debug.Log("Button Found: " + exitButton.name);
            exitButton.clicked += () => { HandleButtonClick(exitTarget); };
        }
        else
        {
            Debug.LogWarning("Exit Button not found");
        }

        if (sendHelpButton != null)
        {
            Debug.Log("Button Found: " + sendHelpButton.name);
            sendHelpButton.clicked += () => { HandleButtonClick(sendHelpTarget); };
        }
        else
        {
            Debug.LogWarning("Send Help Button not found");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Not used for buttons, since we handle click events individually
    }

    private void HandleButtonClick(GameObject target)
    {
        Debug.Log("Button Clicked: " + target.name);

        // Toggle the target GameObject
        if (target != null)
        {
            target.SetActive(!target.activeSelf);
            Debug.Log("Target GameObject " + (target.activeSelf ? "Enabled: " : "Disabled: ") + target.name);
        }
        else
        {
            Debug.LogWarning("Target GameObject not set");
        }

        // Special case for the exit button to disable the MainMenu
        if (target == exitTarget && exitTarget != null)
        {
            GameObject mainMenu = GameObject.Find("MainMenu");
            if (mainMenu != null)
            {
                mainMenu.SetActive(false);
                Debug.Log("MainMenu Disabled");
            }
            else
            {
                Debug.LogWarning("MainMenu not found");
            }
        }
    }
}
