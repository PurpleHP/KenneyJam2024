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

    
    public void StartGame() //Level select yüzünde Level İ -> build index = İ + 2 (Level1 -> build index 3)
    {
        LoadLevel(1); //Load level select
    }

    public void Level1()
    {
        LoadLevel(3);

    }
    public void Level2()
    {
        LoadLevel(4);
    }
    public void Level3()
    {
        LoadLevel(5);

    }
    public void Level4()
    {
        LoadLevel(6);

    }
    public void Level5()
    {
        LoadLevel(7);

    }
    public void Level6()
    {
        LoadLevel(8);

    }
    public void Level7()
    {
        LoadLevel(9);

    }
    public void Level8()
    {
        LoadLevel(10);

    }
}
