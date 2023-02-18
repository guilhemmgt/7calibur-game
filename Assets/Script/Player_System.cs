using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_System : MonoBehaviour {
    // Scripts
    TowerGeneration towerGen;
    Player_Movement playerMvt;

    // Nb de tours vaincues
    public int Tower;
    // Score
    public int score;

    private float divLength;
    private float lastReachedDiv;
    private int anticheatCounter = 0;

    // Prefab du texte de score
    public GameObject scoreEffectPrefab;
    // Parent des textes de score
    private GameObject scoreEffectSpawner;
    private GameObject scoreEffectContent;

    public bool isBeginner = true;

    private void Awake () {
        towerGen = GameObject.Find ("GameManager").GetComponent<TowerGeneration> ();
        playerMvt = GetComponent<Player_Movement> ();

        scoreEffectSpawner = transform.Find ("ScoreEffectPos").gameObject;
        scoreEffectContent = GameObject.Find ("ScoreEffectContent");

        divLength = (towerGen.topPlatformPos.y - towerGen.groundPlatformPos.y) / 100;
        lastReachedDiv = towerGen.topPlatformPos.y;
    }

    private void Update () {
        if (playerMvt.is7Calibur) {
            if (transform.position.y < lastReachedDiv - divLength && lastReachedDiv > towerGen.groundPlatformPos.y) {
                if (anticheatCounter <= 100)
                    AddScore (1);
                anticheatCounter += 1;
                lastReachedDiv = lastReachedDiv - divLength;
            }
        } else {
            anticheatCounter = 0;
            lastReachedDiv = towerGen.topPlatformPos.y;
        }
    }

    // Réinitialise le score à 0
    public void ResetScore () {
        score = 0;
        Tower = 1;
    }

    public void ResetScoreEffect () {
        foreach (Transform child in scoreEffectContent.transform) {
            Destroy (child.gameObject);
        }
    }

    // Ajoute du score et génère un effet
    public void AddScore (int amount) {
        score += amount;
        GameObject effect = Instantiate (scoreEffectPrefab, scoreEffectSpawner.transform.position, Quaternion.identity, scoreEffectContent.transform);
        effect.GetComponent<TextMeshPro> ().text = "+" + amount;
    }

    // Incrémente le nb de tours vaincues (et ajoute du score mais on l'enlèvera après du coup)
    public void AddTower () {
        Tower += 1;
        isBeginner = false;
        AddScore (100);
    }
}