using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiky : MonoBehaviour
{
    public GameObject Player;

    private Player_System player_System;

    
    void Start()
    {
        player_System = Player.GetComponent<Player_System>();
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == ("Player"))
        {
            //player_System.isOver = true;
        }
    }
}
