using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGeneration : MonoBehaviour {
    // Pr�fabs
    // Plateformes
    public GameObject groundPlateformPrefab;
    public GameObject topPlateformPrefab;
    public GameObject plateformPrefab;


    // Parent des plateformes et items generes dans la tour
    private Transform towerContent;

    // Position
    public Vector3 posGround;
    public Vector3 posTop;

    // Param�tres de spawn
    // Item
    public int spawn_number = 300;  // Combien d'entit�s apparaissent

    // Borne Hauteur de Spawn
    public float platformMinH;
    public float platformMaxH;
    // [Plateforme] Borne Largeur de Spawn
    public float platformMinL;
    public float platformMaxL;

    // Items
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
	}

    // Supprime toutes les plateformes et items
	private void CleanTower () {
        foreach (Transform child in towerContent) {
            Destroy (child.gameObject);
        }
    }

    // Regenere une nouvelle tour
    public void GenerateTower () {
        CleanTower ();

        // Instancie les plateformes de d�part et d'arriv�e
        Instantiate (topPlateformPrefab, posTop, Quaternion.identity, towerContent);
        Instantiate (groundPlateformPrefab, posGround, Quaternion.identity, towerContent);

        // Instancie al�atoirement les plateformes et les items
        Vector3 spawn_position = new Vector3 ();
        for (int i = 0; i < spawn_number; i++) {
            spawn_position.y += Random.Range (platformMinH, platformMaxH);
            spawn_position.x = Random.Range (platformMinL, platformMaxL);
            SpawnPlatform (spawn_position);
        }
    }

    private void SpawnPlatform (Vector3 position) {
        // Instanciation de la plateforme
        Transform newPlatform = Instantiate (plateformPrefab, position, Quaternion.identity, towerContent).transform;

        // Code caca pas beau, je referais avec des scriptableobjects je pense
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