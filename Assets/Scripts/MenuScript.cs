using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Image blackImage;
    [SerializeField] private Animator anim;
    private void Awake()
    {
        blackImage.raycastTarget = false;

        if(!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    // If running in a built application
        Application.Quit();
#endif
    }
  
    IEnumerator LoadLevel(int buildIndex)
    {
        PlayerPrefs.SetInt("Level", 1);
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => Math.Abs(blackImage.color.a - 1) < 0.005f);
        SceneManager.LoadScene(buildIndex); 
        
    }

    public void MainMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    
    public void StartGame() //Level select yüzünde Level İ -> build index = İ + 2 (Level1 -> build index 3)
    {
        StartCoroutine(LoadLevel(1)); //Load level select
    }
    public void Tutorial()
    {
        StartCoroutine(LoadLevel(2));

    }
    public void Level1()
    {
        StartCoroutine(LoadLevel(3));

    }
    public void Level2()
    {
        StartCoroutine(LoadLevel(4));
    }
    public void Level3()
    {
        StartCoroutine(LoadLevel(5));

    }
    public void Level4()
    {
        StartCoroutine(LoadLevel(6));

    }
    public void Level5()
    {
        StartCoroutine(LoadLevel(7));

    }
    public void Level6()
    {
        StartCoroutine(LoadLevel(8));

    }
    public void Level7()
    {
        StartCoroutine(LoadLevel(9));

    }
    public void Level8()
    {
        StartCoroutine(LoadLevel(10));

    }
}
