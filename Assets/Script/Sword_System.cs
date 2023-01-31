using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_System : MonoBehaviour
{
    // References
    // Joueur
    private GameObject Player;
    private Player_Movement player_movement;

    void Awake()
    {
       Player = GameObject.FindWithTag("Player"); 
       player_movement = Player.GetComponent<Player_Movement>();
    }

    // Refill
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            player_movement.jump = 7;  
        }
    }
}


