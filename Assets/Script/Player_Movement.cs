using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {
    // Constantes
    public float verticalBounce; // Force du saut
    public float verticalWallBounce; // Force du walljump
    public float horizontalSlide; // Vitesse du déplacement

    // Composants
    private Player_System player_system;
    private Rigidbody2D body;

    // Déplacement
    public int jump = 7; // Nombre de sauts restants
    private bool isBroken = false; // Épée brisée (plus de sauts)
    private bool canWallJump = true; // Peut walljump
    private float xInput = 0; // Input directionnel (-1, 0, 1)


    private void Awake () {
        body = GetComponent<Rigidbody2D> ();
        player_system = GetComponent<Player_System> ();
    }

    private void Update () {
        isBroken = jump <= 0;
    }

    private void FixedUpdate () {
        body.velocity = new Vector2 (xInput * horizontalSlide, body.velocity.y);
    }

    private void OnCollisionEnter2D (Collision2D other) {
        // Si plus d'épée ou en fin de niveau, plus de saut possible
        if (isBroken || player_system.is7Calibur)
            return;

        // Jump si on rebondit sur une plateforme
        if (other.relativeVelocity.y >= 0f && other.transform.GetComponent<Platform_System> () != null) {
            Jump (verticalBounce);
            canWallJump = true;
        }

        // Wall Jump si on touche un mur
        if (other.gameObject.tag == "Wall" && canWallJump) {
            Jump (verticalWallBounce);
            canWallJump = false;
        }
    }

    private void Jump (float force) {
        //body.AddForce (new Vector2 (0f, force), ForceMode2D.Impulse);
        body.velocity = new Vector2 (0, force);

        jump -= 1;
    }

    // Appelé par l'UI
    public void Move (int direction) {
        xInput = direction;
    }
}