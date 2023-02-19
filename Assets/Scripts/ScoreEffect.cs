using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEffect : MonoBehaviour {
	// Vitesse de flottement du texte
    public float floatSpeed;

	private void Start () {
		StartCoroutine (AutoDestroy ());
	}

	private void Update () {
		// Le texte se deplace vers le haut
        transform.Translate (Vector3.up * floatSpeed * Time.deltaTime);
    }

	private IEnumerator AutoDestroy () {
		yield return new WaitForSeconds (3f);
		Destroy (gameObject);
	}
}