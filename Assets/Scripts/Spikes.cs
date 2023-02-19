using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {
	// Scripts
	private Player_Movement player_mvt;
	private GameManager gameManager;

	private void Awake () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		player_mvt = gameManager.player.GetComponent<Player_Movement> ();
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.tag != "Player")
			return;

		// Si on arrive sur les piques depuis le haut, game over
		if (other.transform.position.y >= transform.position.y - 0.1f && other.attachedRigidbody.velocity.y <= 0 && !player_mvt.is7Calibur) {
			gameManager.GameOver ();
		}
	}
}