using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeptCalibur_System : MonoBehaviour {

    // References
    // Composants
    private Animator animator;
    // Terrain
    public GameObject TopPlatform;
    // Dragon
    private Dragon_System dragon_system;
    // GameManager
    private GameManager gameManager;

    private void Awake () {
        gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
        dragon_system = gameManager.Dragon.GetComponent<Dragon_System> ();
        animator = GetComponent<Animator> ();
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag != "Player")
            return;

        Player_Movement player_mvt = other.GetComponent<Player_Movement> ();

        // Etats
        player_mvt.is7Calibur = true;

        // Dragon
        dragon_system.transform.position = gameManager.posDragon;

        // L'epee s'enleve
        animator.SetBool("isEmpty", true);
    }
}