using System.Collections;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Transform targetTransform;
    bool isTeleported = false;
    public float teleportDelay = 2.0f; // Set your desired delay in seconds
    [SerializeField] private PlayerMovement player;
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player.portalTime == 0)
        {
            collision.transform.position = targetTransform.position;
            player.portalTime = teleportDelay;
            //StartCoroutine(TeleportWithDelay(collision.transform));
        }
    }

    private IEnumerator TeleportWithDelay(Transform playerTransform)
    {
        yield return new WaitForSeconds(teleportDelay);
        isTeleported = false; // Reset the flag after teleportation
    }
}
