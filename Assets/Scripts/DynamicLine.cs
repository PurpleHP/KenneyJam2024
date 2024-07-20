using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DynamicLine : MonoBehaviour
{
    public Transform player;
    public Transform playerGhost;
    public Material solidMaterial; // Assign this in the inspector with a solid line material
    public Material dashedMaterial; // Assign this in the inspector with the dashed line material
    private LineRenderer lineRenderer;
    private bool startDeathCounter;
    private float deathCounter;
    public AudioClip deathSound;
    public float deathTime = 2.5f;
    [SerializeField] private SpawnReplay ghostHandler;
    private AudioSource audioSource;

    
    [SerializeField] private Animator anim;
    [SerializeField] private Image  blackImage;
    void Awake()
    {
        blackImage.raycastTarget = false;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        audioSource = GetComponent<AudioSource>(); // Initialize audioSource
    }

    void Update()
    {
        if (ghostHandler.spriteIsEnabled)
        {
            UpdateLineRenderer();
            HandleDeathSequence();
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private void UpdateLineRenderer()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, player.position);
        lineRenderer.SetPosition(1, playerGhost.position);

        float distance = Vector3.Distance(player.position, playerGhost.position);
        UpdateLineColor(distance);
    }

    private void UpdateLineColor(float distance)
    {
        if (distance <= 3.2f)
        {
            SetLineProperties(solidMaterial, LineTextureMode.Stretch, Color.green, Color.green);
        }
        else if (distance <= 4f)
        {
            SetLineProperties(solidMaterial, LineTextureMode.Stretch, Color.green, Color.yellow);
        }
        else if (distance <= 4.7f)
        {
            SetLineProperties(solidMaterial, LineTextureMode.Stretch, Color.yellow, Color.yellow);
        }
        else if (distance <= 5.5f)
        {
            SetLineProperties(solidMaterial, LineTextureMode.Stretch, Color.yellow, Color.red);
        }
        else if (distance < 6f)
        {
            SetLineProperties(solidMaterial, LineTextureMode.Stretch, Color.red, Color.red);
        }
        else
        {
            SetLineProperties(dashedMaterial, LineTextureMode.Tile, Color.red, Color.red);
            HandleDeathSound();
        }
    }

    private void SetLineProperties(Material material, LineTextureMode textureMode, Color startColor, Color endColor)
    {
        lineRenderer.material = material;
        lineRenderer.textureMode = textureMode;
        Gradient gradient = new Gradient();
        gradient.colorKeys = new GradientColorKey[]
        {
            new GradientColorKey(startColor, 0f),
            new GradientColorKey(endColor, 1f)
        };
        lineRenderer.colorGradient = gradient;
    }

    private void HandleDeathSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(deathSound, 0.5f);
            startDeathCounter = true;
        }
    }

    private void HandleDeathSequence()
    {
        if (startDeathCounter)
        {
            deathCounter += Time.deltaTime;
            float distance = Vector3.Distance(player.position, playerGhost.position);

            if (deathCounter > deathTime && distance > 6f)
            {
                ghostHandler.spriteIsEnabled = false;
                StartCoroutine(DeathSequence());
            }
            else if (distance < 6f)
            {
                ResetDeathCounter();
            }
        }
    }

    private void ResetDeathCounter()
    {
        audioSource.Stop();
        deathCounter = 0f;
        startDeathCounter = false;
    }

    IEnumerator DeathSequence()
    {
        lineRenderer.enabled = false;
        Debug.Log("Death Sequence Started!");
        Instantiate(Resources.Load("ExplosionPrefab"), player.position, Quaternion.identity); // Assuming you have an ExplosionPrefab in your Resources folder
        Destroy(player.gameObject);
        anim.SetBool("Fade",true);
        
        yield return new WaitUntil(()=> blackImage.color.a == 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
