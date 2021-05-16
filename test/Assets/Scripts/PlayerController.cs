using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator ani;
    private Collider2D co;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Text cherryText;

    private float speed = 10f;
    private float jump = 30f;
    private float hurtForce = 30f;
    private float hurtTime = 0.7f;
    private int cherries = 0;
    private float time = 2f;

    private bool isGrounded;
    public Transform groundCheck;
    private float checkRadius = 0.3f;

    public Transform enemyDetection;
    public float radiusX = 0;
    public float radiusY = 0;
    public float angle = 90f;

    private enum State { idle, running, jumping, falling, hurting}
    private State state = State.idle;

    private void Start()
    {
        SoundManager.instance.Play("Background");
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        co = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if(state != State.hurting)
            Movement();
        AnimationState();
        ani.SetInteger("state", (int)state);
        if(state == State.falling)
        {
            Falling();
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == "Collectable")
        {
            SoundManager.instance.Play("CollecableSound");
            Destroy(collision.gameObject);
            cherries++;
            cherryText.text = cherries.ToString();
        }

        if ( collision.tag == "Sigh")
        {
            FindObjectOfType<Sign>().Enter();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            
            if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(hurtForce, rb.velocity.y);
            }

            HealthManager();
        }

        if (collision.gameObject.tag == "Fall" || collision.gameObject.tag == "Dead")
        {
            FindObjectOfType<Heart>().heart = 1;
            HealthManager();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Sigh")
        {
            FindObjectOfType<Sign>().Exit();
        }
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(speed * hDirection, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed * hDirection, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }

        if (Input.GetKey(KeyCode.R))
        {
            SoundManager.instance.Pause("Background");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
            
    }

    private void Falling()
    {
        if(state == State.falling)
        {
            Collider2D[] enemy = Physics2D.OverlapBoxAll(enemyDetection.position, new Vector2(radiusX, radiusY), angle, enemyLayer);
            foreach (Collider2D e in enemy)
            {
                e.GetComponent<Enemy>().jumpedOn();
                rb.velocity = new Vector2(rb.velocity.x, jump);
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (enemyDetection == null)
            return;
        Gizmos.DrawCube(enemyDetection.position, new Vector3(radiusX, radiusY, 0));
    }

    private void AnimationState()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if (state == State.hurting)
        {
            return;
        }
        else if (rb.velocity.y > 0)
        {
            state = State.jumping;
        }
        else if (rb.velocity.y < 0)
        {
            state = State.falling;
        }
        else if (hDirection != 0 )
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    private void HealthManager()
    {
        state = State.hurting;
        SoundManager.instance.Play("Hurt");
        FindObjectOfType<Heart>().heart -= 1;

        if (FindObjectOfType<Heart>().heart <= 0)
        {
            StartCoroutine(Dead());
        }
        else
       {
           StartCoroutine(Hurt());
       }
       
    }

    private IEnumerator Hurt()
    {
        yield return new WaitForSeconds(hurtTime);

        state = State.idle;
    }


    private IEnumerator Dead()
    {
        SoundManager.instance.Pause("Background");
        SoundManager.instance.Play("Dead");

        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Wark()
    {
        SoundManager.instance.Play("PlayerWark");
    }
}
