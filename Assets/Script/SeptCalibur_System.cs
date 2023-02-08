using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeptCalibur_System : MonoBehaviour {

    // References
    // Terrain
    public GameObject TopPlatform;
    // Dragon
    private Dragon_System dragon_system;
    // GameManager
    private GameManager gameManager;

    private void Awake () {
        gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
        dragon_system = gameManager.Dragon.GetComponent<Dragon_System> ();
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag != "Player")
            return;

        if (other.GetComponent<Rigidbody2D> ().velocity.y <= 0) {
            Player_System player_sys = other.GetComponent<Player_System> ();

            // Etats
            player_sys.is7Calibur = true;

            // Dragon
            dragon_system.transform.position = gameManager.posDragon;

            // Destroy Terrain
            Destroy (this.gameObject);
        }
    }
}