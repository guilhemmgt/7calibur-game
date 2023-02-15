using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeptCalibur_System : MonoBehaviour {
    // Scripts
    private GameManager gameManager;
    private Player_Movement player_mvt;

    // Composants
    private Animator animator;

    private void Awake () {
        gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
        player_mvt = gameManager.player.GetComponent<Player_Movement> ();

        animator = GetComponent<Animator> ();
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag != "Player")
            return;

        // Quand on touche le rocher, on est 7calibur
        player_mvt.is7Calibur = true;

        // Retire l'epee
        animator.SetBool("isEmpty", true);
    }
}