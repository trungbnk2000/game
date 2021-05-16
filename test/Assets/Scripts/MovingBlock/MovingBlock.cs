using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{

    Rigidbody2D rb = new Rigidbody2D();
    [SerializeField] private float cap = 3;
    [SerializeField] private float speed = 3;
    private float leftCap, rightCap;
    private bool goingLeft = true ; 

    private void Start()
    {
        leftCap = transform.position.x - cap;
        rightCap = transform.position.x + cap;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(gameObject.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if(goingLeft)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if(transform.position.x < leftCap)
            {
                goingLeft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (transform.position.x > rightCap)
            {
                goingLeft = true;
            }
        }
    }
}
