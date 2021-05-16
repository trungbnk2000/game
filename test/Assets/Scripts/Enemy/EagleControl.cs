using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleControl : Enemy
{
    private float upCap;
    private float downCap;
    [SerializeField] private float speed = 3f;
    private bool movingUp = true;

    protected override void Start()
    {
        base.Start();
        rb.bodyType = RigidbodyType2D.Kinematic;
        upCap = transform.position.y + 3f;
        downCap = transform.position.y - 3f;
    }

    private void Update()
    {
        if (!killed)
        {
            Move();
        }
    }

    private void Move()
    {
        if (movingUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if(transform.position.y > upCap)
            {
                movingUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if (transform.position.y < downCap)
            {
                movingUp = true;
            }
        }

    }
}
