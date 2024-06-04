using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float speedRotation;
    [SerializeField] Animator animator;
    [SerializeField] GameObject Player;
    [SerializeField] AutoShoot shooter;

    Rigidbody rb;
    public Joystick joystick;
    Vector3 movement;


    void Start()
    {
       rb = GetComponent<Rigidbody>();  
    }

    void Rotate()
    {

        Vector3 direction = Vector3.RotateTowards(Player.transform.forward, movement, speedRotation * Time.deltaTime, 0.0f);
        Player.transform.rotation = Quaternion.LookRotation(direction);

    }

    // Update is called once per frame
    void Update()
    {
        if(joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            animator.SetBool("Moving", true);
            movement = new Vector3(joystick.Horizontal, rb.velocity.y, joystick.Vertical);
            rb.velocity = movement * speed;    
        }
        else
        {
            rb.velocity = Vector3.zero;
            animator.SetBool("Moving", false);
        }
        if (shooter.Shooting == false)
        {
            Rotate();
        }
    }
}
