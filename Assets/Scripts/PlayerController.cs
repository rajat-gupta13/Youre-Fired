using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;
    [SerializeField]
    private float radius = 1.0F;
    [SerializeField]
    private float power = 10.0F;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private Text damageText;
    [SerializeField]
    private Text timerText;

    public static int currentDamages = 0;
    [SerializeField]
    private float timer = 90f;
    //private int highestDamages;

    [SerializeField]
    private Transform objectHolder;
    [SerializeField]
    private float throwPower = 5f;
    [SerializeField]
    private Font textFont;

    public GameObject coinPrefab;
    public bool fullSpeed = false;

    private bool objectPicked = false;
    private bool buttonPressed = false;
    private GameObject pickedObject;
    // Component caching
    private PlayerMotor motor;

    void Start()
    {
        currentDamages = 0;
        damageText.font = textFont;
        timerText.font = textFont;
        damageText.text = "Damages: $" + currentDamages.ToString();
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = "Time Left: " + Mathf.Round(timer).ToString("00");

        if (timer <= 0)
        {
            SceneManager.LoadScene(3);
        }
        //if (PauseMenu.IsOn)
        //{
        //    if (Cursor.lockState != CursorLockMode.None)
        //        Cursor.lockState = CursorLockMode.None;

        //    motor.Move(Vector3.zero);
        //    motor.Rotate(Vector3.zero);
        //    motor.RotateCamera(0f);

        //    return;
        //}

        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        //Calculate movement velocity as a 3D vector
        float _xMov = Input.GetAxis("Horizontal");
        float _zMov = Input.GetAxis("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        // Final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical) * speed;

        if (_velocity.magnitude > 9)
            fullSpeed = true;
        else
            fullSpeed = false;

        //Apply movement
        motor.Move(_velocity);

        //Calculate rotation as a 3D vector (turning around)
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        //Apply rotation
        motor.Rotate(_rotation);

        //Calculate camera rotation as a 3D vector (turning around)
        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRot * lookSensitivity;

        //Apply camera rotation
        motor.RotateCamera(_cameraRotationX);

        if (Input.GetButtonDown("Fire1") && !objectPicked)
        {
            PickObject();
        }
        else if (Input.GetButtonDown("Fire1") && objectPicked)
        {
            ThrowObject();
        }
    }

    private void PickObject()
    {
        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, 3, mask))
        {
            if (_hit.collider.tag == "Interactable")
            {
                objectPicked = true;
                pickedObject = _hit.collider.gameObject;
                _hit.collider.gameObject.transform.position = objectHolder.position;
                _hit.collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
                _hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                _hit.collider.gameObject.transform.SetParent(objectHolder);
            }
            else if (_hit.collider.tag == "Button" && !buttonPressed)
            {
                Debug.Log("Button Presed");
            }
        }
    }

    private void ThrowObject()
    {
        pickedObject.transform.parent = null;
        objectPicked = false;
        pickedObject.GetComponent<Rigidbody>().useGravity = true;
        pickedObject.GetComponent<Rigidbody>().isKinematic = false;
        pickedObject.GetComponent<ObjectShatter>().isThrown = true;
        pickedObject.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwPower);
    }

    public IEnumerator DestroyObject(GameObject current, GameObject shatter)
    {
        shatter.transform.position = current.transform.position;
        currentDamages += current.GetComponent<ObjectShatter>().damageValue;
        if (currentDamages > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", currentDamages);
        }
        current.SetActive(false);
        shatter.SetActive(true);
        shatter.GetComponent<AudioSource>().Play();
        Vector3 explosionPos = shatter.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null && rb.gameObject.tag != "Player")
            {
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
                //Debug.Log("Force Added");
                rb.useGravity = true;
            }
        }
        int numberOfCoins = Random.Range(3, 9);
        for (int i = 0; i < numberOfCoins; i++)
        {
            Instantiate(coinPrefab, new Vector3(shatter.transform.position.x, shatter.transform.position.y + 0.5f, shatter.transform.position.z), Quaternion.identity);
        }
        StartCoroutine(DisableShardPhysics(shatter));
        damageText.text = "Damages: $" + currentDamages.ToString();
        yield return null;
    }

    private IEnumerator DisableShardPhysics(GameObject shatter)
    {
        yield return new WaitForSeconds(2.5f);
        foreach (Rigidbody rb in shatter.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
    }

}