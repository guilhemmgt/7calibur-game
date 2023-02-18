using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGeneration : MonoBehaviour {
    // Scripts
    private GameManager gameManager;
    private Player_System player_system;
    // Préfabs de plateformes
    private GameObject groundPlateformPrefab;
    private GameObject topPlateformPrefab;
    private GameObject plateformPrefab;
    // Préfab du rocher de fin
    private GameObject topSwordPrefab;

    // Parent des plateformes et items instanciés
    private Transform towerContent;
    // Parent des tours instanciées
    private Transform themeContent;

    // Tours
    [Header ("Themes")]
    public List<Theme> towerThemes = new List<Theme> ();

    // Plateformes
    [Header ("Plateformes")]
    // Borne Hauteur de Spawn
    public float platformMinH;
    public float platformMaxH;
    // Borne Largeur de Spawn
    public float platformMinL;
    public float platformMaxL;
    // Nombre de plateformes
    public int spawn_number;
    // Position des plateformes de début et de fin
    public Vector3 groundPlatformPos;
    public Vector3 topPlatformPos;
    public Vector3 topSwordPos;

    // Items
    // /!\ La somme des fréquences de spawn ne doit pas dépasser 100 sinon c'est complètement con /!\

    // Épée
    [Header ("Épée")]
    public GameObject swordPrefab;
    public int swordFrequence;
    // Borne Largeur de Spawn Par rapport a la plateforme
    public float swordMinL;
    public float swordMaxL;
    public float coinMinL;
    public float coinMaxL;
    public float spikeMinL;
    public float spikeMaxL;
    public float TorchMinL;
    public float TorchMaxL;
    // Borne Hauteur de Spawn Par rapport a la plateforme
    public float swordMinH;
    public float swordMaxH;

    // Pièce
    [Header ("Pièce")]
    public GameObject coinPrefab;
    public int coinFrequence;
    public float coinH; // Hauteur de spawn par rapport à la plateforme

    // Piques
    [Header ("Piques")]
    public GameObject spikesPrefab;
    public int spikesFrequence;
    public float spikesH;

    // Torch
    [Header ("Torche")]
    public GameObject torchPrefab;
    public int torchFrequence;
    private int Sidechoose;

    private int compteur = 0; // compteur pour eviter le softlock


    private void Awake () {
        towerContent = GameObject.Find ("TowerContent").transform;
        themeContent = GameObject.Find ("ThemeContent").transform;
        gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
        player_system = gameManager.player.GetComponent<Player_System> ();
	}

    // Supprime toutes les plateformes et items de la tour
	private void CleanTowerContent () {
        foreach (Transform child in towerContent) {
            Destroy (child.gameObject);
        }
    }

    // Modifie les préfabs en fonction du theme
    private void SetTheme (Theme theme) {
        plateformPrefab = theme.platform;
        groundPlateformPrefab = theme.groundPlatform;
        topPlateformPrefab = theme.topPlatform;
        topSwordPrefab = theme.topSword;
        spikesPrefab = theme.spike;
        coinPrefab = theme.coin;
    }

    // Règles pour choisir un thème
    private Theme ChooseNewTheme () {
        // Pour l'instant c'est juste random, mais on pourra implémenter des fréquences
        // ou des tours spéciales en fonction de la progression du joueur
       
        int randomnumber = Random.Range(0, 100); 
        int themeIndex;

        if(player_system.Tower == 1 && player_system.isBeginner){
            return towerThemes[0];
        }
        /*else{
            // Oui ce serait mieux avec un case switch mais ça marchait po
            if(randomnumber == 0){
                themeIndex = 4; // Tour d'ivoire        1%
            }
            if(randomnumber>=1 && randomnumber<=10){
                themeIndex = 3; // Tour verte           10%
            }
            if(randomnumber>=11 && randomnumber<=30){
                themeIndex = 3; // Tour rouge           20%      
            }
            if(randomnumber>=31 && randomnumber<=70){
                themeIndex = 3; // Tour rouge           40%      
            }
            if(randomnumber>=71 && randomnumber<=85){
                themeIndex = 3; // Tour Hi7             15%      
            }
            if(randomnumber>=86 && randomnumber<=100){
                themeIndex = 3; // Tour Ram7            15%      
            }
        }*/
        else{
            themeIndex = Random.Range (1, towerThemes.Count);
            return towerThemes[themeIndex];
        }
    }

    // Regenere une nouvelle tour
    public void GenerateTower () {
        CleanTowerContent ();
        Theme newTheme = ChooseNewTheme ();
        SetTheme (newTheme);

        // Détruit l'éventuel prefab de tour déjà instancié
        foreach (Transform child in themeContent) {
            Destroy (child.gameObject);
        }

        // Instancie la nouvelle tour
        Instantiate (newTheme.tower, themeContent);
        // Instancie les plateformes de départ et d'arrivée
        Instantiate (topPlateformPrefab, topPlatformPos, Quaternion.identity, towerContent);
        Instantiate (groundPlateformPrefab, groundPlatformPos, Quaternion.identity, towerContent);
        Instantiate (topSwordPrefab, topSwordPos, Quaternion.identity, towerContent);
        // Instancie les plateformes et les items
        Vector3 spawn_position = new Vector3 ();
        for (int i = 0; i < spawn_number; i++) {
            spawn_position.y += Random.Range (platformMinH, platformMaxH);
            spawn_position.x = Random.Range (platformMinL + 0.1f, platformMaxL- 0.1f);
            SpawnPlatform (spawn_position);
        }
    }

    // Créé une plateforme à la position donnée, et avec (ou non) un item (aléatoirement)
    private void SpawnPlatform (Vector3 position) {

        // Instancie la plateforme
        Transform newPlatform = Instantiate (plateformPrefab, position, Quaternion.identity, towerContent).transform;

        // Code caca pas super beau, je referais avec des scriptableobjects je pense si y'a besoin
        // En gros on choisit aléatoirement l'objet qu'on va spawn sur cette plateform :
        GameObject[] items = new GameObject[] { coinPrefab, swordPrefab, spikesPrefab };
        int[] frequences = new int[] { coinFrequence, swordFrequence, spikesFrequence };
        GameObject itemToSpawn = null;
        int somme = 0;
        for (int i = 0; i < items.Length; i++) {
            somme += frequences[i];
            if (Random.Range (0, 100) < somme) {
                itemToSpawn = items[i];
                break;
            }
        }

        compteur += 1; // Compteur augmente des qu'on pose une plateforme 

        // Protection Softlock
        if (compteur > 5) {
            itemToSpawn = swordPrefab;
        }

        // On spawn l'objet choisi
        if (itemToSpawn == coinPrefab) {
            SpawnCoin (newPlatform);
        } else if (itemToSpawn == swordPrefab) {
            SpawnSword (newPlatform);
            compteur = 0; // Si c'est un épée qui apparait il se reset
        } else if (itemToSpawn == spikesPrefab) {
            SpawnSpikes (newPlatform);
        }


        // Spawn des torches
        if (position.x > 1) {
            Sidechoose = 1;
        } else if (position.x < -1) {
            Sidechoose = 2;
        } else {
            Sidechoose = 0;
        }

        if (Random.Range (0, 100) < torchFrequence) {
            SpawnTorch (newPlatform, Sidechoose);
        }

    }

    private void SpawnSword (Transform platform) {
        // Position de l'epee
        float xRandomTranslation = Random.Range (swordMinL, swordMaxL);
        float yRandomTranslation = Random.Range (swordMinH, swordMaxH);
        Vector3 position = platform.position + new Vector3 (xRandomTranslation, yRandomTranslation, 0f);

        // Instanciation de l'epee
        Transform newSword = Instantiate (swordPrefab, position, Quaternion.identity, platform).transform;

        // Rotation de l'epee
        float swordAngle = Random.Range (-50f, 50f);
        newSword.Rotate (0, 0, swordAngle, Space.World);
    }

    private void SpawnCoin (Transform platform) {
        // Position de la pièce
        float xRandomTranslation = Random.Range (coinMinL, coinMaxL);
        Vector3 position = platform.position + new Vector3 (xRandomTranslation, coinH, 0f);

        // Instanciation de la pièce
        Instantiate (coinPrefab, position, Quaternion.identity, platform);
    }

    private void SpawnSpikes (Transform platform) {
        // Position des piques
        float xRandomTranslation = Random.Range (spikeMinL, spikeMaxL);
        Vector3 position = platform.position + new Vector3 (xRandomTranslation, spikesH, 0f);

        // Instanciation des piques
        Instantiate (spikesPrefab, position, Quaternion.identity, platform);
    }

    private void SpawnTorch (Transform platform, int Side) {

        if(Side==1){
            // Position des piques
            float xRandomTranslation = Random.Range (-TorchMaxL, -TorchMinL);
            Vector3 position = platform.position + new Vector3 (xRandomTranslation, 0f, 0f);

            // Instanciation des piques
            Instantiate (torchPrefab, position, Quaternion.identity, platform);
        }
        if(Side==2){

            // Position des piques
            float xRandomTranslation = Random.Range (TorchMinL, TorchMaxL);
            Vector3 position = platform.position + new Vector3 (xRandomTranslation, 0f, 0f);

            // Instanciation des piques
            Instantiate (torchPrefab, position, Quaternion.identity, platform);
        }
        else{
        // Side == 0 , pas de spawn 
        }
    }
}