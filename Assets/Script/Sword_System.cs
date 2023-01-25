using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_System : MonoBehaviour
// C'est pas opti avec Platform_System j'aurai du faire qu'un code pour gérer ça
{
    // References
    // Joueur
    private GameObject Player;
    private Player_Movement player_movement;


    // Constantes
    private float verticalBounce;

    // Start is called before the first frame update
    void Awake()
    {
       Player = GameObject.FindWithTag("Player"); 
       player_movement = Player.GetComponent<Player_Movement>();
       verticalBounce = player_movement.verticalBounce;
    }

    // Jump
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.relativeVelocity.y <= 0f && other.gameObject.tag == "Player")
        {
            Rigidbody2D body = other.gameObject.GetComponent<Rigidbody2D>();

            Refill();

            body.AddForce(new Vector2(0f, verticalBounce), ForceMode2D.Impulse);

            player_movement.jump -= 1;
            player_movement.canWallJump = true;
        }
    }

    private void Refill()
    {
        Destroy(this.gameObject);
        player_movement.jump = 7;            
    }
}


