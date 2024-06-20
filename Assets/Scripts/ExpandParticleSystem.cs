using UnityEngine;

public class ExpandParticleShapeRadius : MonoBehaviour
{
    public ParticleSystem particleSystem; // Dein Partikelsystem
    public float startRadius = 500f; // Anfangsradius
    public float maxRadius = 15000f; // Maximalradius
    public float radiusIncreaseFactor = 1.2f; // Erhöhungsfaktor für Radius
    public float radiusIncreaseSpeed = 100f; // Geschwindigkeit, mit der der Radius zunimmt

    public float startMinParticleSize = 0.4f; // Anfangsgröße der Partikel
    public float maxMinParticleSize = 2f; // Maximale Partikelgröße
    public float particleSizeIncreaseFactor = 1.1f; // Erhöhungsfaktor für Partikelgröße
    public float particleSizeIncreaseSpeed = 0.02f; // Geschwindigkeit, mit der die Partikelgröße zunimmt

    private ParticleSystem.ShapeModule shapeModule;
    private ParticleSystemRenderer particleRenderer;
    private float targetRadius;
    private float targetMinParticleSize;

    void Start()
    {
        if (particleSystem == null)
        {
            particleSystem = GetComponent<ParticleSystem>();
        }

        shapeModule = particleSystem.shape;
        particleRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();
        shapeModule.radius = startRadius;
        particleRenderer.minParticleSize = startMinParticleSize;
        targetRadius = startRadius;
        targetMinParticleSize = startMinParticleSize;

        Debug.Log("Initial radius: " + shapeModule.radius);
        Debug.Log("Initial min particle size: " + particleRenderer.minParticleSize);
    }

    void Update()
    {
        // Radius schrittweise erhöhen
        if (shapeModule.radius < targetRadius)
        {
            shapeModule.radius += radiusIncreaseSpeed * Time.deltaTime;
            shapeModule.radius = Mathf.Min(shapeModule.radius, targetRadius); // Verhindert Überschreiten des Zielradius
            Debug.Log("Updated radius: " + shapeModule.radius);
        }

        // Min Particle Size schrittweise erhöhen
        if (particleRenderer.minParticleSize < targetMinParticleSize)
        {
            particleRenderer.minParticleSize += particleSizeIncreaseSpeed * Time.deltaTime;
            particleRenderer.minParticleSize = Mathf.Min(particleRenderer.minParticleSize, targetMinParticleSize); // Verhindert Überschreiten des Zielwertes
            Debug.Log("Updated min particle size: " + particleRenderer.minParticleSize);
        }
    }

    public void IncreaseRadiusByButton()
    {
        targetRadius = Mathf.Min(targetRadius * radiusIncreaseFactor, maxRadius);
        targetMinParticleSize = Mathf.Min(targetMinParticleSize * particleSizeIncreaseFactor, maxMinParticleSize);
        Debug.Log("New target radius: " + targetRadius);
        Debug.Log("New target min particle size: " + targetMinParticleSize);
    }
}
