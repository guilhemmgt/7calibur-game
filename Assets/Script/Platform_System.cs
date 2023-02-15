using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_System : MonoBehaviour {
    // Scripts
    private Player_Movement player_mvt;

    // Composants
    private Collider2D coll;

    private void Awake () {
        coll = GetComponent<EdgeCollider2D> ();
        player_mvt = GameObject.Find ("GameManager").GetComponent<GameManager> ().player.GetComponent<Player_Movement> ();
    }

    private void Update () {
        // En 7calibur, les plateformes ne font plus de collision
        coll.isTrigger = player_mvt.is7Calibur;
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag != "Player")
            return;

        // En 7calibur, le joueur d�truit les plateformes
        if (player_mvt.is7Calibur) {
            Destroy (gameObject);
        }
    }
}