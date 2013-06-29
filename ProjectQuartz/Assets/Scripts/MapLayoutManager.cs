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
	public int[,] TerrainLayout = new int[,]{{1,2,3,1,3,1,2,1}, {1,2,3,1,1,3,2,2}, {3,2,1,2,1,3,3,1}, {1,2,1,2,1,3,1,2},{1,2,1,2,3,3,1,2},{1,2,1,2,3,3,1,2},{1,2,1,2,3,3,1,2}, {1,2,3,1,1,3,2,2}, {1,2,3,1,1,3,2,2}, {1,2,3,1,1,3,2,2}, {1,2,3,1,1,3,2,2}, {1,2,3,1,1,3,2,2}, {1,2,3,1,1,3,2,2}, {1,2,3,1,1,3,2,2}, {1,2,3,1,1,3,2,2}, {1,2,3,1,1,3,2,2}};
	
	private MapNode[,] MapLayout1;		// Array to store all MapNodes
	private MapNode[,] MapLayout;

	//***************************************************************//
	// Prefabs for terrain types. Add more types here
	public Transform Empty;
	public Transform Grass;
	public Transform Gravel;
	public Transform Stone;
	public Transform GenericFourByFour;
	
	void Awake () {
		// Add listeners
		Messenger.AddListener<Vector3>("building created", NewBuilding);				// Listen from PlacementBuilding
		Messenger.AddListener<Vector3>("need buildable", IsBuildable);					// Listen from PlacementBuilding
		Messenger.AddListener<Vector3[]>("unit position change", UnitPositionChange);	// Listen from UnitPositionManager
		Messenger.AddListener<Vector3[]>("unit position start", UnitPositionStart);		// Listen from UnitPositionManager
		CreateMapLayout();
	}
	
	void Start () {
		// broadcast the map layout
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
	
	// Check for buildable
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
	
	// Changes 9x9 square around unit to be not buildable
	void NewBuilding(Vector3 Location)  {
		MapLayout[(int)Location.x, (int)Location.z].SetBuildable(false);
		MapLayout[(int)Location.x, (int)Location.z].SetTraversable(false);
	}
	
	void UnitPositionChange(Vector3[] Location) {
		MapLayout[Mathf.RoundToInt(Location[0].x), Mathf.RoundToInt(Location[0].z)].SetBuildable(true);
		MapLayout[Mathf.RoundToInt(Location[1].x), Mathf.RoundToInt(Location[1].z)].SetBuildable(true);
		MapLayout[Mathf.RoundToInt(Location[1].x), Mathf.RoundToInt(Location[0].z)].SetBuildable(true);
		MapLayout[Mathf.RoundToInt(Location[0].x), Mathf.RoundToInt(Location[1].z)].SetBuildable(true);
		
		MapLayout[Mathf.RoundToInt(Location[2].x), Mathf.RoundToInt(Location[2].z)].SetBuildable(false);
		MapLayout[Mathf.RoundToInt(Location[3].x), Mathf.RoundToInt(Location[3].z)].SetBuildable(false);
		MapLayout[Mathf.RoundToInt(Location[3].x), Mathf.RoundToInt(Location[2].z)].SetBuildable(false);
		MapLayout[Mathf.RoundToInt(Location[2].x), Mathf.RoundToInt(Location[3].z)].SetBuildable(false);
		
		/*
		Vector3 OldLocation = new Vector3(Mathf.Round(Location[0].x - .5f), Mathf.Round(Location[0].y), Mathf.Round(Location[0].z - .5f));
		Vector3 NewLocation = new Vector3(Mathf.Round(Location[1].x - .5f), Mathf.Round(Location[1].y), Mathf.Round(Location[1].z - .5f));
		
		MapLayout[(int)OldLocation.x, (int)OldLocation.z].SetBuildable(true);
		MapLayout[(int)OldLocation.x + 1, (int)OldLocation.z].SetBuildable(true);
		MapLayout[(int)OldLocation.x, (int)OldLocation.z + 1].SetBuildable(true);
		MapLayout[(int)OldLocation.x +1, (int)OldLocation.z + 1].SetBuildable(true);
		
		MapLayout[(int)NewLocation.x, (int)NewLocation.z].SetBuildable(false);
		MapLayout[(int)NewLocation.x + 1, (int)NewLocation.z].SetBuildable(false);
		MapLayout[(int)NewLocation.x, (int)NewLocation.z + 1].SetBuildable(false);
		MapLayout[(int)NewLocation.x + 1, (int)NewLocation.z + 1].SetBuildable(false);
		*/
	}
	
	// Sets position of unit start locations to be not buildable
	void UnitPositionStart(Vector3[] Location) {
		MapLayout[Mathf.RoundToInt(Location[0].x), Mathf.RoundToInt(Location[0].z)].SetBuildable(false);
		MapLayout[Mathf.RoundToInt(Location[1].x), Mathf.RoundToInt(Location[1].z)].SetBuildable(false);
		MapLayout[Mathf.RoundToInt(Location[1].x), Mathf.RoundToInt(Location[0].z)].SetBuildable(false);
		MapLayout[Mathf.RoundToInt(Location[0].x), Mathf.RoundToInt(Location[1].z)].SetBuildable(false);
		// MapLayout[(int)Location[0].x, (int)Location[0].z].SetBuildable(false);
		
		
		/*
		MapLayout[(int)Location.x, (int)Location.z].SetBuildable(false);
		MapLayout[(int)Location.x + 1, (int)Location.z].SetBuildable(false);
		MapLayout[(int)Location.x, (int)Location.z + 1].SetBuildable(false);
		MapLayout[(int)Location.x + 1, (int)Location.z + 1].SetBuildable(false);
		*/
	}
}
