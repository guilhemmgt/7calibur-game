using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_System : MonoBehaviour {
    // Reference 
    // Joueur
    private Player_System player_System;
    // GameManager
    private GameManager gameManager;

    // Constantes
    public float speed;


    private void Awake () {
        gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
        player_System = gameManager.player.GetComponent<Player_System> ();
    }


    private void Update () {
        if (!player_System.is7Calibur) {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag != "Player")
            return;

        if (player_System.is7Calibur) {
            gameManager.Same_Tower (); // Slain
        } else {
            player_System.isOver = true; // Game Over
        }
    }
}