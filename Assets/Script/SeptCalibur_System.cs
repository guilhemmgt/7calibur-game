using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeptCalibur_System : MonoBehaviour
{
    // References
    // Terrain
    public GameObject TopPlatform;
    // Dragon
    public GameObject Dragon;
    private Dragon_System dragon_system;
    // Joueur
    public GameObject Player;
    private Player_System player_system;
    // GameManager
    public GameObject GameManager;
    private GameManager s_GameManager;

    private void Awake() 
    {
        s_GameManager = GameManager.GetComponent<GameManager>();
        dragon_system = Dragon.GetComponent<Dragon_System>();
        player_system = Player.GetComponent<Player_System>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            // Etats
            dragon_system.isAttacking = false;
            player_system.is7Calibur = true;

            //Score
            player_system.CompteurCollision = 0;

            // Destroy Terrain
            Destroy(TopPlatform, 1f);
            var Grounds = GameObject.FindGameObjectsWithTag ("GroundPlatform"); 
            foreach (var Ground in Grounds)
            { 
                Destroy(Ground); 
            }

            // Dragon
            Dragon.transform.position = s_GameManager.posDragon;
        }
        
    }
}
