using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_System : MonoBehaviour {
    // Nb de tours vaincues
    public int Tower;
    // Score
    public float score;
    public float oldScore;
    public float newScore;
    public float climbScore;
    public float height;
    public float maxheight = 0;

    private float tempsDerniereExecution;
    public float delai;

    // Prefab du texte de score
    public GameObject scoreEffectPrefab;
    // Parent des textes de score
    private GameObject scoreEffectSpawner;

	private void Awake () {
        scoreEffectSpawner = transform.Find ("ScoreEffectPos").gameObject;
	}

    private void Update() {

        if(this.GetComponent<Player_Movement>().is7Calibur){
            tempsDerniereExecution += Time.fixedDeltaTime;		
		    if (tempsDerniereExecution > delai) {
			    AddScore(5);
			    tempsDerniereExecution = 0;			
		    }
        }
    }

    // Réinitialise le score à 0
    public void ResetScore () {
        score = 0;
        oldScore = 0;
        newScore = 0;
        climbScore = 0;

        Tower = 1;
	}

    // Ajoute du score et génère un effet
    public void AddScore (int amount) {
        score += amount;
        GameObject effect = Instantiate (scoreEffectPrefab, scoreEffectSpawner.transform.position, Quaternion.identity);
        effect.GetComponent<TextMeshPro> ().text = "+" + amount;
	}

    // Incrémente le nb de tours vaincues (et ajoute du score mais on l'enlèvera après du coup)
    public void AddTower () {
        Tower += 1;
        AddScore (100);
	}
}