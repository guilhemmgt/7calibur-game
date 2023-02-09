using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // Scripts
    private UI_Manager uiManager;
    private TowerGeneration towerGen;
    private Player_Movement player_movement;
    private Player_System player_system;

    // GameObjects
    public GameObject Dragon;
    public GameObject player;

    // Positions de spawn
    public Vector3 posPlayer;
    public Vector3 posDragon;

    private void Awake () {
        player_system = player.GetComponent<Player_System> ();
        player_movement = player.GetComponent<Player_Movement> ();
        uiManager = GameObject.Find ("UI").GetComponent<UI_Manager> ();
        towerGen = GetComponent<TowerGeneration> ();
    }

	private void Start () {
        Menu ();
    }

    private void ResetGame () {
        towerGen.GenerateTower ();
        ResetPlayer ();
        ResetDragon ();
    }

    private void ResetPlayer () {
        player_movement.transform.position = posPlayer; // Position
        player_movement.jump = 7; // Sauts
        player.GetComponent<Rigidbody2D> ().velocity = Vector3.zero; // Vélocité
        player_movement.is7Calibur = false;
    }

    private void ResetDragon () {
        Dragon.transform.position = posDragon; // Position
    }

    //
    // J'ai fais 150 fonctions spécifiques en dessous pcq ça sera + pratique, surtout quand on rajoutera des boutons style "retour au menu principal" etc
    //

    // Lancer le menu
    public void Menu () {
        // Pause
        Time.timeScale = 0;
        // Réinitialisation
        ResetGame ();
        player_system.ResetScore ();

        uiManager.OpenMenuUI ();
    }
    // Démarrer le jeu en l'état actuel (utilisé depuis le menu ou la pause)
    public void Play () {
        // Action
        Time.timeScale = 1;

        uiManager.OpenGameUI ();
	}
    // Mettre le jeu en pause
    public void Pause () {
        // Pause
        Time.timeScale = 0;

        uiManager.OpenPauseUI ();
	}
    // Relancer une nouvelle tour (sans reset du score)
    public void ReplayAfterWin () {
        // Réinitialisation
        ResetGame ();
        // Jeu
        Play ();
    }
    // Relancer une nouvelle tour (avec reset du score)
    public void ReplayAfterGameOver () {
        // Réinitialisation
        ResetGame ();
        player_system.ResetScore ();
        // Jeu
        Play ();
    }
    // Mort
    public void GameOver () {
        // Pause
        Time.timeScale = 0;

        uiManager.OpenGameOverUI ();
    }
}