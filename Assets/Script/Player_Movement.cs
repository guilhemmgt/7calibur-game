using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // References
    // GroundCheck ====> NEVER USED
    public GameObject GroundCheck;
    private GroundCheck_System groundCheck_System;
    // Joueur
    public GameObject Player;
    private Player_System player_system;
    public Rigidbody2D body;

    // Constantes
    public float verticalBounce = 10f;
    public float verticalWallBounce = 10f;
    //public float brokenjumpForce = 10f;
    public float horizontalSlide = 10f;
    public int jump = 7;

    // Etats
    public bool isBroken;
    public bool canWallJump;

    // Variables
    private float moveX;
    private float moveXButton;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();  
        groundCheck_System = GroundCheck.GetComponent<GroundCheck_System>();
        player_system = Player.GetComponent<Player_System>();
    }

    void Update()
    {   
        // Etats
        if(jump <= 0){
            isBroken = true;
        }
        else{
            isBroken = false;
        }

        // Movement
        moveX = Input.GetAxis("Horizontal") * horizontalSlide;
         
    }

    private void FixedUpdate() 
    {
        Vector2 velocity = body.velocity;
        velocity.x  = moveX + moveXButton;
        body.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        // Wall Jump
        if (other.gameObject.tag == "Wall" && !isBroken && !player_system.is7Calibur && canWallJump)
        {
            body.AddForce(transform.up * verticalWallBounce, ForceMode2D.Impulse);
            jump -= 1;

            canWallJump = false;
        }
    }
    
    public void move(int Direction)
    {
        moveXButton = horizontalSlide * Direction;
    }
}
