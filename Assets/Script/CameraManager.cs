using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    private Transform player;

	private void Awake () {
		player = GameObject.Find ("GameManager").GetComponent<GameManager> ().player.transform;
	}

	private void Update () {
		// La caméra suit la position y du joueur
		float yPos = player.position.y;
		transform.position = new Vector3 (0, yPos, -10);
	}
}