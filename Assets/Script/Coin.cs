using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
	// Score que donne la pièce
	public int scoreAmount;

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.tag != "Player")
			return;

		other.GetComponent<Player_System> ().AddScore (scoreAmount);
		Destroy (gameObject);
	}
}