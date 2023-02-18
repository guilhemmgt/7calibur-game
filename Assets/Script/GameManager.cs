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

    // Positions initiales
    public Vector3 posPlayer;
    public Vector3 posDragon;

    // Nombre de sauts initial
    public int initialJumpAmount; 

    private void Awake () {
        player_system = player.GetComponent<Player_System> ();
        player_movement = player.GetComponent<Player_Movement> ();
        uiManager = GameObject.Find ("UI").GetComponent<UI_Manager> ();
        towerGen = GetComponent<TowerGeneration> ();
    }

	private void Start () {
        // Au démarrage, on affiche le menu principal
        Menu ();
    }

    // Réinitialise le joueur et le dragon et génère une nouvelle tour
    private void ResetGame () {
        player_movement.ResetMovement ();
        towerGen.GenerateTower ();
        ResetPlayer ();
        ResetDragon ();
    }

    // Réinitialise le joueur
    private void ResetPlayer () {
        player_movement.transform.position = posPlayer; // Position
        player_movement.jump = initialJumpAmount; // Sauts
        player.GetComponent<Rigidbody2D> ().velocity = Vector3.zero; // Vélocité
        player_movement.is7Calibur = false;
    }

    // Réinitialise le dragon
    private void ResetDragon () {
        Dragon.transform.position = posDragon; // Position
    }

    // Lancer le menu
    public void Menu () {
        // Pause
        Time.timeScale = 0;
        // Réinitialisation
        ResetGame ();
        player_system.ResetScore ();
        // UI
        uiManager.OpenMenuUI ();
    }
    // Démarrer le jeu en l'état actuel (utilisé depuis le menu ou la pause)
    public void Play () {
        // Action
        Time.timeScale = 1;
        // UI
        uiManager.OpenGameUI ();
	}
    // Mettre le jeu en pause
    public void Pause () {
        // Pause
        Time.timeScale = 0;
        // UI
        uiManager.OpenPauseUI ();
	}
    // Relancer une nouvelle tour (sans reset du score)
    public void ReplayAfterWin () {
        // Si on gagne on quitte le mode débutant.
        player_system.isBeginner = false;
        // Réinitialisation
        ResetGame ();
        // Incrémentation du nb de tours vaincues
        player_system.AddTower ();
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
        // UI
        uiManager.OpenGameOverUI ();
    }
}