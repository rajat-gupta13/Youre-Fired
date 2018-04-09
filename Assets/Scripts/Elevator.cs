using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    [SerializeField]
    private GameObject audioManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            audioManager.GetComponent<AudioSource>().clip = audioManager.GetComponent<Audio>().elevator;
            audioManager.GetComponent<AudioSource>().Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            audioManager.GetComponent<AudioSource>().clip = audioManager.GetComponent<Audio>().song;
            audioManager.GetComponent<AudioSource>().Play();
        }
    }
}
