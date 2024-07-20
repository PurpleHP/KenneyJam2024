using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRecorder : MonoBehaviour
{
    public Ghost ghost;
    private float timer;
    private float timeValue;

    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if (ghost.isRecord)
        {
            ghost.ResetData();
            timeValue = 0;
            timer = 0;
        }
    }
    private void FixedUpdate()
    {
        timer += Time.unscaledDeltaTime;
        timeValue += Time.unscaledDeltaTime;

        if(ghost.isRecord & timer >= 1/ghost.recordFrequancy)
        {
            ghost.timeStamp.Add(timeValue);
            ghost.position.Add(this.transform.position);
            ghost.scale.Add(this.transform.localScale);
            if (anim.GetBool("isWalking"))
            {
                ghost.animation.Add("isWalking");
            }
            else if (anim.GetBool("isJumping"))
            {
                ghost.animation.Add("isJumping");
            }
            else if (anim.GetBool("isSprintJumping"))
            {
                ghost.animation.Add("isSprintJumping");
            }
            else
            {
                ghost.animation.Add("isIdle");
            }
           
                
            timer = 0; 
        }
    }
}
