using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    [SerializeField] GameObject text;

    public void Enter()
    {
        text.gameObject.SetActive(true);
    }

    public void Exit()
    {
        text.gameObject.SetActive(false);
    }
}
