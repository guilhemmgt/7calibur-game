using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;
     
    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}

