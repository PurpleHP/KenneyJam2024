using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnReplay : MonoBehaviour
{

    [SerializeField] private Ghost ghostScriptable;
    [SerializeField] private GameObject ghostPlayer;
    [SerializeField] private GameObject spawnPoint;

    public bool spriteIsEnabled;
    
    private SpriteRenderer sr;
    private int sceneID;
    private void Awake()
    {
        sr = ghostPlayer.GetComponent<SpriteRenderer>();
        
        if (PlayerPrefs.HasKey("Level"))
        {
            if (PlayerPrefs.GetInt("Level") == 1) //asıl level, ghost kaydet şuanlık ghost yok
            {
                sr.enabled = false;
                spriteIsEnabled = false;
                ghostPlayer.SetActive(false);
                ghostScriptable.isReplay = false;
                ghostScriptable.isRecord = true;

            }
            else //ghost spawn olacak
            {
                ghostPlayer.SetActive(true);
                sr.enabled = true;
                spriteIsEnabled = true;
                ghostScriptable.isRecord = false;
                ghostScriptable.isReplay = true;
            }
        }
        
       
    }

    public void LevelChanged()
    {
        
        Debug.Log("Level Changed");
        if (PlayerPrefs.HasKey("Level"))
        {
            if (PlayerPrefs.GetInt("Level") == 1) //asıl level, ghost kaydet
            {
                sr.enabled = false;
                spriteIsEnabled = false;
                ghostPlayer.SetActive(false);
                ghostScriptable.isReplay = false;
                ghostScriptable.isRecord = true;
            }
            else
            {
                
                float startTime = ghostScriptable.timeStamp[0];
                Debug.Log("First Tİmestamp: " + startTime);
                StartCoroutine(ShowGhost(startTime)); 
           
            }
        }
        

    }

    IEnumerator ShowGhost(float startTime)
    {
        ghostScriptable.isRecord = false;
        ghostPlayer.SetActive(true);
        ghostPlayer.gameObject.transform.position = spawnPoint.gameObject.transform.position;
        yield return new WaitForSeconds(startTime);
        sr.enabled = true;
        spriteIsEnabled = true;
        ghostScriptable.isReplay = true;

    }
}
