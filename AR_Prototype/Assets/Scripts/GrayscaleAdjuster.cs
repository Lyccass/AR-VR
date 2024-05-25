using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GrayscaleAdjuster : MonoBehaviour
{
    public Renderer objectRenderer;
    [Range(0f, 1f)]
    public float grayscaleIntensity;

    public Slider grayscaleSlider;

    public void SetGrayscaleIntensity(float intensity)
    {
        grayscaleIntensity = intensity;
        UpdateGrayscaleIntensity();
    }


    void Start()
    {
        StartCoroutine(DelayedAssignment());

        // Ensure that the grayscaleSlider is not null
        if (grayscaleSlider != null)
        {
            // Add a listener to the slider's value changed event
            grayscaleSlider.onValueChanged.AddListener(SetGrayscaleIntensity);
        }
        else
        {
            Debug.LogWarning("Grayscale Slider is not assigned in the Inspector!");
        }
    }


    IEnumerator DelayedAssignment()
    {
        yield return new WaitForSeconds(.1f);

        GameObject arCamera = GameObject.FindGameObjectWithTag("MainCamera");

        if (arCamera != null)
        {
            Transform videoBackground = arCamera.transform.Find("VideoBackground");

            if (videoBackground != null)
            {
                objectRenderer = videoBackground.GetComponent<Renderer>();
                
            }
            else
            {
                Debug.LogWarning("VideoBackground GameObject not found under AR camera.");
            }
        }
        else
        {
            Debug.LogWarning("AR camera not found in the scene.");
        }
    }

    void Update()
    {
        if (objectRenderer != null)
        {
            
            if (objectRenderer.material != null)
            {
                
                if (objectRenderer.material.shader != null)
                {
                   
                }
                else
                {
                    Debug.LogWarning("Material shader not set.");
                }
            }
            else
            {
                Debug.LogWarning("Material not set.");
            }
        }
        else
        {
           
        }

        // Ensure the objectRenderer and shader are set
        if (objectRenderer != null && objectRenderer.material != null && objectRenderer.material.shader.name == "Custom/Grayscale")
        {
            // Set the grayscale intensity property within the range of 0 to 1
            objectRenderer.material.SetFloat("_GrayscaleIntensity", Mathf.Clamp01(grayscaleIntensity));
        }
    }
    void UpdateGrayscaleIntensity()
    {
        // Ensure the objectRenderer and shader are set
        if (objectRenderer != null && objectRenderer.material != null && objectRenderer.material.shader.name == "Custom/Grayscale")
        {
            // Set the grayscale intensity property within the range of 0 to 1
            objectRenderer.material.SetFloat("_GrayscaleIntensity", Mathf.Clamp01(grayscaleIntensity));
        }
    }
}
