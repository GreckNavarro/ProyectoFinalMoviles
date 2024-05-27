using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    Rigidbody rb;
    public Joystick joystick;
    Vector3 movement;


    void Start()
    {
       rb = GetComponent<Rigidbody>();  
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(joystick.Horizontal, rb.velocity.y, joystick.Vertical);
        rb.velocity = movement * speed;
    }
}
