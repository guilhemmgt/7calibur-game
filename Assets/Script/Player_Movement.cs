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
    private SpriteRenderer spriteRenderer;

    // Déplacement
    public int jump = 7; // Nombre de sauts restants
    private bool isBroken = false; // Épée brisée (plus de sauts)
    private bool canWallJump = true; // Peut walljump
    private float xInput = 0; // Input directionnel (-1, 0, 1)
    public bool is7Calibur = false;


    private void Awake () {
        body = GetComponent<Rigidbody2D> ();
        spriteRenderer = GetComponent<SpriteRenderer> ();
        player_system = GetComponent<Player_System> ();
    }

    private void Update () {
        isBroken = jump <= 0;

        if (body.velocity.x > 0f) {
            spriteRenderer.flipX = true;
        }
        if (body.velocity.x < 0f) {
            spriteRenderer.flipX = false;
        }
    }

    private void FixedUpdate () {
        body.velocity = new Vector2 (xInput * horizontalSlide, body.velocity.y);
    }

    public void OnFeetCollisionStay (Collision2D other) {
        // Si plus d'épée ou en fin de niveau, plus de saut possible
        if (isBroken || is7Calibur)
            return;

        // Jump si on rebondit sur une plateforme
        if (body.velocity.y <= 0 && (other.transform.tag == "Platform" || other.transform.tag == "GroundPlatform")) {
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
        body.velocity = new Vector2 (0, force);

        jump -= 1;
    }

    // Appelé par l'UI
    public void Move (int direction) {
        xInput = direction;
    }
}