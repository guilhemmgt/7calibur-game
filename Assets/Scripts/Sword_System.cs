using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_System : MonoBehaviour {
    // Scripts
    private GameManager gameManager;

    public GameObject DropSword;

	private void Awake () {
        gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}

	private void OnTriggerStay2D (Collider2D other) {
        if (other.tag != "Player")
            return;
        
        Vector3 positionplayer = new Vector3 ();
        positionplayer.x = other.transform.position.x;
        positionplayer.y = other.transform.position.y;

        // Si on touche l'epee depuis le haut, on prend l'epee et on reinitialise nos sauts
        if (other.transform.position.y >= transform.position.y) {
            other.GetComponent<Player_Movement> ().jump = gameManager.initialJumpAmount;
            Destroy (gameObject);

            /*
            GameObject dropsword = Instantiate(DropSword, positionplayer, Quaternion.identity);
            Rigidbody2D dropsword_body = dropsword.GetComponent<Rigidbody2D>();
            dropsword_body.AddForce(new Vector2 (50, 50));
            */
        }
    }
}