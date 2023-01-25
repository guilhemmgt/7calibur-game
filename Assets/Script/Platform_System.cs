using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_System : MonoBehaviour
{
    // Reference
    // Joueur
    private GameObject Player;
    private Player_Movement player_movement;
    private Player_System player_system;

    // Constantes
    private float verticalBounce;
    public float Destroy_time;
    public bool Destoyable;

    // Component
    private Collider2D Collider;

    void Awake()
    {
        Collider = GetComponent<EdgeCollider2D>();

        Player = GameObject.FindWithTag("Player"); 
        player_movement = Player.GetComponent<Player_Movement>();
        player_system = Player.GetComponent<Player_System>();

        verticalBounce = player_movement.verticalBounce;
    }

    void Update()
    {
        // Etats
        if(player_system.is7Calibur)
        {
            Collider.isTrigger = true;
        }
        else
        {
            Collider.isTrigger = false;
        }
    }

    // Jump
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.relativeVelocity.y <= 0f && other.gameObject.tag == "Player")
        {
            Rigidbody2D body = other.gameObject.GetComponent<Rigidbody2D>();
            if(!player_movement.isBroken && !player_system.is7Calibur)
            {           
                body.AddForce(new Vector2(0f, verticalBounce), ForceMode2D.Impulse);
                    
                player_movement.jump -= 1;
                player_movement.canWallJump = true;

                if(Destoyable)
                {
                    Destroy(this.gameObject, Destroy_time);
                }
            } 
        }
    }

    // Score Descente
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Rigidbody2D body = other.gameObject.GetComponent<Rigidbody2D>();

        if(body != null && player_system.is7Calibur)
            {                
                Destroy(this.gameObject);
                player_system.CompteurCollision += 1;
            }
    }
}
