using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Vuforia;


public class ShaderScript : MonoBehaviour

{
    private ObserverBehaviour mObserverBehaviour;
    public Volume postProcessingVolume;
    public string shaderName = "YourShaderName"; // Replace with the name of your shader

    void Start()
    {
        mObserverBehaviour = GetComponent<ObserverBehaviour>();
        if (mObserverBehaviour)
        {
            mObserverBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }

        // Ensure the post-processing volume is initially disabled
        if (postProcessingVolume)
        {
            postProcessingVolume.enabled = false;
        }
    }

    private void OnDestroy()
    {
        if (mObserverBehaviour)
        {
            mObserverBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED ||
            targetStatus.Status == Status.EXTENDED_TRACKED)
        {
            OnTargetFound();
        }
        else
        {
            OnTargetLost();
        }
    }

    void OnTargetFound()
    {
        Debug.Log("Target Found");
        // Activate the post-processing shader
        if (postProcessingVolume)
        {
            postProcessingVolume.enabled = true;
        }
    }

    void OnTargetLost()
    {
        Debug.Log("Target Lost");
        // Deactivate the post-processing shader
        if (postProcessingVolume)
        {
            postProcessingVolume.enabled = false;
        }
    }
}