using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeptCalibur_System : MonoBehaviour {
    // Scripts
    private GameManager gameManager;
    private Player_Movement player_mvt;

    // Sprites
    public Sprite Base;
    public Sprite Empty;
    bool isEmpty;
    private void Awake () {
        gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
        player_mvt = gameManager.player.GetComponent<Player_Movement> ();
    }

    private void Update() {
        if(isEmpty){
            GetComponent<SpriteRenderer>().sprite = Empty;
        }
        else{
            GetComponent<SpriteRenderer>().sprite = Base;
        }
    }
    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag != "Player")
            return;

        // Quand on touche le rocher, on est 7calibur
        player_mvt.is7Calibur = true;
        player_mvt.jump = gameManager.initialJumpAmount;

        // Retire l'epee
        isEmpty = true;
    }
}