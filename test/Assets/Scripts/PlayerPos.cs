using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{

    void Start()
    {
        transform.position = GM.instance.plPos;
    }

}
