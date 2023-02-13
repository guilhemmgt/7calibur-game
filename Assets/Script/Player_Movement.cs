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
    public int jump = 7; // Nombre de sauts restants
    private bool isBroken = false; // Épée brisée (plus de sauts)
    private bool canWallJump = true; // Peut walljump
    private float xInput = 0; // Input directionnel (-1, 0, 1)
    public bool is7Calibur = false;


    private void Awake () {
        body = GetComponent<Rigidbody2D> ();
        spriteRenderer = GetComponent<SpriteRenderer> ();
        animator = GetComponent<Animator> ();
    }

    private void Update () {
        isBroken = jump <= 0;

        // Graphisme

        // Flip
        if (body.velocity.x > 0f) {
            spriteRenderer.flipX = true;
        }
        if (body.velocity.x < 0f) {
            spriteRenderer.flipX = false;
        }
        // Animation
        animator.SetBool ("isBroken", isBroken);

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
        // Ne peut plus walljump si arrivé en haut de la tour
		if (other.transform.tag == "TopPlatform") {
            canWallJump = false;
        }

        
    }

    private void OnCollisionStay2D (Collision2D other) {
        // Si plus d'épée ou en fin de niveau, plus de saut possible
        if (isBroken || is7Calibur)
            return;

        // Wall Jump si on touche un mur
        if (other.gameObject.tag == "Wall" && canWallJump) {
            Jump (verticalWallBounce);
            canWallJump = false;
        }
    }

	private void Jump (float force) {
        body.velocity = new Vector2 (0, force);
        jump -= 1;

        // Animation
        animator.SetTrigger ("Jump");
    }

    // Appelé par l'UI
    //public void Move (int direction) {
    //    if (direction == 0) {
    //        pressedInputs = Mathf.Clamp (pressedInputs - 1, 0, 2);
    //        if (pressedInputs == 0)
    //            xInput = 0;
    //    } else {
    //        pressedInputs = Mathf.Clamp (pressedInputs + 1, 0, 2);
    //        xInput = direction;
    //    }
    //}

    public bool leftPressed = false;
    public bool rightPressed = false;
    public void PressLeft () {
        xInput = -1;
        leftPressed = true;
    }
    public void PressRight () {
        xInput = 1;
        rightPressed = true;
    }
    public void ReleaseLeft () {
        leftPressed = false;
        if (!rightPressed)
            xInput = 0;
	}
    public void ReleaseRight () {
        rightPressed = false;
        if (!leftPressed)
            xInput = 0;
    }
    public void ResetMovement () {
        xInput = 0;
        rightPressed = false;
        leftPressed = false;
	}
}