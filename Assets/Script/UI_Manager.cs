using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    // References
    // GameManager
    public GameObject gameManager;
    private GameManager s_gameManager;
    // Player
    public GameObject Player;
    private Player_Movement player_Movement;
    private Player_System player_system;

    // Texte
    public Text Text_Score;
    public Text Text_Tower;
    public Text Text_Jump;

    // Menu
    public GameObject menuPause;
    public GameObject menuOver;
    public GameObject menuStart;
    public GameObject menuIG;

    void Awake() 
    {
        s_gameManager = gameManager.GetComponent<GameManager>();
        player_Movement = Player.GetComponent<Player_Movement>();
        player_system = Player.GetComponent<Player_System>();
    }
    void Update()
    {   
        // Score 

        Text_Score.text = "Score : " + (int)player_system.Score;
        Text_Tower.text = "Tower : " + (int)player_system.Tower;

        // Jump 

        if(player_Movement.jump > 0)
        {
            Text_Jump.text = "Jump : " + player_Movement.jump;
        }
        else
        {
            Text_Jump.text = "Jump : " + 0;
        }
        

        // Menu Pause

        if(Input.GetKeyDown(KeyCode.Escape) && s_gameManager.isLaunch){
            if (s_gameManager.isPaused){
                Time.timeScale = 1f;
                s_gameManager.isPaused = false;
                menuPause.SetActive(false);
            }
            else{
                Time.timeScale = 0f;
                s_gameManager.isPaused = true;
                menuPause.SetActive(true);
            }
        }
        
    
        // Menu Over

        if (player_system.isOver) {
            Time.timeScale = 0f;  
            menuOver.SetActive(player_system.isOver); 
        }

        if((Input.anyKey) && player_system.isOver){
            player_system.maxheight = 0f;
            s_gameManager.Launch();
        }

        // Menu Start

        if (!s_gameManager.isLaunch) {
            Time.timeScale = 0f;  
            menuStart.SetActive(!s_gameManager.isLaunch);     
        }

        if((Input.anyKey) && !s_gameManager.isLaunch)
        {
            s_gameManager.Setup(); 
        }


    }
}
