using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_System : MonoBehaviour {

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag != "Player")
            return;

        if (other.transform.position.y >= transform.position.y) {
            other.GetComponent<Player_Movement> ().jump = 7;

            Destroy (gameObject);
        }
    }
}