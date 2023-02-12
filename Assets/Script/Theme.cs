using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New theme", menuName = "Tower theme", order = 1)]
public class Theme : ScriptableObject {
	public GameObject tower; // Préfab de la tour (grids, ciel, etc ...)
	public GameObject platform, topPlatform, groundPlatform; // Préfabs des plateformes
	public GameObject topSword; // Préfab du rocher de fin
}