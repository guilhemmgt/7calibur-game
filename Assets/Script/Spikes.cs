using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {
	private GameManager gameManager;

	private void Awake () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}

	private void OnTriggerStay2D (Collider2D other) {
		if (other.tag != "Player")
			return;

		if (other.transform.position.y >= transform.position.y) {
			gameManager.GameOver ();
		}
	}
}