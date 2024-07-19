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
            int levelID = SceneManager.GetActiveScene().buildIndex;
            if (PlayerPrefs.GetInt("Level") == 2) //leveli bitirdi. Sonraki level. Örnek 2.2 -> 3.1
            {
                PlayerPrefs.SetInt("Level", 1);
                SceneManager.LoadScene(levelID + 1);
                return;
            } 
            PlayerPrefs.SetInt("Level", 2);
            spawnReplay.LevelChanged();
            Player.gameObject.transform.position = new Vector2(-10, -1);
            previousGrid.SetActive(false);
            currentGrid.SetActive(true);

        }
    }
}
