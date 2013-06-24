using UnityEngine;
using System.Collections;
using System;

/* This script oversees the array that stores information about the entire
 * map. Will update array as it receives messages from other scripts.
 * */
public class MapLayoutManager : MonoBehaviour {
	
	/* Array that stores values for each terrain type.
	 * Only used for initialization of MapNode array.
	 * */
	public int[,] TerrainLayout = new int[,]{{1,2,0,1,0,1,2,1}, {1,2,0,1,1,0,2,2}, {0,2,1,2,1,0,0,1}, {1,2,1,2,4,0,1,2},{1,2,1,2,0,0,1,2},{1,2,1,2,0,0,1,2},{1,2,1,2,0,0,1,2}, {1,2,0,1,1,0,2,2}, {1,2,0,1,1,0,2,2}, {1,2,0,1,1,0,2,2}, {1,2,0,1,1,0,2,2}, {1,2,0,1,1,0,2,2}, {1,2,0,1,1,0,2,2}, {1,2,0,1,1,0,2,2}, {1,2,0,1,1,0,2,2}, {1,2,0,1,1,0,2,2}};
	
	private MapNode[,] MapLayout;		// Array to store all MapNodes

	//***************************************************************//
	// Prefabs for terrain types. Add more types here
	public Transform Empty;
	public Transform Grass;
	public Transform Gravel;
	public Transform Stone;
	public Transform GenericFourByFour;
	
	void Awake () {
		Messenger.AddListener<Vector3>("building created", ChangeBuildable);
		Messenger.AddListener<Vector3>("need buildable", IsBuildable);
		Messenger.AddListener<Vector3[]>("unit position change", UnitPositionChange);
		Messenger.AddListener<Vector3>("unit position start", UnitPositionStart);
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
					MapLayout[x,z] = new MapNode(Empty); break;
				case 1:
					MapLayout[x,z] = new MapNode(Grass); break;
				case 2:
					MapLayout[x,z] = new MapNode(Gravel); break;
				case 3:
					MapLayout[x,z] = new MapNode(Stone); break;
				case 4:
					MapLayout[x,z] = new MapNode(GenericFourByFour); break;
				}
			}
		}
	}
	
	void IsBuildable(Vector3 Location) {
		bool Buildable;
		try{
			Buildable = MapLayout[(int)(Location.x), (int)Location.z].IsBuildable();
		}
		catch(IndexOutOfRangeException e) {
			Buildable = false;
		}
		Messenger.Broadcast<bool>("is buildable", Buildable);
	}
	
	void ChangeBuildable(Vector3 Location)  {
		MapLayout[(int)(Location.x), (int)Location.z].SetBuildable(false);
	}
	
	void UnitPositionChange(Vector3[] Location) {
		MapLayout[(int)(Location[0].x), (int)Location[0].z].SetBuildable(true);

		if(Location[0].x < MapLayout.GetLength(0) - 1) {
			MapLayout[(int)(Location[0].x + 1), (int)Location[0].z].SetBuildable(true);
			
			if(Location[0].z > 1) {
				MapLayout[(int)(Location[0].x + 1), (int)Location[0].z - 1].SetBuildable(true);
				
			}
			if(Location[0].z < MapLayout.GetLength(1) - 1) {
				MapLayout[(int)(Location[0].x + 1), (int)Location[0].z + 1].SetBuildable(true);
			}
		}
		if(Location[0].x > 0) {
			if(Location[0].z > 1) {
				MapLayout[(int)(Location[0].x), (int)Location[0].z - 1].SetBuildable(true);
			}
			if(Location[0].z < MapLayout.GetLength(1) - 1) {
				MapLayout[(int)(Location[0].x), (int)Location[0].z + 1].SetBuildable(true);
			}
		}
		if(Location[0].x > 1) {
			MapLayout[(int)(Location[0].x - 1), (int)Location[0].z].SetBuildable(true);
			
			if(Location[0].z > 1) {
				MapLayout[(int)(Location[0].x - 1), (int)Location[0].z - 1].SetBuildable(true);
			}
			if(Location[0].z < MapLayout.GetLength(1) - 1) {
				MapLayout[(int)(Location[0].x - 1), (int)Location[0].z + 1].SetBuildable(true);
			}
		}
		
		MapLayout[(int)(Location[1].x), (int)Location[1].z].SetBuildable(false);
		
		if(Location[1].x < MapLayout.GetLength(0) - 1) {
			MapLayout[(int)(Location[1].x + 1), (int)Location[1].z].SetBuildable(false);
			
			if(Location[1].z > 1) {
				MapLayout[(int)(Location[1].x + 1), (int)Location[1].z - 1].SetBuildable(false);
				
			}
			if(Location[1].z < MapLayout.GetLength(1) - 1) {
				MapLayout[(int)(Location[1].x + 1), (int)Location[1].z + 1].SetBuildable(false);
			}
		}
		if(Location[1].x > 0) {
			if(Location[1].z > 1) {
				MapLayout[(int)(Location[1].x), (int)Location[1].z - 1].SetBuildable(false);
			}
			if(Location[1].z < MapLayout.GetLength(1) - 1) {
				MapLayout[(int)(Location[1].x), (int)Location[1].z + 1].SetBuildable(false);
			}
		}
		if(Location[1].x > 1) {
			MapLayout[(int)(Location[1].x - 1), (int)Location[1].z].SetBuildable(false);
			
			if(Location[1].z > 1) {
				MapLayout[(int)(Location[1].x - 1), (int)Location[1].z - 1].SetBuildable(false);
			}
			if(Location[1].z < MapLayout.GetLength(1) - 1) {
				MapLayout[(int)(Location[1].x - 1), (int)Location[1].z + 1].SetBuildable(false);
			}
		}
	}
	
	void UnitPositionStart(Vector3 Location) {
		MapLayout[(int)(Location.x), (int)Location.z].SetBuildable(false);
	}
}
