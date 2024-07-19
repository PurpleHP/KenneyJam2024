using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRecorder : MonoBehaviour
{
    public Ghost ghost;
    private float timer;
    private float timeValue;

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

            timer = 0; 
        }
    }
}
