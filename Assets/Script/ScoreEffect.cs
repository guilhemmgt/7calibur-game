using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEffect : MonoBehaviour {
    public float floatSpeed;
	public float floatTime;

	private void Start () {
		AutoDestroy (floatTime);
	}

	private void Update () {
        transform.Translate (Vector3.up * floatSpeed * Time.deltaTime);
    }

	private IEnumerator AutoDestroy (float time) {
		yield return new WaitForSeconds (time);
		Destroy (gameObject);
	}
}