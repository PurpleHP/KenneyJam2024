using System.Collections;
using UnityEngine;

public class Ceiling : MonoBehaviour
{
    [SerializeField] private float moveDownSpeed = 1.2f;
    [SerializeField] private float shakeIntensity = 0.05f;
    [SerializeField] private float shakeDuration = 100f;

    private void Start()
    {
        // Start the coroutine for moving down while shaking
        StartCoroutine(MoveDownAndShake());
    }

    private IEnumerator MoveDownAndShake()
    {
        float elapsed = 0f;
        Vector2 originalPosition = transform.position;

        while (elapsed < shakeDuration)
        {
            // Shake effect
            float offsetX = Random.Range(-shakeIntensity, shakeIntensity);
            float offsetY = Random.Range(-shakeIntensity, shakeIntensity);

            // Move down and apply shake offset
            transform.position = new Vector2(originalPosition.x + offsetX, transform.position.y - (moveDownSpeed * Time.deltaTime) + offsetY);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is properly adjusted after shaking ends
        transform.position = new Vector2(originalPosition.x, transform.position.y);
    }
}