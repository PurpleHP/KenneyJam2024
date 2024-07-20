using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) //Restart whole level
        {
            RestartWholeLevel();

        }
        else if (Input.GetKeyDown(KeyCode.G)) //Restart ghost level (x.2)
        {
            RestartGhostLevel();
        }
    }

    public void RestartWholeLevel()
    {
        
        PlayerPrefs.SetInt("Level",1); //Reload scene animation buraya
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    
    }

    public void RestartGhostLevel()
    {
        if (PlayerPrefs.GetInt("Level") == 2)
        {
            PlayerPrefs.SetInt("Level",2); //Reload scene animation buraya
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Debug.Log("Not in 2nd Phase");
        }
    
    }
}
