using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int time = 3;
    private Vector3 enemyPo;

    protected Animator ani;
    protected Rigidbody2D rb;
    protected Collider2D co;
    protected bool killed = false;

    protected virtual void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        co = GetComponent<Collider2D>();

        rb.bodyType = RigidbodyType2D.Dynamic;
        co.enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        enemyPo = transform.position;
    }

    public void jumpedOn()
    {
        killed = true;
        rb.bodyType = RigidbodyType2D.Static;
        co.enabled = false;
        SoundManager.instance.Play("EnemyDead");
        ani.SetTrigger("dead");
    }

    private void Dead()
    {
        GetComponent<SpriteRenderer>().enabled = false;

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(time);
        Instantiate(gameObject, enemyPo, transform.rotation);
        Destroy(gameObject);
    }


}
