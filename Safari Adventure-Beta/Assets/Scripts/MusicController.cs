using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip music;
    public AudioSource musicSource;

    private GameManager gm;

    void Start()
    {
        musicSource.clip = music;//set music clip
        musicSource.Play();//play music
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();//find game manager
    }

    void Update()
    {
        if(!gm.isGameActive){
            musicSource.Stop();//stop music when game is over
        }
    }
}