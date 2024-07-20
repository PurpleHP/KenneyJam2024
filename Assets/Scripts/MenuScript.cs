using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private void Awake()
    {
        if(!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
        }
    }

  
    public void LoadLevel(int buildIndex)
    {
        PlayerPrefs.SetInt("Level", 1);
        SceneManager.LoadScene(buildIndex); 

    }

    public void MainMenu()
    {
        LoadLevel(0);
    }

    
    public void StartGame() //Level select yüzünde Level İ -> build index = İ + 1 (Level1 -> build index 2)
    {
        LoadLevel(1); //Load level select
    }

    public void Level1()
    {
        LoadLevel(2);

    }
    public void Level2()
    {
        LoadLevel(3);

    }
}
