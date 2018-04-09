using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour {

    
    public AudioClip drumstick, song, elevator;

    private AudioSource audioSource;
    private int stickCount = 0;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = drumstick;
        audioSource.Play();
        stickCount++;
	}
	
	// Update is called once per frame
	void Update () {
        if (!audioSource.isPlaying && stickCount < 4)
        {
            audioSource.Play();
            stickCount++;
        }
        else if (!audioSource.isPlaying && stickCount == 4)
        {
            audioSource.clip = song;
            audioSource.loop = true;
            audioSource.Play();
            stickCount++;
        }
    }
}
