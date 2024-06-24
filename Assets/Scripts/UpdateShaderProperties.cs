using UnityEngine;
using System.Collections;

public class UpdateShaderGraphProperties : MonoBehaviour
{
    // Reference to the GameObject with the material
    public GameObject orbNew;

    // Variables to hold the shader property values
    public float cloudScale = 1.0f;
    public float cloudPower = 1.0f;
    public float cloudAlpha = 1.0f;
    public Vector2 cloudSpeed = new Vector2(0.1f, 0.1f);
    public float distortScale = 1.0f;
    public Vector2 distortSpeed = new Vector2(0.1f, 0.1f);
    public Color cloudColor = Color.white;

    // Size variables
    private float targetSize;
    private float maxSize = 25000;
    private bool isScaling = false;
    private float multiplier = 1.2f;

    private Material cloudMaterial;

    void Start()
    {
        // Get the Material component from the GameObject
        Renderer renderer = orbNew.GetComponent<Renderer>();

        // Ensure the renderer has a material attached
        if (renderer != null)
        {
            cloudMaterial = renderer.material;
        }
        else
        {
            Debug.LogError("Renderer not found on the specified GameObject.");
        }

        // Initialize the target size to the current size of the object
        targetSize = orbNew.transform.localScale.x;

        // Initialize the shader properties
        UpdateShaderMaterialProperties();
    }

    void Update()
    {
        // Dynamically update the shader properties if needed
        UpdateShaderMaterialProperties();
    }

    void UpdateShaderMaterialProperties()
    {
        if (cloudMaterial != null)
        {
            // Set the shader properties
            cloudMaterial.SetFloat("_cloudScale", cloudScale);
            cloudMaterial.SetFloat("_cloudPower", cloudPower);
            cloudMaterial.SetFloat("_CloudsAlpha", cloudAlpha);
            cloudMaterial.SetVector("_cloudSpeed", new Vector4(cloudSpeed.x, cloudSpeed.y, 0, 0));
            cloudMaterial.SetFloat("_distortScale", distortScale);
            cloudMaterial.SetVector("_distortSpeed", new Vector4(distortSpeed.x, distortSpeed.y, 0, 0));
            cloudMaterial.SetColor("_CloudColor", cloudColor);
        }
        else
        {
            Debug.LogError("Cloud material is not assigned.");
        }
    }

    public void IncreaseSize()
    {
        if (!isScaling && targetSize < maxSize)
        {
            float newSize = Mathf.Min(targetSize * multiplier, maxSize);
            StartCoroutine(IncreaseSizeOverTime(newSize, 5)); // Increase to new target size over 2 seconds

            // Update shader properties
            cloudScale *= multiplier;
            cloudPower *= multiplier;
            cloudAlpha *= multiplier;
            distortScale *= multiplier;

            // Ensure cloudAlpha does not exceed 1.0 if it is a value between 0 and 1
            cloudAlpha = Mathf.Clamp(cloudAlpha, 0, 1);

            // Update the shader properties to reflect the new values
            UpdateShaderMaterialProperties();
        }
    }

    IEnumerator IncreaseSizeOverTime(float newSize, float duration)
    {
        isScaling = true;
        Vector3 initialScale = orbNew.transform.localScale;
        Vector3 targetScale = new Vector3(newSize, newSize, newSize);
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            orbNew.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        orbNew.transform.localScale = targetScale;
        targetSize = newSize; // Update targetSize to the new size
        isScaling = false;
    }
}
