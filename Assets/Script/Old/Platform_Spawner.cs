using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Spawner : MonoBehaviour
{

    // Spawn 
    public GameObject platform; // Entité à faire apparaître

    public int spawn_number = 300;

    public float Min_H;
    public float Max_H;

    public float Min_L;
    public float Max_L;

    void Start()
    {
        Vector3 spawn_position = new Vector3();

        for (int i = 0 ; i < spawn_number ; i++)
        {
            spawn_position.y += Random.Range(Min_H , Max_H);
            spawn_position.x = Random.Range(Min_L , Max_L);

            GameObject new_Plateform = Instantiate(platform, spawn_position, Quaternion.identity);
        }
    }
}

