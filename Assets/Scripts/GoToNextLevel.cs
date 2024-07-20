using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToNextLevel : MonoBehaviour
{
    [SerializeField] private GameObject firstLevelGrid;
    [SerializeField] private GameObject secondLevelGrid;
    [SerializeField] private GameObject Player;
    [SerializeField] private SpawnReplay spawnReplay;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private Ghost ghost;
    [SerializeField] private Animator anim;
    [SerializeField] private Image blackImage;

    private PlayerMovement movementScript;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private static readonly int Fade = Animator.StringToHash("Fade");

    private void Awake()
    {
        blackImage.raycastTarget = false;
        movementScript = Player.GetComponent<PlayerMovement>();
        sr = Player.GetComponent<SpriteRenderer>();
        rb = Player.GetComponent<Rigidbody2D>();
        if (PlayerPrefs.GetInt("Level") == 2)
        {
            firstLevelGrid.SetActive(false);
            secondLevelGrid.SetActive(true);
        }
        else
        {
            firstLevelGrid.SetActive(true);
            secondLevelGrid.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int levelID = SceneManager.GetActiveScene().buildIndex;
            if (PlayerPrefs.GetInt("Level") == 2)
            {
                PlayerPrefs.SetInt("Level", 1);
                StartCoroutine(NextScene(levelID));
                return;
            } 
            PlayerPrefs.SetInt("Level", 2);
            StartCoroutine(SameScene());
        }
    }

    IEnumerator NextScene(int buildIndex)
    {
        movementScript.enabled = false;
        rb.velocity = Vector2.zero;
        sr.enabled = false; 
        anim.speed = 1f;
        anim.SetBool(Fade, true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(buildIndex + 1);
    }

    IEnumerator SameScene()
    {
        ghost.isRecord = false;
        movementScript.enabled = false;
        rb.velocity = Vector2.zero;
        sr.enabled = false;
        anim.SetBool(Fade, true);

        // Ensure animation state is updated
        yield return new WaitForEndOfFrame();

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (ghost.timeStamp.Count > 0)
        {
            if (ghost.timeStamp[0] > 1)
            {
                anim.speed = stateInfo.length * ghost.timeStamp[0] * 2;
            }
            else
            {
                anim.speed = 2f;
            }
        }

        yield return new WaitForSeconds(stateInfo.length / anim.speed);
        firstLevelGrid.SetActive(false);
        secondLevelGrid.SetActive(true);
        Player.transform.position = spawnPoint.transform.position;

        anim.SetBool(Fade, false);
        yield return new WaitForSeconds(stateInfo.length / anim.speed);
        yield return new WaitForEndOfFrame();
        spawnReplay.LevelChanged();
        sr.enabled = true;
        movementScript.enabled = true;
    }
}
