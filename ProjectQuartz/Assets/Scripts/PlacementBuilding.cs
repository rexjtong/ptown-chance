using UnityEngine;
using System.Collections;

public class PlacementBuilding : MonoBehaviour {
	
	public Transform PlacedBuilding;	// Actual Building
	private Vector3 TargetPosition;		// Position to be moved to
	private Vector2 TerrainEdge;		// Edges of map
	// public int GridSize = 1;
	private bool Buildable;				// Gotten from MapLayoutManager
	private bool NowBuilding = false;
	
	void Awake() {
		// Add Listeners
		Messenger.AddListener("give coordinates", GiveCoordinates);
		Messenger.AddListener("place building", PlaceBuilding);
		Messenger.AddListener("stop building", StopBuilding);
		Messenger.AddListener<bool>("building color", SetBuildableColor);
	}
	
	void Start () {
		TargetPosition = transform.position;	// Initial position so building doesn't fly to origin
	}
	
	void Update () {
		if(!NowBuilding) {
			// Code for finding mouse clicking position on map
			Plane TerrainPlane = new Plane(Vector3.up, new Vector3(0f, 0f, 0f));
			Ray TargetRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			float HitDistance = 0.0f;
		
			if(TerrainPlane.Raycast(TargetRay, out HitDistance)) {
				Messenger.Broadcast<Vector3>("need buildable", transform.position);
			
				TargetPosition = TargetRay.GetPoint(HitDistance);
			}
		
			Vector3 NewPosition = new Vector3((int) TargetPosition.x, (int) TargetPosition.y, (int) TargetPosition.z);
		
		// TargetPosition.x = Mathf.Round(TargetPosition.x/GridSize) * GridSize + .5f;
   	 	// TargetPosition.y = Mathf.Round(TargetPosition.y/GridSize) * GridSize + 1f;
    	// TargetPosition.z = Mathf.Round(TargetPosition.z/GridSize) * GridSize;
		
		// move to target location
		// transform.position = TargetPosition;
		
			transform.position = NewPosition;		// Move to new position
		}
	}
	
	/* Cleans up PreBuilding and messages MapLayoutManager about the position of new 
	 * building
	 * */
	void PlaceBuilding() {
		Instantiate(PlacedBuilding, transform.position, Quaternion.identity);
		Messenger.Broadcast<Vector3>("building created", transform.position);
		Messenger.RemoveListener("place building", PlaceBuilding );
		Messenger.RemoveListener("stop building", StopBuilding );
		Messenger.RemoveListener<bool>("building color", SetBuildableColor);
		Messenger.RemoveListener("give coordinates", GiveCoordinates);
		Destroy (gameObject);
	}
	
	/* Cleans up PreBuilding
	 * */
	void StopBuilding() {
		Messenger.RemoveListener("place building", PlaceBuilding );
		Messenger.RemoveListener("stop building", StopBuilding );
		Messenger.RemoveListener<bool>("building color", SetBuildableColor);
		Messenger.RemoveListener("give coordinates", GiveCoordinates);
		Destroy (gameObject);
	}
	
	/* Change color of PreBuilding according to buildable and not buildable
	 * */
	void SetBuildableColor(bool Buildable) {
		Renderer renderer = GetComponentInChildren<Renderer>();
		if(Buildable) {
			renderer.material.color = Color.green;
		}
		if(!Buildable)
		{
			renderer.material.color = Color.red;
		}
	}
	
	// Give coordinates of placement building to character
	void GiveCoordinates() {
		NowBuilding = true;
		Messenger.Broadcast<Vector3>("building coordinates", transform.position);
	}
}
