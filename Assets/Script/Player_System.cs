using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_System : MonoBehaviour {
    // Score
    public float Score;
    public float OldScore;
    public float NewScore;
    public float ClimbScore;
    public float height;
    public float maxheight = 0;

    // Tower
    public int Tower;

    private void Update () {
        // Score

        height = transform.position.y;

        if (height > maxheight && height <= 101f) {
            maxheight = height;
        }

        // Spagetti code pour regler un bug sur le reset du score à la mort (mais si on abandonne le score osef)
        if (height < 2 && !GetComponent<Player_Movement>().is7Calibur) {
            maxheight = 0f;
        }

        // Montee
        ClimbScore = maxheight;

        Score = OldScore + NewScore;

        NewScore = ClimbScore;
    }

    public void ResetScore () {
        Score = 0;
        OldScore = 0;
        NewScore = 0;
        ClimbScore = 0;

        Tower = 1;
	}
}