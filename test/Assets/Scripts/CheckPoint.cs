using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Animator ani;

    private void Start()
    {
        ani = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            ani.SetBool("saved", true);
            GM.instance.plPos = transform.position;
        }
    }
}
