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
    private InputActions inputActions;

    // Audio

    private AudioManager audiomanager;
    public GameObject gameManager;

    private void Awake() {
        gameManager = GameObject.Find("GameManager");
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audiomanager = gameManager.GetComponent<AudioManager>();
        inputActions = new InputActions();
    }
    
    private void OnEnable() {
        inputActions.Enable();
    }

    private void Update () {

        // animation 7calibur
        animator.SetBool ("is7calibur", is7Calibur);

        // Vérifie le nb de sauts du joueur
        isBroken = jump <= 0;
        animator.SetBool ("isBroken", isBroken);

        // Flip selon la direction du déplacement
        if (body.linearVelocity.x > 0f) {
            spriteRenderer.flipX = true;
        }
        if (body.linearVelocity.x < 0f)
        {
            spriteRenderer.flipX = false;
        }

        // Controles direction
        if (leftPressed && rightPressed)
            xInput = 0f;
        else if (leftPressed)
            xInput = -1f;
        else if (rightPressed)
            xInput = 1f;
        else
            xInput = inputActions.Map.Move.ReadValue<float>();
    }

    public void PressLeft()
    {
        leftPressed = true;
    }
    public void ReleaseLeft() {
        leftPressed = false;
    }


    public void PressRight() {
        rightPressed = true;
    }
    public void ReleaseRight() {
        rightPressed = false;
    }

    private void FixedUpdate () {
        // Déplacement horizontal
        body.linearVelocity = new Vector2 (xInput * horizontalSlide, body.linearVelocity.y);
    }

    // Fonction OnCollisionStay2D de Feet
    public void OnFeetCollisionStay (Collision2D other) {
        if (isBroken || is7Calibur)
			return;

		// Jump si on rebondit sur une plateforme
		if (body.linearVelocity.y <= 0 && (other.transform.tag == "Platform" || other.transform.tag == "GroundPlatform")) {
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
        body.linearVelocity = new Vector2 (0, force);
        // Décompte du nb de sauts
        jump -= 1;
        // Animation
        animator.SetTrigger ("Jump");
        // Audio 
        audiomanager.JumpSound();
    }
}