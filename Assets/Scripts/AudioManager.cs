using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource musicSource;
    [SerializeField] private AudioClip bgMusic;

    public static AudioManager instance;
    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        musicSource.clip = bgMusic;
        musicSource.Play();
    }
}
