using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnReplay : MonoBehaviour
{

    [SerializeField] private Ghost ghostScriptable;
    [SerializeField] private GameObject ghostPlayer;
    private int sceneID;
    private void Awake()
    {
        /*
        if (PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level",2);
            int level = PlayerPrefs.GetInt("Level");
            if (level % 2 == 0) //asıl level, ghost kaydet
            {
                ghostPlayer.SetActive(false);
                ghostScriptable.isReplay = false;
                ghostScriptable.isRecord = true;
            }
            else
            {
                ghostPlayer.SetActive(true);
                ghostScriptable.isRecord = false;
                ghostScriptable.isReplay = true;
            }
        }
        */
        
        ghostPlayer.SetActive(false);
        ghostScriptable.ResetData();
        ghostScriptable.isReplay = false;
        ghostScriptable.isRecord = true;
       
    }

    public void LevelChanged()
    {
        /*
         * Debug.Log("Level Changed");
        if (PlayerPrefs.HasKey("Level"))
        {
            int level = PlayerPrefs.GetInt("Level");
            Debug.Log("Level: " + level);
            if (level % 2 == 0) //asıl level, ghost kaydet
            {
                ghostPlayer.SetActive(false);
                ghostScriptable.isReplay = false;
                ghostScriptable.isRecord = true;
            }
            else
            {
                ghostPlayer.SetActive(true);
                ghostScriptable.isRecord = false;
                ghostScriptable.isReplay = true;
            }
        }
         */
        ghostPlayer.SetActive(true);
        ghostScriptable.isRecord = false;
        ghostScriptable.isReplay = true;
    }
    
}
