using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarekController : MonoBehaviour
{
    Rigidbody2D marekRb;
    public float jumpForce = 10f;
    public float speed = 5f;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    bool grounded = false;

    void Start()
    {
        marekRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        DoJump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void DoJump()
    {
        grounded = Physics2D.OverlapPoint(groundCheck.position, whatIsGround);

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(grounded)
            {
                marekRb.velocity = new Vector2(marekRb.velocity.x, jumpForce);
            }
        }
    }

    void Move()
    {
        marekRb.velocity = new Vector2(speed, marekRb.velocity.y);
    }
}
