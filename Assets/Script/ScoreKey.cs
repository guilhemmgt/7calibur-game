using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKey : MonoBehaviour {
	public TextAsset keysTextAsset;

	private string[] keys;

	private void Start () {
		keys = keysTextAsset.text.Split ('\n');
	}

	public int GetKey (int score) {
		int scoreIndex = Mathf.FloorToInt (score / 100);
		return int.Parse (keys[scoreIndex]);
	}
}