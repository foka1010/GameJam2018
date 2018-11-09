using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarekController : MonoBehaviour
{
    Rigidbody2D marekRb;
    public float jumpForce = 10f;
    public float speed = 5f;

    public Transform groundCheck;
    public Transform gunPosition;
    public LayerMask whatIsGround;
    bool grounded = false;
    bool justGrounded = false;

    public GameObject apple;
    public GameObject Shooo;
    public GameObject jumpEffect;

    void Start()
    {
        marekRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        DoJump();
        ShootApple();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void DoJump()
    {
        grounded = Physics2D.OverlapPoint(groundCheck.position, whatIsGround);

        if (Input.GetButtonDown("Jump"))
        {
            if(grounded)
            {
                Instantiate(jumpEffect, groundCheck.position, Quaternion.identity);
                marekRb.velocity = new Vector2(marekRb.velocity.x, jumpForce);
            }
        }

        if(grounded)
        {
            if(justGrounded == false)
            {
                justGrounded = true;
                Instantiate(jumpEffect, groundCheck.position, Quaternion.identity);
            }
        }
        else
        {
            justGrounded = false;
        }
    }

    void Move()
    {
        marekRb.velocity = new Vector2(speed, marekRb.velocity.y);
    }

    void ShootApple()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(Shooo, gunPosition.position, Quaternion.identity);
            GameObject instance = Instantiate(apple, gunPosition.position, Quaternion.identity);
            instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(1000, 200));
        }
    }
}
