using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
	// Score que donne la piece
	public int scoreAmount;

	// Audio

    private AudioManager audiomanager;

    public GameObject gameManager;

	private void Awake() {
		gameManager = GameObject.Find ("GameManager");
        audiomanager = gameManager.GetComponent<AudioManager>();
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.tag != "Player")
			return;

		other.GetComponent<Player_System> ().AddScore (scoreAmount);
		Destroy (gameObject);

		audiomanager.CoinSound();
	}
}