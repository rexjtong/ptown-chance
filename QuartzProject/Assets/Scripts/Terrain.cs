using UnityEngine;
using System.Collections;

/* This script instantiates the grid by taking a message from MapLayoutManager.
 * Instantiate each prefab individually by calling information from each
 * MapNode.
 * */
public class Terrain : MonoBehaviour {
	
	void Awake () {
		Messenger.AddListener<MapNode[,]>("map layout", InstantiateTerrain);
	}
	
	/* Instantiates Terrain by taking a message from MapLayoutManager.
	 * Calls for information from each MapNode to instantiate different
	 * prefabs for each terrain type.
	 * */
	private void InstantiateTerrain(MapNode[,] TerrainLayout) {
		
		for(int x = 0; x < TerrainLayout.GetLength(0); x++) {
			for(int z = 0; z < TerrainLayout.GetLength(1); z++) {
				
				Transform gameObject = transform;
				Transform instance = (Transform) Instantiate(TerrainLayout[x,z].GetTerrain(), new Vector3(x, 0, z), Quaternion.identity);
				instance.parent = gameObject;
			}
		}
	}
}
