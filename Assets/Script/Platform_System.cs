using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_System : MonoBehaviour {
    // Reference
    // Joueur
    private Player_System player_system;

    // Composants
    private Collider2D coll;

    private void Awake () {
        coll = GetComponent<EdgeCollider2D> ();
        player_system = GameObject.Find ("GameManager").GetComponent<GameManager> ().player.GetComponent<Player_System> ();
    }

    private void Update () {
        coll.isTrigger = player_system.is7Calibur;
    }

    // Score Descente
    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag != "Player")
            return;

        if (player_system.is7Calibur) {
            Destroy (this.gameObject);
        }
    }
}