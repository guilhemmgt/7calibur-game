using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New theme", menuName = "Tower theme", order = 1)]
public class Theme : ScriptableObject {
	public GameObject tower; // Prefab de la tour (grids, ciel, etc ...)
	public GameObject platform, topPlatform, groundPlatform; // Prefabs des plateformes
	public GameObject topSword; // Prefab du rocher de fin

	public GameObject spike; // Prefab des spikes

	public GameObject coin; // Prefab des coin
}