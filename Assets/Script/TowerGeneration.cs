using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGeneration : MonoBehaviour {
    // Préfabs
    // Plateformes
    public GameObject groundPlateformPrefab;
    public GameObject topPlateformPrefab;
    public GameObject plateformPrefab;
    // Items
    public GameObject swordPrefab;

    // Parent des plateformes et items générés dans la tour
    private Transform towerContent;

    // Position
    public Vector3 posGround;
    public Vector3 posTop;

    // Paramètres de spawn
    // Item
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
        towerContent = GameObject.Find ("TowerContent").transform;
	}

    // Supprime toutes les plateformes et items
	private void CleanTower () {
        foreach (Transform child in towerContent) {
            Destroy (child.gameObject);
        }
    }

    // Regénère une nouvelle tour
    public void GenerateTower () {
        CleanTower ();

        // Instancie les plateformes de départ et d'arrivée
        Instantiate (topPlateformPrefab, posTop, Quaternion.identity, towerContent);
        Instantiate (groundPlateformPrefab, posGround, Quaternion.identity, towerContent);

        // Instancie aléatoirement les plateformes et les items
        Vector3 spawn_position = new Vector3 ();
        for (int i = 0; i < spawn_number; i++) {
            spawn_position.y += Random.Range (Min_H, Max_H);
            spawn_position.x = Random.Range (Platform_Min_L, Platform_Max_L);
            SpawnPlatform (spawn_position);
        }
    }

    private void SpawnPlatform (Vector3 position) {
        // Instanciation de la plateforme
        Instantiate (plateformPrefab, position, Quaternion.identity, towerContent);

        // A une chance de spawn une épée
        if (Random.Range (0, 100) <= Frequence_Sword) {
            // Position de l'épée
            float xRandomTranslation = Random.Range (Sword_Min_L, Sword_Max_L);
            float yRandomTranslation = Random.Range (Sword_Min_H, Sword_Max_H);
            Vector3 swordPosition = position + new Vector3 (xRandomTranslation, yRandomTranslation, 0);
            // Instanciation de l'épée
            Transform newSword = Instantiate (swordPrefab, swordPosition, Quaternion.identity, towerContent).transform;
            // Rotation de l'épée
            float swordAngle = Random.Range (-50f, 50f);
            newSword.Rotate (0, 0, swordAngle, Space.World);
        }
    }
}