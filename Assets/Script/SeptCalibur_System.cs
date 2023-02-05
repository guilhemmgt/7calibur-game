using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeptCalibur_System : MonoBehaviour {
    // References
    // Terrain
    public GameObject TopPlatform;
    // Dragon
    private GameObject Dragon;
    private Dragon_System dragon_system;
    // Joueur
    private Player_System player_system;
    // GameManager
    private GameManager gameManager;

    private void Awake () {
        gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
        dragon_system = gameManager.Dragon.GetComponent<Dragon_System> ();
        player_system = gameManager.player.GetComponent<Player_System> ();
        Dragon = GameObject.Find ("Dragon");
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.tag == "Player") {
            // Etats
            dragon_system.isAttacking = false;
            player_system.is7Calibur = true;

            // Destroy Terrain
            Destroy (TopPlatform, 1f);
            var Grounds = GameObject.FindGameObjectsWithTag ("GroundPlatform");
            foreach (var Ground in Grounds) {
                Destroy (Ground);
            }

            // Dragon
            Dragon.transform.position = gameManager.posDragon;
        }

    }
}