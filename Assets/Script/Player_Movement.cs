using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {
    // References

    // Joueur
    private Player_System player_system;
<<<<<<< HEAD
    private Rigidbody2D body;
=======
    public Rigidbody2D body;
    public SpriteRenderer sprite;
>>>>>>> 1600842933d89c2118803f5683a416d6f516ba40

    // Constantes
    public float verticalBounce = 10f;
    public float verticalWallBounce = 10f;
    public float horizontalSlide = 10f;
    public int jump = 7;

    // Etats
    public bool isBroken = false;
    public bool canWallJump = true;

    // Variables
<<<<<<< HEAD
    private float xInput = 0;

    private void Awake () {
        body = GetComponent<Rigidbody2D> ();
=======
    private float moveX;
    private float moveXBouton;
    private float moveXClavier;


    void Awake () {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        groundCheck_System = GroundCheck.GetComponent<GroundCheck_System> ();
>>>>>>> 1600842933d89c2118803f5683a416d6f516ba40
        player_system = GetComponent<Player_System> ();
    }

    private void Update () {
        // Etats
        if (jump <= 0) {
            isBroken = true;
        } else {
            isBroken = false;
        }
<<<<<<< HEAD
    }

    private void FixedUpdate () {
        body.velocity = new Vector2 (xInput * horizontalSlide, body.velocity.y);
    }

    private void OnCollisionEnter2D (Collision2D other) {
        if (isBroken || player_system.is7Calibur)
=======

        // Movement
        moveXClavier = Input.GetAxis ("Horizontal") * horizontalSlide;

        moveX = moveXBouton + moveXClavier;
    }

    private void FixedUpdate () {

        if (!isWallJumping) {
            body.velocity = new Vector2 (moveX, body.velocity.y);
        }
        
    }

    private void OnCollisionEnter2D (Collision2D other) {

        if (isBroken || player_system.is7Calibur) {
>>>>>>> 1600842933d89c2118803f5683a416d6f516ba40
            return;

        // Jump
        if (other.relativeVelocity.y >= 0f && other.transform.GetComponent<Platform_System> () != null) {
            Jump (verticalBounce);
            canWallJump = true;
        }

<<<<<<< HEAD
        // Wall Jump
        if (other.gameObject.tag == "Wall" && canWallJump) {
            Jump (verticalWallBounce);
            canWallJump = false;
=======
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
>>>>>>> 1600842933d89c2118803f5683a416d6f516ba40
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