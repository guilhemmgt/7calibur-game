using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {
    // Constantes
    public float verticalBounce; // Force du saut
    public float verticalWallBounce; // Force du walljump
    public float horizontalSlide; // Vitesse du déplacement

    // Composants
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Déplacement
    public int jump; // Nombre de sauts restants
    public bool isBroken = false; // N'a plus de saut ?
    private bool canWallJump = true; // Peut walljump ?
    public bool is7Calibur = false; // En 7calibur ?

    // Input
    private float xInput = 0; // Input directionnel (-1, 0, 1)
    private bool leftPressed = false; // Bouton gauche pressé ?
    private bool rightPressed = false; // Bouton droit pressé ?

    // Audio

    private AudioManager audiomanager;
    public GameObject gameManager;

    private void Awake () {
        gameManager = GameObject.Find ("GameManager");
        body = GetComponent<Rigidbody2D> ();
        spriteRenderer = GetComponent<SpriteRenderer> ();
        animator = GetComponent<Animator> ();
        audiomanager = gameManager.GetComponent<AudioManager>();
    }

    private void Update () {

        // animation 7calibur
        animator.SetBool ("is7calibur", is7Calibur);

        // Vérifie le nb de sauts du joueur
        isBroken = jump <= 0;
        animator.SetBool ("isBroken", isBroken);

        // Flip selon la direction du déplacement
        if (body.velocity.x > 0f) {
            spriteRenderer.flipX = true;
        }
        if (body.velocity.x < 0f) {
            spriteRenderer.flipX = false;
        }
        
        // Controles claviers
        if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.Q))
            PressLeft ();
        if (Input.GetKeyUp (KeyCode.LeftArrow) || Input.GetKeyUp (KeyCode.Q))
            ReleaseLeft ();
        if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D))
            PressRight ();
        if (Input.GetKeyUp (KeyCode.RightArrow) || Input.GetKeyUp (KeyCode.D))
            ReleaseRight ();
    }

    private void FixedUpdate () {
        // Déplacement horizontal
        body.velocity = new Vector2 (xInput * horizontalSlide, body.velocity.y);
    }

    // Fonction OnCollisionStay2D de Feet
    public void OnFeetCollisionStay (Collision2D other) {
        if (isBroken || is7Calibur)
			return;

		// Jump si on rebondit sur une plateforme
		if (body.velocity.y <= 0 && (other.transform.tag == "Platform" || other.transform.tag == "GroundPlatform")) {
			Jump (verticalBounce);
			canWallJump = true;
		}

        // Ne peut plus walljump si arrivé en haut de la tour
		if (other.transform.tag == "TopPlatform") {
            canWallJump = false;
        }
    }

    private void OnCollisionStay2D (Collision2D other) {
        if (isBroken || is7Calibur)
            return;

        // Wall Jump si on touche un mur
        if (other.gameObject.tag == "Wall" && canWallJump) {
            Jump (verticalWallBounce);
            canWallJump = false;
        }
    }

	private void Jump (float force) {
        // Vélocité
        body.velocity = new Vector2 (0, force);
        // Décompte du nb de sauts
        jump -= 1;
        // Animation
        animator.SetTrigger ("Jump");
        // Audio 
        audiomanager.JumpSound();
    }

    // Gestion de l'input gauche
    public void PressLeft () {
        xInput = -1;
        leftPressed = true;
    }
    public void ReleaseLeft () {
        leftPressed = false;
        if (!rightPressed)
            xInput = 0;
    }
    // Gestion de l'input droit
    public void PressRight () {
        xInput = 1;
        rightPressed = true;
    }
    public void ReleaseRight () {
        rightPressed = false;
        if (!leftPressed)
            xInput = 0;
    }

    // Réinitialiser l'input directionnel
    public void ResetMovement () {
        xInput = 0;
        rightPressed = false;
        leftPressed = false;
	}
}