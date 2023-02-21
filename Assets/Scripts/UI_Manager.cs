using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {
    // Scripts
    private GameManager gameManager;
    private Player_Movement player_Movement;
    private Player_System player_system;
    private ScoreKey scoreKey;

    // Textes
    public TextMeshProUGUI Text_Score;
    public TextMeshProUGUI Text_Tower;
    public TextMeshProUGUI Text_Towerbis;
    public TextMeshProUGUI Text_Jump;
    public TextMeshProUGUI Text_Score_Over;
    public TextMeshProUGUI Text_Key;

    // UI
    public GameObject pauseUI;
    public GameObject gameOverUI;
    public GameObject menuUI;
    public GameObject gameUI;
    public GameObject rulesUI;
    // UI active (parmi les UI ci-dessus)
    private GameObject activeUI;

    // Polices
    public TMP_FontAsset blackFont;
    public TMP_FontAsset redFont;
    public TMP_FontAsset goldFont;

    private void Awake () {
        gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
        player_Movement = gameManager.player.GetComponent<Player_Movement> ();
        player_system = gameManager.player.GetComponent<Player_System> ();
        scoreKey = gameManager.GetComponent<ScoreKey> ();
    }

    private void Update () {
        // Actualisation des stats à l'écran 
        // Score
        Text_Score.text = "" + player_system.score;
        Text_Score_Over.text = "Score : " + player_system.score;
        // Nb de tours
        Text_Tower.text = "" + player_system.Tower;
        Text_Towerbis.text = "Tour " + player_system.Tower;
        // Nb de jumps
        Text_Jump.text = "" + player_Movement.jump;
        // Clé du score
        int key = scoreKey.GetKey (player_system.score);
        if (key == -1)
            Text_Key.text = "Frérot fais un effort";
        else if (key == -2)
            Text_Key.text = "Frérot arrête de cheater";
        else
            Text_Key.text = "[" + gameManager.version + "." + key + "]";
        // [Anticheat] Changement de l'ombre de la police en fonction du score
        if (player_system.score < 5000)
            Text_Key.font = blackFont;
        else if (player_system.score < 10000)
            Text_Key.font = redFont;
        else
            Text_Key.font = goldFont;

        // Gestion des inputs selon l'UI ouverte
        if (activeUI == pauseUI) { // UI pause
            if (Input.GetKeyDown (KeyCode.Escape)) {
                gameManager.Play ();
            }
        } else if (activeUI == gameOverUI) { // UI Game Over
            if ((Input.anyKeyDown)) {
                //gameManager.ReplayAfterGameOver ();
            }
        } else if (activeUI == gameUI) { // UI Jeu
            if (Input.GetKeyDown (KeyCode.Escape)) {
                gameManager.Pause ();
            }
        } else if (activeUI == menuUI) { // UI Menu
            if ((Input.anyKeyDown)) {
                //gameManager.Play ();
            }
        }
    }

    // Ouvre une UI et ferme toutes les autres
    private void SelectUI (GameObject menu) {
        pauseUI.SetActive (false);
        gameOverUI.SetActive (false);
        menuUI.SetActive (false);
        gameUI.SetActive (false);
        rulesUI.SetActive (false);

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
    public void OpenRulesUI () {
        SelectUI (rulesUI);
	}
}