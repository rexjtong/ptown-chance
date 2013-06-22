using UnityEngine;
using System.Collections;

/* This script oversees the array that stores information about the entire
 * map. Will update array as it receives messages from other scripts.
 * */
public class MapLayoutManager : MonoBehaviour {
	
	/* Array that stores values for each terrain type.
	 * Only used for initialization of MapNode array.
	 * */
	public int[,] TerrainLayout = new int[,]{{1,2,0,1,0,1,2,1}, {1,2,0,1,1,0,2,2}, {0,2,1,2,1,0,0,1}, {1,2,1,2,0,0,1,2},{1,2,1,2,0,0,1,2},{1,2,1,2,0,0,1,2},{1,2,1,2,0,0,1,2}};
	
	private MapNode[,] MapLayout;		// Array to store all MapNodes

	//***************************************************************//
	// Prefabs for terrain types. Add more types here
	public Transform Grass;	
	public Transform Gravel;
	public Transform Stone;
	
	void Awake () {
		CreateMapLayout();
	}
	
	void Start () {
		Messenger.Broadcast<MapNode[,]>("map layout", MapLayout);
	}
	
	// Initializes MapNode array by using TerrainLayout.
	void CreateMapLayout() {
		MapLayout = new MapNode[TerrainLayout.GetLength(0), TerrainLayout.GetLength(1)];
		
		for(int x = 0; x < TerrainLayout.GetLength(0); x++) {
			for(int z = 0; z < TerrainLayout.GetLength (1); z++) {
				// Add more cases here for each type
				switch (TerrainLayout[x,z]) {
				case 0:
					MapLayout[x,z] = new MapNode(Grass); break;
				case 1:
					MapLayout[x,z] = new MapNode(Gravel); break;
				case 2:
					MapLayout[x,z] = new MapNode(Stone); break;
				}
			}
		}
	}
}
