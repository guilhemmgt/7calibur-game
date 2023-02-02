using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_System : MonoBehaviour {
    // Reference 
    // Joueur
    private Player_System player_System;
    // GameManager
    private GameManager gameManager;


    // Etats
    public bool isAttacking;

    // Constantes
    public float speed;


    private void Awake () {
        gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
        player_System = gameManager.player.GetComponent<Player_System> ();
    }


    private void Update () {
        if (isAttacking) {
            Move ();
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        // Game Over
        if (other.gameObject.tag == ("Player") && isAttacking) {
            player_System.isOver = true;
        }

        // Slain
        if (other.gameObject.tag == ("Player") && player_System.is7Calibur) {
            gameManager.Same_Tower ();
        }
    }

    private void Move () {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}