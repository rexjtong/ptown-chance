using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour {
	
	// Transforms for PreBuildings
	public Transform OneSpacePlacing;
	public Transform FourSpacePlacing;
	
	private bool BuildingNow = false;	// Is building mode on
	private bool Buildable;				// Is node buildable
	private Vector3 TargetPosition;		// Position to be moved
	private Vector2 TerrainEdge;		// Edges of map
	
	void Start () {
		// Add listeneres
		Messenger.AddListener<bool>("is buildable", SetBuildable);
		Messenger.AddListener("place building", StopBuildingNow);
	}
	
	// Update is called once per frame
	void Update () {
		if(!BuildingNow) {
			// When button pressed down show grid
			if(Input.GetButtonDown("ToggleGrid")) {
				Messenger.Broadcast("start building");
				BuildingNow = true;
				Instantiate(OneSpacePlacing);
			}
		}
		if(BuildingNow) {
			Messenger.Broadcast<bool>("building color", Buildable);
			if(Input.GetMouseButtonDown(1)) {
				Messenger.Broadcast("stop building");
				BuildingNow = false;
			}
			if(Buildable){
				if(Input.GetMouseButtonDown(0)) {
					Messenger.Broadcast("give coordinates");
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
