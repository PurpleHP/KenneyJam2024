using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FinalScene : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField]
    TextMeshProUGUI txt;

    private bool showingTime = true;

    private PlayerMovement pr;
    private void Start()
    {
        pr = player.GetComponent<PlayerMovement>();
        txt.text = pr.finalTime;
    }

    private void Update()
    {
        
        if (PlayerPrefs.HasKey("Time") && showingTime)
        {
            float timeOnTheGame = PlayerPrefs.GetFloat("Time");
            string finalTime = FormatTime(timeOnTheGame);
            txt.text = "Total Time Played: " + finalTime;
            //showingTime = false; -> Enable for not updating
        }
    }
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        int milliseconds = Mathf.FloorToInt((time * 100F) % 100F);
        return string.Format("{0:00} m {1:00} s {2:00} ms", minutes, seconds, milliseconds);
    }
    public void ResetTimer()
    {
        if(PlayerPrefs.HasKey("Time"))
        {
            pr.timeOnTheGame = 0f;
            PlayerPrefs.DeleteKey("Time");
        }
    }
    
   
}
