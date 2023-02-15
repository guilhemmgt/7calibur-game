using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGeneration : MonoBehaviour {
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


    private void Awake () {
        towerContent = GameObject.Find ("TowerContent").transform;
        themeContent = GameObject.Find ("ThemeContent").transform;
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
    }

    // Règles pour choisir un thème
    private Theme ChooseNewTheme () {
        // Pour l'instant c'est juste random, mais on pourra implémenter des fréquences
        // ou des tours spéciales en fonction de la progression du joueur
        
        //int themeIndex = Random.Range (0, towerThemes.Count);
       
        int randomnumber = Random.Range(0, 100); 
        int themeIndex;

        if(randomnumber == 1){
            themeIndex = 1;
        }
        else{
            themeIndex = Random.Range (0, towerThemes.Count - 1);
        }
        
        return towerThemes[themeIndex];
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
        // On spawn l'objet choisi
        if (itemToSpawn == coinPrefab)
            SpawnCoin (newPlatform);
        else if (itemToSpawn == swordPrefab)
            SpawnSword (newPlatform);
        else if (itemToSpawn == spikesPrefab)
            SpawnSpikes (newPlatform);
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
        float xRandomTranslation = Random.Range (swordMinL, swordMaxL);
        Vector3 position = platform.position + new Vector3 (xRandomTranslation, coinH, 0f);

        // Instanciation de la pièce
        Instantiate (coinPrefab, position, Quaternion.identity, platform);
    }

    private void SpawnSpikes (Transform platform) {
        // Position des piques
        float xRandomTranslation = Random.Range (swordMinL, swordMaxL);
        Vector3 position = platform.position + new Vector3 (xRandomTranslation, spikesH, 0f);

        // Instanciation des piques
        Instantiate (spikesPrefab, position, Quaternion.identity, platform);
    }
}