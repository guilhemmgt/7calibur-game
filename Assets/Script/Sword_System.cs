using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_System : MonoBehaviour {
    // Scripts
    private GameManager gameManager;

	private void Awake () {
        gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}

	private void OnTriggerStay2D (Collider2D other) {
        if (other.tag != "Player")
            return;

        // Si on touche l'epee depuis le haut, on prend l'epee et on reinitialise nos sauts
        if (other.transform.position.y >= transform.position.y) {
            other.GetComponent<Player_Movement> ().jump = gameManager.initialJumpAmount;
            Destroy (gameObject);
        }
    }
}