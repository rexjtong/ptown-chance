using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour {

	public Transform OneSpacePlacing;
	public Transform FourSpacePlacing;
	
	private bool BuildingNow;
	private bool Buildable;
	private Vector3 TargetPosition;		// Position to be moved
	private Vector2 TerrainEdge;		// Edges of map
	
	void Start () {
		Messenger.AddListener<bool>("is buildable", SetBuildable);
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
			if(Input.GetButtonDown("CancelGrid")) {
				Messenger.Broadcast("stop building");
				BuildingNow = false;
			}
			if(Buildable){
				if(Input.GetMouseButtonDown(0)) {
					Messenger.Broadcast("place building");
					BuildingNow = false;
				}
			}
		}
	}
	
	void SetBuildable(bool Buildable) {
		this.Buildable = Buildable;
	}
}
