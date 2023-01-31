using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {
    // References
    // GroundCheck ====> NEVER USED
    public GameObject GroundCheck;
    private GroundCheck_System groundCheck_System;
    // Joueur
    private Player_System player_system;
    public Rigidbody2D body;

    // Constantes
    public float verticalBounce = 10f;
    public float verticalWallBounce = 10f;
    public float horizontalWallBounce = 0.5f;
    //public float brokenjumpForce = 10f;
    public float horizontalSlide = 10f;
    public int jump = 7;

    // Etats
    public bool isBroken;
    public bool canWallJump;
    private bool isWallJumping = false;

    // Variables
    private float moveX;


    void Awake () {
        body = GetComponent<Rigidbody2D> ();
        groundCheck_System = GroundCheck.GetComponent<GroundCheck_System> ();
        player_system = GetComponent<Player_System> ();
    }


    void Update () {

        // Etats
        if (jump <= 0) {
            isBroken = true;
        } else {
            isBroken = false;
        }

        // Movement
        //moveX = Input.GetAxis ("Horizontal") * horizontalSlide;
    }

    private void FixedUpdate () {
        if (!isWallJumping) {
            body.velocity = new Vector2 (moveX, body.velocity.y);
        }
    }

    private void OnCollisionEnter2D (Collision2D other) {
        if (isBroken || player_system.is7Calibur) {
            return;
        }

        if ((other.gameObject.GetComponent<Platform_System> () != null || other.gameObject.GetComponent<Sword_System> () != null) && other.relativeVelocity.y >= 0f) {
            // Saut
            body.velocity = new Vector2 (body.velocity.x, verticalBounce);

            jump -= 1;
            canWallJump = true;
            isWallJumping = false;

        } else if (other.gameObject.tag == "Wall" && canWallJump) {
            // Wall jump
            body.velocity = new Vector2 (-moveX * horizontalWallBounce, verticalWallBounce);
            jump -= 1;

            canWallJump = false;
            isWallJumping = true;
        } else if (other.gameObject.tag == "Wall" && !canWallJump) {
            // Toucher un mur après un wall jump
            isWallJumping = false;
        }
    }

    public void move (int Direction) {
        moveX = horizontalSlide * Direction;
    }
}