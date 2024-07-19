using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Ghost : ScriptableObject
{
    public bool isRecord;
    public bool isReplay;
    public float recordFrequancy;

    public List<float> timeStamp;
    public List<Vector2> position;
    public void ResetData()
    {
        timeStamp.Clear();
        position.Clear();
    }
    
}   
