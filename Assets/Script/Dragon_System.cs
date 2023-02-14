using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_System : MonoBehaviour {
    // Scripts
    private Player_Movement player_mvt;
    private GameManager gameManager;

    // Vitesse du dragon
    public float speed;


    private void Awake () {
        gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
        player_mvt = gameManager.player.GetComponent<Player_Movement> ();
    }


    private void Update () {
        if (!player_mvt.is7Calibur) {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag != "Player")
            return;

        if (player_mvt.is7Calibur) {
            gameManager.ReplayAfterWin (); // Slain
        } else {
            gameManager.GameOver (); // Game Over
        }
    }
}