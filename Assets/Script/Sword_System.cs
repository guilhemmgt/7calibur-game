using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_System : MonoBehaviour {
    // References
    // Joueur
    private Player_Movement player_movement;

    void Awake () {
        player_movement = GameObject.Find ("GameManager").GetComponent<GameManager> ().player.GetComponent<Player_Movement> ();
    }

    // Refill
    private void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.tag == "Player") {
            Destroy (gameObject);
            player_movement.jump = 7;
        }
    }
}