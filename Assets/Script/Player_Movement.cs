using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {
    // References

    // Joueur
    private Player_System player_system;
    private Rigidbody2D body;

    // Constantes
    public float verticalBounce = 10f;
    public float verticalWallBounce = 10f;
    public float horizontalSlide = 10f;
    public int jump = 7;

    // Etats
    public bool isBroken = false;
    public bool canWallJump = true;

    // Variables
    private float xInput = 0;

    private void Awake () {
        body = GetComponent<Rigidbody2D> ();
        player_system = GetComponent<Player_System> ();
    }

    private void Update () {
        // Etats
        if (jump <= 0) {
            isBroken = true;
        } else {
            isBroken = false;
        }
    }

    private void FixedUpdate () {
        body.velocity = new Vector2 (xInput * horizontalSlide, body.velocity.y);
    }

    private void OnCollisionEnter2D (Collision2D other) {
        if (isBroken || player_system.is7Calibur)
            return;

        // Jump
        if (other.relativeVelocity.y >= 0f && other.transform.GetComponent<Platform_System> () != null) {
            Jump (verticalBounce);
            canWallJump = true;
        }

        // Wall Jump
        if (other.gameObject.tag == "Wall" && canWallJump) {
            Jump (verticalWallBounce);
            canWallJump = false;
        }
    }

    private void Jump (float force) {
        body.AddForce (new Vector2 (0f, force), ForceMode2D.Impulse);

        jump -= 1;
    }

    public void Move (int direction) {
        xInput = direction;
    }
}