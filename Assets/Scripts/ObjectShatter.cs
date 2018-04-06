using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShatter : MonoBehaviour {

    private bool hasCollided = false;
    public GameObject current, shatter;

    private GameObject player;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !hasCollided)
        {
            hasCollided = true;
            StartCoroutine(player.GetComponent<PlayerController>().DestroyObject(current, shatter));
        }
    }
}
