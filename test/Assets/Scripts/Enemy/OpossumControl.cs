using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumControl : Enemy
{
    private float speed = 5;
    private bool facingLeft;
    
    protected override void Start()
    {
        transform.localScale = new Vector3(1,1,1);
        base.Start();
        facingLeft = true;
    }

    private void Update()
    {
        if (!killed)
        {
            Move();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            facingLeft = !facingLeft;
        }
    }

    private void Move()
    {
        if (facingLeft)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }
}
