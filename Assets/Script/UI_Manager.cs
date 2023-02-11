using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {
    // References
    // GameManager
    private GameManager gameManager;
    // Player
    private Player_Movement player_Movement;
    private Player_System player_system;

    // Texte
    public Text Text_Score;
    public Text Text_Tower;
    public Text Text_Jump;
    public Text Text_Score_Over;

    // UI
    public GameObject pauseUI;
    public GameObject gameOverUI;
    public GameObject menuUI;
    public GameObject gameUI;
    // UI active (parmi les UI ci-dessus)
    private GameObject activeUI;

    private void Awake () {
        gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
        player_Movement = gameManager.player.GetComponent<Player_Movement> ();
        player_system = gameManager.player.GetComponent<Player_System> ();
    }

    private void Update () {
        // Score 
        Text_Score.text = "Score : " + (int)player_system.score;
        Text_Score_Over.text = "Score : " + (int)player_system.score;
        Text_Tower.text = "Tower : " + (int)player_system.Tower;

        // Jump 
        if (player_Movement.jump > 0) {
            Text_Jump.text = "Jump : " + player_Movement.jump;
        } else {
            Text_Jump.text = "Jump : " + 0;
        }

        // Gestion des inputs selon l'UI ouverte
        if (activeUI == pauseUI) { // UI pause
            if (Input.GetKeyDown (KeyCode.Escape)) {
                gameManager.Play ();
            }
        } else if (activeUI == gameOverUI) { // UI Game Over
            if ((Input.anyKeyDown)) {
                gameManager.ReplayAfterGameOver ();
            }
        } else if (activeUI == gameUI) { // UI Jeu
            if (Input.GetKeyDown (KeyCode.Escape)) {
                gameManager.Pause ();
            }
        } else if (activeUI == menuUI) { // UI Menu
            if ((Input.anyKeyDown)) {
                gameManager.Play ();
            }
        }
    }

    // Ouvre une UI et ferme toutes les autres
    private void SelectUI (GameObject menu) {
        pauseUI.SetActive (false);
        gameOverUI.SetActive (false);
        menuUI.SetActive (false);
        gameUI.SetActive (false);

        menu.SetActive (true);
        activeUI = menu;
    }

    // Ouvre le menu principal
    public void OpenMenuUI () {
        SelectUI (menuUI);
	}
    // Ouvre la pause
    public void OpenPauseUI () {
        SelectUI (pauseUI);
	}
    // Ouvre l'interface de jeu
    public void OpenGameUI () {
        SelectUI (gameUI);
	}
    // Ouvre l'écran de mort
    public void OpenGameOverUI () {
        SelectUI (gameOverUI);
	}
}