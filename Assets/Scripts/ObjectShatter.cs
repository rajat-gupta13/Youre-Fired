using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShatter : MonoBehaviour {

    private bool hasCollided = false;
    [HideInInspector]
    public bool isThrown = false;
    public GameObject current, shatter;
    public int damageValue = 50;

    private GameObject player;
    // Use this for initialization
    void Start()
    {
        isThrown = false;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Player") && !hasCollided)
        {
            Debug.Log("Player Collided with " + this.gameObject.name);
            hasCollided = true;
            StartCoroutine(player.GetComponent<PlayerController>().DestroyObject(current, shatter));
        }
        else if ((collision.gameObject.tag == "Wall") && isThrown && !hasCollided)
        {
            Debug.Log("Thrown on wall");
            hasCollided = true;
            StartCoroutine(player.GetComponent<PlayerController>().DestroyObject(current, shatter));
        }
        else if ((collision.gameObject.tag == "Floor") && isThrown && !hasCollided)
        {
            Debug.Log("Thrown on floor");
            hasCollided = true;
            StartCoroutine(player.GetComponent<PlayerController>().DestroyObject(current, shatter));
        }
    }

}
