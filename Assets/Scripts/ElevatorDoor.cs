using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElevatorDoor : MonoBehaviour {

    [SerializeField]
    private float elevatorOpenSpeed = 0.5f;
    [SerializeField]
    private bool left = false;

    public IEnumerator Elevator()
    {
        yield return new WaitForSeconds(1.5f);
        float openSpeed = 0f;
        while (openSpeed <= 1.8f)
        {
            openSpeed += Time.deltaTime * elevatorOpenSpeed;
            if (left)
                transform.localPosition = new Vector3(openSpeed, 0, 0);
            else
                transform.localPosition = new Vector3(-openSpeed, 0, 0);
        }
        yield return new WaitForSeconds(5f);
        openSpeed = 1.8f;
        while (openSpeed >= 0f)
        {
            openSpeed -= Time.deltaTime * elevatorOpenSpeed;
            if (left)
                transform.localPosition = new Vector3(openSpeed, 0, 0);
            else
                transform.localPosition = new Vector3(-openSpeed, 0, 0);
        }
        GameObject.Find("Player").GetComponent<PlayerController>().buttonPressed = false;
    }
}
