using UnityEngine;
using System.Collections;

public class PlacementBuilding : MonoBehaviour {
	
	public Transform PlacedBuilding;	// Actual Building
	
	private Vector3 TargetPosition;		// Position to be moved to
	private Vector2 TerrainEdge;		// Edges of map
	private bool Buildable;				// Gotten from MapLayoutManager
	private bool NowBuilding = false;	// Is building mode on
	private Vector3 StartingPosition;
	private float YPlacement;
	
	void Awake() {
		// Add Listeners
		Messenger.AddListener("give coordinates", GiveCoordinates);			// Listen from BuildingManager
		Messenger.AddListener("place building", PlaceBuilding);				// Listen from BuildingManager/CharacterController
		Messenger.AddListener("stop building", StopBuilding);				// Listen from BuildingManager
		Messenger.AddListener<bool>("building color", SetBuildableColor);	// Listen from BuildingManager
	}
	
	void Start () {
		Renderer renderer = gameObject.GetComponent<Renderer>();
		YPlacement = (renderer.bounds.max.y - renderer.bounds.min.y) / 2;
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
		
			Vector3 NewPosition = new Vector3((int) TargetPosition.x, (int) TargetPosition.y + YPlacement, (int) TargetPosition.z);
		
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
