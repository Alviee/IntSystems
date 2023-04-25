using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject bouncingPlat;
    public AudioSource soundObject;

    private Rigidbody rb;
    private int count;
    private float movementX, movementY;
    
    // Start is called before the first frame update
    void Start()
    {
        //This sets the value of rb by getting a reference to the Rigidbody component attached to the sphere.
        rb = GetComponent<Rigidbody>();
        count = 0;
        transform.position = new Vector3(8.0f, 0.5f, -8.0f);

        SetCountText();
        winTextObject.SetActive(false);
        bouncingPlat.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {   
        //Gets the vector2 data from the movement value and stores it in a vector2 variable.
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 27)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        if (transform.position.y <= -15)
        {
            transform.position = new Vector3(8.0f, 0.5f, -8.0f);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

        }

        if (count == 26)
        {
            bouncingPlat.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            soundObject.Play();
            count = count + 1;
            SetCountText();
        }
    }


}
