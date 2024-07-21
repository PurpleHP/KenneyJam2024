using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] private Image blackImage;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject playerObject;
    private static readonly int Fade = Animator.StringToHash("Fade");

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private PlayerMovement movementScript;
    private void Awake()
    {
        rb = playerObject.GetComponent<Rigidbody2D>();
        sr = playerObject.GetComponent<SpriteRenderer>();
        movementScript = playerObject.GetComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ReloadScene());

        }

    }
    
    IEnumerator ReloadScene()
    {   

        Instantiate(Resources.Load("BloodPrefab"), playerObject.transform.position, Quaternion.identity); 
        movementScript.enabled = false;
        rb.velocity = Vector2.zero;
        sr.enabled = false; 
        anim.speed = 1f;
        anim.SetBool(Fade, true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
