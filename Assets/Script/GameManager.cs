using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // Reference
    // UI
    private UI_Manager UI_Manager;
    // Joueur
    public GameObject player;
    private Player_Movement player_movement;
    private Player_System player_system;
    // Dragon
    public GameObject Dragon;
    private Dragon_System dragon_system;
    // Terrain
    public GameObject Ground;
    public GameObject Top;
    // Epee
    public GameObject sword;
    // Plateforme
    public GameObject platform;
    // Parent des plateformes et items générés dans la tour
    private Transform towerContent;

    // Etats
    public bool isLaunch = false;
    public bool isPaused = false;

    // Position
    public Vector3 posPlayer;
    public Vector3 posDragon;
    public Vector3 posGround;
    public Vector3 posTop;

    // Variables
    // Spawn
    public int spawn_number = 300;  // Combien d'entités apparaissent
    public int Frequence_Sword;     // Rand(0..100) < Frequence_Sword => Spawn Sword

    // Borne Hauteur de Spawn
    public float Min_H;
    public float Max_H;
    // [Epee] Borne Largeur de Spawn Par rapport à la plateforme
    public float Sword_Min_L;
    public float Sword_Max_L;
    // [Epee] Borne Hauteur de Spawn Par rapport à la plateforme
    public float Sword_Min_H;
    public float Sword_Max_H;
    // [Plateforme] Borne Largeur de Spawn
    public float Platform_Min_L;
    public float Platform_Max_L;

    private void Awake () {
        player_system = player.GetComponent<Player_System> ();
        player_movement = player.GetComponent<Player_Movement> ();
        UI_Manager = GameObject.Find ("UI").GetComponent<UI_Manager> ();
        dragon_system = Dragon.GetComponent<Dragon_System> ();

        towerContent = GameObject.Find ("TowerContent").transform;
    }

    private void Start () {
        Spawn ();
    }


    // Reinitialiser
    public void Launch () {
        Setup ();
        Despawn ();
        Spawn ();
    }

    public void Setup () {
        Bool_Setup ();
        UI_Setup ();

        Player_Setup ();
        Dragon_Setup ();

        Score_Setup ();
        player_system.Tower = 1;
    }

    private void Bool_Setup () {
        // Player
        player_system.isOver = false;
        player_system.is7Calibur = false;

        // System
        isLaunch = true;
        isPaused = false;
    }

    private void UI_Setup () {
        UI_Manager.menuStart.SetActive (false);
        UI_Manager.menuPause.SetActive (false);
        UI_Manager.menuOver.SetActive (false);
        UI_Manager.menuIG.SetActive (true);

        Time.timeScale = 1f;
    }

    private void Player_Setup () {
        player_movement.transform.position = posPlayer;
        player_movement.jump = 7;
        player.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
    }

    private void Dragon_Setup () {
        Dragon.transform.position = posDragon;
        dragon_system.isAttacking = true;
    }

    private void Score_Setup () {
        player_system.Score = 0f;
        player_system.OldScore = 0f;
        player_system.maxheight = 0f;
    }

    private void Despawn () {
        foreach (Transform child in towerContent) {
            Destroy (child.gameObject);
        }
    }

    private void Spawn () {
        Vector3 spawn_position = new Vector3 ();

        for (int i = 0; i < spawn_number; i++) {
            spawn_position.y += Random.Range (Min_H, Max_H);
            spawn_position.x = Random.Range (Platform_Min_L, Platform_Max_L);
            SpawnPlatform (spawn_position);
        }
    }

    private void SpawnPlatform (Vector3 position) {
        // Instanciation de la plateforme
        Instantiate (platform, position, Quaternion.identity, towerContent);

        // A une chance de spawn une épée
        if (Random.Range (0, 100) <= Frequence_Sword) {
            // Position de l'épée
            float xRandomTranslation = Random.Range (Sword_Min_L, Sword_Max_L);
            float yRandomTranslation = Random.Range (Sword_Min_H, Sword_Max_H);
            Vector3 swordPosition = position + new Vector3 (xRandomTranslation, yRandomTranslation, 0);
            // Instanciation de l'épée
            Transform newSword = Instantiate (sword, swordPosition, Quaternion.identity, towerContent).transform;
            // Rotation de l'épée
            float swordAngle = Random.Range (-50f, 50f);
            newSword.Rotate (0, 0, swordAngle, Space.World);
		}
	}

    public void Same_Tower () {
        // Terrain
        Despawn ();
        Spawn ();
        Instantiate (Top, posTop, Quaternion.identity);
        Instantiate (Ground, posGround, Quaternion.identity);

        // Score
        player_system.OldScore += player_system.NewScore;
        player_system.maxheight = 0;
        player_system.NewScore = 0;

        // Setup
        player_system.is7Calibur = false;
        Player_Setup ();
        Dragon_Setup ();

        player_system.Tower += 1;
    }
}