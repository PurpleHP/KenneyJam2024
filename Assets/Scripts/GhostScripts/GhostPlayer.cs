using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlayer : MonoBehaviour
{
    public Ghost ghost;
    private float timeValue;
    private int index1;
    private int index2;
    private Animator anim;
    
    private void Awake()
    {
        timeValue = 0;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        timeValue += Time.unscaledDeltaTime;

        if (ghost.isReplay)
        {
            Getindex();
            SetTransform();
            SetAnim();
        }
    }

    private void Getindex()
    {
        for(int i = 0; i < ghost.timeStamp.Count - 2; i++)
        {
            if (ghost.timeStamp[i] == timeValue)
            {
                index1 = i;
                index2 = i;
                return;
            }
            else if (ghost.timeStamp[i] < timeValue && timeValue < ghost.timeStamp[i + 1])
            {
                index1 = i;
                index2 = i + 1; 
                return;
            }
        }
        index1 = ghost.timeStamp.Count - 1;
        index2 = ghost.timeStamp.Count - 1; 
    }

    private void SetTransform()
    {
        if (index1 == index2)
        {
            this.transform.position = ghost.position[index1];
            SetAnimationState(ghost.animation[index1]);
        }
        else
        {
            float interpolationFactor = (timeValue - ghost.timeStamp[index1]) / (ghost.timeStamp[index2] - ghost.timeStamp[index1]);
            this.transform.position = Vector2.Lerp(ghost.position[index1], ghost.position[index2], interpolationFactor);
            
            // Interpolating between animation states
            string animState1 = ghost.animation[index1];
            string animState2 = ghost.animation[index2];
            SetInterpolatedAnimationState(animState1, animState2, interpolationFactor);
        }
    }

    private void SetAnim()
    {
        if (index1 == index2)
        {
            SetAnimationState(ghost.animation[index1]);
        }
        else
        {
            float interpolationFactor = (timeValue - ghost.timeStamp[index1]) / (ghost.timeStamp[index2] - ghost.timeStamp[index1]);
            string animState1 = ghost.animation[index1];
            string animState2 = ghost.animation[index2];
            SetInterpolatedAnimationState(animState1, animState2, interpolationFactor);
        }
    }

    private void SetAnimationState(string animState)
    {
        anim.SetBool("isWalking", animState == "isWalking");
        anim.SetBool("isJumping", animState == "isJumping");
        anim.SetBool("isSprintJumping", animState == "isSprintJumping");
        //anim.SetBool("isIdle", animState == "isIdle");
    }

    private void SetInterpolatedAnimationState(string animState1, string animState2, float interpolationFactor)
    {
        if (interpolationFactor < 0.5f)
        {
            SetAnimationState(animState1);
        }
        else
        {
            SetAnimationState(animState2);
        }
    }
}
