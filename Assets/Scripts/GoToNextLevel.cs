using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToNextLevel : MonoBehaviour
{
    [SerializeField] private GameObject previousGrid;
    [SerializeField] private GameObject currentGrid;
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
        movementScript = Player.GetComponent<PlayerMovement>();
        sr = Player.GetComponent<SpriteRenderer>();
        rb = Player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int levelID = SceneManager.GetActiveScene().buildIndex;
            if (PlayerPrefs.GetInt("Level") == 2) //leveli bitirdi. Sonraki level. Örnek 2.2 -> 3.1
            {
                PlayerPrefs.SetInt("Level", 1);
                StartCoroutine(NextScene(levelID));
                SceneManager.LoadScene(levelID + 1);
                return;
            } 
            PlayerPrefs.SetInt("Level", 2);
            StartCoroutine(SameScene());

        }
    }

    IEnumerator NextScene(int buildIndex)
    {
        anim.SetBool(Fade,true);
        yield return new WaitUntil(()=> Math.Abs(blackImage.color.a - 1) < 0.0001f);
        SceneManager.LoadScene(buildIndex + 1);
    }
    
    IEnumerator SameScene()
    {
        ghost.isRecord = false;
        movementScript.enabled = false;
        rb.velocity = new Vector2(0,0);
        sr.enabled = false;
        anim.SetBool(Fade,true);
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (ghost.timeStamp.Count > 0)
        {
            if (ghost.timeStamp[0] > 1)
            {
                anim.speed = stateInfo.length * ghost.timeStamp[0] * 2;
            }
            else
            {
                anim.speed = 0.5f;
            }

        }
        yield return new WaitForSeconds(stateInfo.length / anim.speed);
        currentGrid.SetActive(true);
        Player.gameObject.transform.position = spawnPoint.gameObject.transform.position;
        previousGrid.SetActive(false);
        anim.SetBool(Fade,false);
        yield return new WaitForSeconds(stateInfo.length / anim.speed);
        sr.enabled = true;
        movementScript.enabled = true;
        spawnReplay.LevelChanged();

    }
}
