using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : Enemy
{
    private float LeftCap;
    private float RightCap;
    private bool FacingLeft = true;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpLength = 5f;
    [SerializeField] private float cap = 3f;
    private int pushExOther = 4;
    private bool jumping = false;
    private bool falling = false;

    [SerializeField] private LayerMask ground;


    protected override void Start()
    {
        base.Start();
        LeftCap = transform.position.x - cap;
        RightCap = transform.position.x + cap;
    }

    private void Update()
    {
        ani.SetBool("falling", falling);
        ani.SetBool("jumping", jumping);
        animationControl();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {

            if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-pushExOther, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(pushExOther, rb.velocity.y);
            }
        }
    }

    private void animationControl()
    {
        if (ani.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                jumping = false;
                falling = true;
            }
        }
        if (ani.GetBool("falling"))
        {
            if (co.IsTouchingLayers(ground))
            {
                jumping = false;
                falling = false;
            }
        }
    }

    private void Move()
    {
        if (FacingLeft)
        {
            if (transform.position.x > LeftCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }

                if (co.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpForce);
                    jumping = true;
                    falling = false;
                }
            }
            else
            {
                FacingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < RightCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }

                if (co.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpForce);
                    jumping = true;
                    falling = false;
                }
            }
            else
            {
                FacingLeft = true;
            }
        }
    }

    
}
