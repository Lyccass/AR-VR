using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIButtonSceneChanger : MonoBehaviour
{
    // Scene name to load
    public string sceneName;

    // Name of the button associated with this script
    public string buttonName;

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
                // Change the scene
                SceneManager.LoadScene(sceneName);
            };
        }
        else
        {
            Debug.LogWarning("Button not found for GameObject: " + gameObject.name);
        }
    }
}
