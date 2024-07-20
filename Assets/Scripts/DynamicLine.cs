using System.Collections;
using UnityEngine;

public class DynamicLine : MonoBehaviour
{
    public Transform player;
    public Transform playerGhost;
    public Material solidMaterial; // Assign this in the inspector with a solid line material
    public Material dashedMaterial; // Assign this in the inspector with the dashed line material
    private LineRenderer lineRenderer;
    private bool startDeathCounter;
    private float deathCounter;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        // Set the line positions
        lineRenderer.SetPosition(0, player.position);
        lineRenderer.SetPosition(1, playerGhost.position);

        float distance = Vector3.Distance(player.position, playerGhost.position);

        if (distance < 2f) //Short Distance
        {
            // Use solid material
            lineRenderer.material = solidMaterial;
            // Ensure the solid line covers the entire length
            lineRenderer.textureMode = LineTextureMode.Stretch;

            // Set the color gradient to green
            Gradient gradientGreen = new Gradient();
            gradientGreen.colorKeys = new GradientColorKey[] { new GradientColorKey(Color.green, 0f), new GradientColorKey(Color.green, 1f) };
            lineRenderer.colorGradient = gradientGreen;
        }
        else if (distance < 4f) //Mid Distance
        {
            // Use solid material
            lineRenderer.material = solidMaterial;
            // Ensure the solid line covers the entire length
            lineRenderer.textureMode = LineTextureMode.Stretch;

            // Set the color gradient to yellow
            Gradient gradientYellow = new Gradient();
            gradientYellow.colorKeys = new GradientColorKey[] { new GradientColorKey(Color.yellow, 0f), new GradientColorKey(Color.yellow, 1f) };
            lineRenderer.colorGradient = gradientYellow;
        }
        else if (distance > 6f) //Long Distance
        {
            // Use dashed material
            lineRenderer.material = dashedMaterial;
            // Repeat the dashed pattern
            lineRenderer.textureMode = LineTextureMode.Tile;

            startDeathCounter = true;
            Debug.Log(deathCounter);


        }

        if (startDeathCounter)
        {
            deathCounter += Time.deltaTime;
            if (deathCounter > 3f && distance > 6f)
            {
                StartCoroutine(DeathSequance());
            }
            else if (distance < 6f)
            {
                deathCounter = 0f;
                startDeathCounter = false;
            }
        }
        IEnumerator DeathSequance()
        {
            Debug.Log("Death Sequance Started!");
            yield return new();
        }
    }
}
