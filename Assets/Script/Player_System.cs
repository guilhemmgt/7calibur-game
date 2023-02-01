using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_System : MonoBehaviour {
    // Score
    public float Score;
    public float OldScore;
    public float NewScore;
    public float FallScore;
    public float ClimbScore;
    public float height;
    public float maxheight = 0;
    public int CompteurCollision;
    public float CollisionPenalty;

    // Etats
    public bool isOver = false;
    public bool is7Calibur = false;

    // Tower
    public int Tower;

    void Update () {
        // Score

        height = transform.position.y;

        if (height > maxheight && height <= 101f) {
            maxheight = height;
        }

        // Spagetti code pour regler un bug sur le reset du score à la mort (mais si on abandonne le score osef)
        if (height < 2 && !is7Calibur) {
            maxheight = 0f;
        }

        // Montee
        ClimbScore = maxheight;
        // Descente
        FallScore = 100 - CollisionPenalty * CompteurCollision;

        Score = OldScore + NewScore;

        NewScore = ClimbScore + FallScore;
    }
}