using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextLevel : MonoBehaviour
{
    [SerializeField] private GameObject previousGrid;
    [SerializeField] private GameObject currentGrid;
    [SerializeField] private GameObject Player;
    [SerializeField] private SpawnReplay spawnReplay;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //String levelID = SceneManager.GetActiveScene().name.Substring(7);
            if (PlayerPrefs.GetInt("Level") == 1)
            {
                PlayerPrefs.SetInt("Level", 2);
            }
            else
            {
                PlayerPrefs.SetInt("Level", 1);

            }
            Debug.Log("PlayerPrefs: " + PlayerPrefs.GetInt("Level"));
            spawnReplay.LevelChanged();
            Player.gameObject.transform.position = new Vector2(-10, -1);
            previousGrid.SetActive(false);
            currentGrid.SetActive(true);
        }
    }
}
