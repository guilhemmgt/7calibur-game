using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_System : MonoBehaviour {

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag != "Player")
            return;

        if (other.GetComponent<Rigidbody2D> ().velocity.y <= 0) {
            other.GetComponent<Player_Movement> ().jump = 7;

            Destroy (gameObject);
        }
    }
}