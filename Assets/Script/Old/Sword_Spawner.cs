using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Spawner : MonoBehaviour
{
    // Spawn 
    public GameObject sword; // Entité à faire apparaîter

    public int spawn_number = 300;

    public float Min_H;
    public float Max_H;

    public float Min_L;
    public float Max_L;

    private int Side; // Droite[1] ou Gauche[0]

    void Start()
    {
        Vector3 spawn_position = new Vector3();

        for (int i = 0 ; i < spawn_number ; i++){

            Side = Random.Range(0, 2);

            if(Side == 1)
            {
                spawn_position.x = -Random.Range(Min_L , Max_L);
            }
            else
            {
                spawn_position.x = Random.Range(Min_L , Max_L);
            }

            spawn_position.y += Random.Range(Min_H , Max_H);
            

            GameObject new_Plateform = Instantiate(sword, spawn_position, Quaternion.identity);
        }
    }
}
