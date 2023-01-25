using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_System : MonoBehaviour
{
    // Reference 
    // Joueur
    public GameObject Player;
    private Player_System player_System;
    // GameManager
    public GameObject GameManager;
    private GameManager s_GameManager;


    // Etats
    public bool isAttacking;

    // Constantes
    public float speed;
    

    void Awake()
    {
        player_System = Player.GetComponent<Player_System>();
        s_GameManager = GameManager.GetComponent<GameManager>();
    }


    void Update()
    {
        if(isAttacking){
            Move();
        }   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Game Over
        if (other.gameObject.tag == ("Player") && isAttacking)
        {
            player_System.isOver = true;
        }

        // Slain
        if (other.gameObject.tag == ("Player") && player_System.is7Calibur)
        {
            s_GameManager.Same_Tower();
        }
    }

    void Move()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}
