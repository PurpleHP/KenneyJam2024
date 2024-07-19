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
    [SerializeField] private Ghost ghost;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ghost.isRecord = false;
            Player.gameObject.transform.position = new Vector2(-10, -1);
            int levelID = SceneManager.GetActiveScene().buildIndex;
            if (PlayerPrefs.GetInt("Level") == 2)
            {
                PlayerPrefs.SetInt("Level", 1);
                SceneManager.LoadScene(levelID + 1);
                return;
            } 
            PlayerPrefs.SetInt("Level", 2);
            spawnReplay.LevelChanged();
            previousGrid.SetActive(false);
            currentGrid.SetActive(true);

        }
    }
}
