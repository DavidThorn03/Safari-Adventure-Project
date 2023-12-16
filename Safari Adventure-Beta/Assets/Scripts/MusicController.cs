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
        musicSource.clip = music;
        musicSource.Play();
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(!gm.isGameActive){
            musicSource.Stop();
        }
    }
}
