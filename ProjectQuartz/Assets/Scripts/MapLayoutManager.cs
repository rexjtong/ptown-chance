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
		Messenger.AddListener<GameObject>("enemy died", RemoveUnit);
		Messenger.AddListener<Vector3>("building died", RemoveBuilding);
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
		MapLayout[(int)Location.x, (int)Location.z].SetBuildingOnTop(true);
		MapLayout[(int)Location.x, (int)Location.z].SetPassable(false);
	}
	
	void UnitPositionChange(Vector3[] Location) {
		MapLayout[Mathf.RoundToInt(Location[0].x), Mathf.RoundToInt(Location[0].z)].RemoveUnitOnTop();
		MapLayout[Mathf.RoundToInt(Location[1].x), Mathf.RoundToInt(Location[1].z)].RemoveUnitOnTop();
		MapLayout[Mathf.RoundToInt(Location[1].x), Mathf.RoundToInt(Location[0].z)].RemoveUnitOnTop();
		MapLayout[Mathf.RoundToInt(Location[0].x), Mathf.RoundToInt(Location[1].z)].RemoveUnitOnTop();
		
		MapLayout[Mathf.RoundToInt(Location[2].x), Mathf.RoundToInt(Location[2].z)].AddUnitOnTop();
		MapLayout[Mathf.RoundToInt(Location[3].x), Mathf.RoundToInt(Location[3].z)].AddUnitOnTop();
		MapLayout[Mathf.RoundToInt(Location[3].x), Mathf.RoundToInt(Location[2].z)].AddUnitOnTop();
		MapLayout[Mathf.RoundToInt(Location[2].x), Mathf.RoundToInt(Location[3].z)].AddUnitOnTop();
	}
	
	// Sets position of unit start locations to be not buildable
	void UnitPositionStart(Vector3[] Location) {
		MapLayout[Mathf.RoundToInt(Location[0].x), Mathf.RoundToInt(Location[0].z)].AddUnitOnTop();
		MapLayout[Mathf.RoundToInt(Location[1].x), Mathf.RoundToInt(Location[1].z)].AddUnitOnTop();
		MapLayout[Mathf.RoundToInt(Location[1].x), Mathf.RoundToInt(Location[0].z)].AddUnitOnTop();
		MapLayout[Mathf.RoundToInt(Location[0].x), Mathf.RoundToInt(Location[1].z)].AddUnitOnTop();
	}
	
	void RemoveUnit(GameObject Unit) {
		Renderer renderer = Unit.GetComponent<Renderer>();
		Vector3 MinPosition = new Vector3(renderer.bounds.min.x, renderer.bounds.min.y, renderer.bounds.min.z);
		Vector3 MaxPosition = new Vector3(renderer.bounds.max.x, renderer.bounds.max.y, renderer.bounds.max.z);
		
		MapLayout[Mathf.RoundToInt(MinPosition.x), Mathf.RoundToInt(MinPosition.z)].RemoveUnitOnTop();
		MapLayout[Mathf.RoundToInt(MaxPosition.x), Mathf.RoundToInt(MaxPosition.z)].RemoveUnitOnTop();
		MapLayout[Mathf.RoundToInt(MaxPosition.x), Mathf.RoundToInt(MinPosition.z)].RemoveUnitOnTop();
		MapLayout[Mathf.RoundToInt(MinPosition.x), Mathf.RoundToInt(MaxPosition.z)].RemoveUnitOnTop();
	}
	
	void RemoveBuilding(Vector3 Location) {
		MapLayout[(int)Location.x, (int)Location.z].SetBuildingOnTop(false);
		MapLayout[(int)Location.x, (int)Location.z].SetPassable(true);
	}
}
