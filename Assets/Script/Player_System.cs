using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_System : MonoBehaviour {
    // Score
    public float score;
    public float oldScore;
    public float newScore;
    public float climbScore;
    public float height;
    public float maxheight = 0;

    // Tower
    public int Tower;

    private void Update () {
        // Score

        //height = transform.position.y;

        //if (height > maxheight && height <= 101f) {
        //    maxheight = height;
        //}

        //// Spagetti code pour regler un bug sur le reset du score à la mort (mais si on abandonne le score osef)
        //if (height < 2 && !GetComponent<Player_Movement>().is7Calibur) {
        //    maxheight = 0f;
        //}

        //// Montee
        //climbScore = maxheight;

        //score = oldScore + newScore;

        //newScore = climbScore;
    }

    public void ResetScore () {
        score = 0;
        oldScore = 0;
        newScore = 0;
        climbScore = 0;

        Tower = 1;
	}

    public void AddScore (int amount) {
        score += amount;
	}

    public void AddTower () {
        Tower += 1;
        AddScore (100);
	}
}