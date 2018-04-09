using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour {

    [SerializeField]
    private GameObject image1, image2;
    [SerializeField]
    private GameObject[] finalImage;
    [SerializeField]
    private AudioClip clip1, clip2, clip3;

    private AudioSource audioSource;
    private bool clip1Played, clip2Played, clip3played = false;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        image1.SetActive(true);
        image2.SetActive(false);
        foreach (GameObject part in finalImage)
        {
            part.SetActive(false);
        }
        audioSource.clip = clip1;
        audioSource.Play();
        clip1Played = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (!audioSource.isPlaying && clip1Played && !clip2Played && !clip3played)
        {
            image1.SetActive(false);
            image2.SetActive(true);
            audioSource.clip = clip2;
            audioSource.Play();
            clip2Played = true;
        }
        else if (!audioSource.isPlaying && clip1Played && clip2Played &&!clip3played)
        {
            image2.SetActive(false);
            foreach (GameObject part in finalImage)
            {
                part.SetActive(true);
            }
            audioSource.clip = clip3;
            audioSource.Play();
            clip3played = true;
        }
    }
}
