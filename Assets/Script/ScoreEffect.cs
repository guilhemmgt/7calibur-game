using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEffect : MonoBehaviour {
    public float floatSpeed;


	private void Start () {
		StartCoroutine (AutoDestroy ());
	}

	private void Update () {
        transform.Translate (Vector3.up * floatSpeed * Time.deltaTime);
    }

	private IEnumerator AutoDestroy () {
		yield return new WaitForSeconds (3f);
		Destroy (gameObject);
	}
}