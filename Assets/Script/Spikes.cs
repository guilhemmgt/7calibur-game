using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {
	private GameManager gameManager;

	// Scripts
	private Player_Movement player_mvt;

    // GameObjects
    public GameObject player;

	private void Awake () {
		player = GameObject.Find ("Player");
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		player_mvt = player.GetComponent<Player_Movement> ();
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.tag != "Player")
			return;

		if (other.transform.position.y >= transform.position.y && !player_mvt.is7Calibur) {
			gameManager.GameOver ();
		}
	}
}