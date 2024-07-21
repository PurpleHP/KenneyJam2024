using System.Collections;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Transform targetTransform;
    bool isTeleported = false;
    public float teleportDelay = 2.0f; // Set your desired delay in seconds

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTeleported)
        {
            isTeleported = true;
            StartCoroutine(TeleportWithDelay(collision.transform));
        }
    }

    private IEnumerator TeleportWithDelay(Transform playerTransform)
    {
        yield return new WaitForSeconds(teleportDelay);
        playerTransform.position = targetTransform.position;
        isTeleported = false; // Reset the flag after teleportation
    }
}
