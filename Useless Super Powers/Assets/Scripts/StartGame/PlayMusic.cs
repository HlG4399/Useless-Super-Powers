using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AudioSource sound;

    // Use this for initialization
    void Start()
    {
        sound = this.GetComponent<AudioSource>();
    }

    public void playMusic()
    {
        sound.Play();
    }
}
