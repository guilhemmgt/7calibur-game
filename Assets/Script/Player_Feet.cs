using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Feet : MonoBehaviour {
    private Player_Movement player_mvt;

	private void Awake () {
        player_mvt = transform.parent.GetComponent<Player_Movement> ();
	}

	private void OnCollisionStay2D (Collision2D other) {
		// Pour + de simplicité, on transfère les instructions à Player_Movement
		player_mvt.OnFeetCollisionStay (other);
	}
}