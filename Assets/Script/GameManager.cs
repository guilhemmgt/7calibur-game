using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // Reference
    // UI
    public GameObject UI;
    private UI_Manager UI_Manager;
    // Joueur
    public GameObject Player;
    private Player_Movement player_Movement;
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

    // Etats
    public bool isLaunch = false;
    public bool isPaused = false;
    public bool isTransition = false;

    // Position
    public Vector3 posPlayer;
    public Vector3 posDragon;
    public Vector3 posGround;

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
        player_system = Player.GetComponent<Player_System> ();
        player_Movement = Player.GetComponent<Player_Movement> ();
        UI_Manager = UI.GetComponent<UI_Manager> ();
        dragon_system = Dragon.GetComponent<Dragon_System> ();
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

    public void Bool_Setup () {
        // Player
        player_system.isOver = false;
        player_system.is7Calibur = false;

        // System
        isLaunch = true;
        isPaused = false;
        isTransition = false;

    }

    public void UI_Setup () {
        UI_Manager.menuStart.SetActive (false);
        UI_Manager.menuPause.SetActive (false);
        UI_Manager.menuOver.SetActive (false);
        UI_Manager.menuIG.SetActive (true);

        Time.timeScale = 1f;
    }

    public void Player_Setup () {
        player_Movement.transform.position = posPlayer;
        player_Movement.jump = 7;
    }

    public void Dragon_Setup () {
        Dragon.transform.position = posDragon;
        dragon_system.isAttacking = true;
    }

    public void Score_Setup () {
        player_system.Score = 0f;
        player_system.OldScore = 0f;
        player_system.CompteurCollision = 10;
        player_system.maxheight = 0f;
    }

    public void Despawn () {
        // PLateforme
        var Platforms = GameObject.FindGameObjectsWithTag ("Platform");
        foreach (var Platform in Platforms) {
            Destroy (Platform);
        }
        // Epee
        var Swords = GameObject.FindGameObjectsWithTag ("Sword");
        foreach (var Sword in Swords) {
            Destroy (Sword);
        }
    }

    public void Spawn () {
        Vector3 spawn_position = new Vector3 ();

        for (int i = 0; i < spawn_number; i++) {

            // Spawn Plateforme
            spawn_position.y += Random.Range (Min_H, Max_H);
            spawn_position.x = Random.Range (Platform_Min_L, Platform_Max_L);
            GameObject new_Plateform = Instantiate (platform, spawn_position, Quaternion.identity);

            // Spawn Sword

            GameObject new_sword = Spawn_Something (sword, Frequence_Sword, spawn_position, Sword_Min_L, Sword_Max_L, Sword_Min_H, Sword_Max_H);

            // Angle

            if (new_sword != null) {
                float AngleRandom = Random.Range (30f, 150f);
                new_sword.transform.Rotate (0f, 0f, AngleRandom, Space.World);
            }



            // Old
            /*
            if(R <= Frequence_Sword) 
            // Sword Spawn
            {
                // Side
                Sword_Side = Random.Range(0, 2); 

                // Largeur
                if(Sword_Side == 1)
                {
                    spawn_position.x = -Random.Range(Sword_Min_L , Sword_Max_L);
                }
                else
                {
                    spawn_position.x = Random.Range(Sword_Min_L , Sword_Max_L);
                }

                // Hauteur
                spawn_position.y += Random.Range(Min_H , Max_H); 
                
                //Spawn
                GameObject new_Sword = Instantiate(sword, spawn_position, Quaternion.identity);
            }
            else 
            // Spawn Plateforme
            {
                spawn_position.y += Random.Range(Min_H , Max_H);
                spawn_position.x = Random.Range(Platform_Min_L , Platform_Max_L);

                GameObject new_Plateform = Instantiate(platform, spawn_position, Quaternion.identity);
            }
            */

        }
    }

    public GameObject Spawn_Something (GameObject Something, int Frequence, Vector3 spawn_position, float RangeSide_Min, float RangeSide_Max, float RangeH_Min, float RangeH_Max) {
        int R = Random.Range (0, 100);

        if (R <= Frequence) {
            // Position
            spawn_position.x += Random.Range (RangeSide_Min, RangeSide_Max);
            spawn_position.y += Random.Range (RangeH_Min, RangeH_Max);

            //Spawn
            GameObject new_something = Instantiate (sword, spawn_position, Quaternion.identity);

            return new_something;
        } else {
            return null;
        }
    }


    public void Same_Tower () {
        // Terrain
        Despawn ();
        Spawn ();
        Instantiate (Top, new Vector3 (0f, 100f, 0f), Quaternion.identity);
        Instantiate (Ground, posGround, Quaternion.identity);

        // Score
        player_system.OldScore += player_system.NewScore;
        player_system.maxheight = 0;
        player_system.CompteurCollision = 10;
        player_system.NewScore = 0;

        // Setup
        player_system.is7Calibur = false;
        Player_Setup ();
        Dragon_Setup ();

        player_system.Tower += 1;
    }

    // Truc pour les rotations tkt 
    // Change the Quaternion values depending on the values of the Sliders
    private static Quaternion Change (float x, float y, float z) {
        //Return the new Quaternion
        return new Quaternion (x, y, z, 1);
    }
}