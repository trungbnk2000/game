using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public int heart;
    private int numOfHeart = 3;

    [SerializeField] private Image[] heartSpirte;

    private void Start()
    {
        heart = numOfHeart;
    }

    private void Update()
    {
        for(int i=0; i< numOfHeart; i++)
        {
            if (i < heart)
                heartSpirte[i].enabled = true;
            else
                heartSpirte[i].enabled = false;
        }
    }
}
