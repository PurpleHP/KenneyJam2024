using UnityEngine;

public class DynamicLine : MonoBehaviour
{
    public Transform player;
    public Transform playerGhost;
    public Material solidMaterial; // Assign this in the inspector with a solid line material
    public Material dashedMaterial; // Assign this in the inspector with the dashed line material
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        lineRenderer.SetPosition(0, player.position);
        lineRenderer.SetPosition(1, playerGhost.position);

        float distance = Vector3.Distance(player.position, playerGhost.position);

        if (distance > 5f)
        {
            // Use dashed material
            lineRenderer.material = dashedMaterial;
            // Set the texture mode to Tile to repeat the dashed pattern
            lineRenderer.textureMode = LineTextureMode.Tile;
        }
        else
        {
            // Use solid material
            lineRenderer.material = solidMaterial;
            // Set the texture mode to Stretch to ensure the solid line covers the entire length
            lineRenderer.textureMode = LineTextureMode.Stretch;
        }
    }
}
