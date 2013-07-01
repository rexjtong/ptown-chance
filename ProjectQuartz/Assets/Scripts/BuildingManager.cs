using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour{
	
	// Transforms for PreBuildings
	public Transform OneByOneTowerPlacement;
	public Transform TwoByTwoPlacement;
	
	private bool BuildingNow = false;	// Is building mode on
	private bool Buildable;				// Is node buildable
	private Vector3 TargetPosition;		// Position to be moved
	private Vector2 TerrainEdge;		// Edges of map
	
	void Start () {
		// Add listeneres
		Messenger.AddListener<bool>("is buildable", SetBuildable);	// Listen from MapLayoutManager
		Messenger.AddListener("place building", StopBuildingNow);	// Listen from CharacterController
	}
	
	void Update () {
		// When building mode is not on
		if(!BuildingNow) {
			// When button pressed down show grid
			if(Input.GetButtonDown("ToggleGrid")) {
				Messenger.Broadcast("start building");				// Send to PlacementBuilding/CharacterController
				BuildingNow = true;
				Instantiate(OneByOneTowerPlacement);						// Create the PreBuilding
			}
		}
		// when building mode is on
		if(BuildingNow) {
			Messenger.Broadcast<bool>("building color", Buildable);	// Send to PlacementBuilding
			// right click stops building
			if(Input.GetMouseButtonDown(1)) {
				Messenger.Broadcast("stop building");				// Send to PlacementBuilding/CharacterController
				BuildingNow = false;
			}
			// Check if node is buildable
			if(Buildable){
				// left click
				if(Input.GetMouseButtonDown(0)) {
					Messenger.Broadcast("give coordinates");		// Send to PlacementBuilding
				}
			}
		}
	}
	
	// Store boolean for buildable on node
	void SetBuildable(bool Buildable) {
		this.Buildable = Buildable;
	}
	
	// Stop building process
	void StopBuildingNow() {
		BuildingNow = false;
	}
}
