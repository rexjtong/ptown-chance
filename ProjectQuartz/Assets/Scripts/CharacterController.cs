using UnityEngine;
using System.Collections;
using System;

/* Script used for movement of the main character. Movement is limited by
 * size of the map.
 * */
public class CharacterController : MonoBehaviour {
	
	private bool MovementOn = true;				// can character move
	public float MovementSpeed;					// Movement Speed
	private Vector3 TargetPosition;				// Position to be moved to
	private Vector2 TerrainEdge;				// Edges of map
	private bool NowBuilding = false;			// true when building grid is on
	private bool CancelFirstClick = false;		// To cancel first click when canceling building grid
	
	void Awake () {
		// Add listeners
		Messenger.AddListener<MapNode[,]>("map layout", SetEdges);					// Listen from MapLayoutManager
		Messenger.AddListener("start building", ToggleMovement);					// Listen from BuildingManager
		Messenger.AddListener("stop building", StopBuilding);						// Listen from BuildingManager
		Messenger.AddListener("place building", ToggleMovement);					// Listen from BuildingManager
		Messenger.AddListener<Vector3>("building coordinates", MoveToBuilding);		// Listen from PlacementBuilding
	}
	
	void Start () {
		TargetPosition = transform.position;	// to set initial position
	}
	
	void Update () {
		// Check if building grid is not on
		if(!NowBuilding) {
			// if right click set target location to move to
			if(Input.GetMouseButtonDown(1) && MovementOn && !CancelFirstClick) {
				Plane PlayerPlane = new Plane(Vector3.up, Vector3.zero);
				Ray TargetRay = Camera.main.ScreenPointToRay (Input.mousePosition);
				float HitDistance = 0.0f;
			
				if(PlayerPlane.Raycast(TargetRay, out HitDistance)) {
					Vector3 TargetPoint = TargetRay.GetPoint(HitDistance);
					if(TargetPoint.x >= .2 && TargetPoint.z >= 0 && TargetPoint.x <= TerrainEdge.x - 1 && TargetPoint.z <= TerrainEdge.y - 1) {
						TargetPosition = TargetRay.GetPoint(HitDistance);
						TargetPosition.y += .5f;
						var TargetRotation = Quaternion.LookRotation(TargetPoint - transform.position);
						TargetRotation.z = 0;
						TargetRotation.x = 0;
						transform.rotation = TargetRotation;
					}
				}
			}
			// Used for canceling first click after canceling grid
			if(CancelFirstClick) {
				CancelFirstClick = false;
			}
		}
		
		// If currently building
		if(NowBuilding) {
			// Check if right next to target building location
			if(Math.Abs(transform.position.x - TargetPosition.x) <= 1.5 && Math.Abs(transform.position.z - TargetPosition.z) <= 1.5) {
				TargetPosition = transform.position;			// Stop character
				Messenger.Broadcast("place building");			// Send to BuildingManager/PlacementBuilding
				NowBuilding = false;							// Turn off building mode
			}
		}
		
		// Move to target location
		transform.position = Vector3.MoveTowards(transform.position, TargetPosition, MovementSpeed * Time.deltaTime);
	}
	
	// Set map edges for limiters
	void SetEdges(MapNode[,] MapEdges) {
		this.TerrainEdge = new Vector2(MapEdges.GetLength(0), MapEdges.GetLength(1));
	}
	
	// Toggle movement on and off
	void ToggleMovement() {
		MovementOn = !MovementOn;
		CancelFirstClick = true;		// Set to cancel first right click
	}
	
	// Move to the building location
	void MoveToBuilding(Vector3 Location) {
		NowBuilding = true;
		TargetPosition = new Vector3(Location.x, .5f, Location.z);
		var TargetRotation = Quaternion.LookRotation(TargetPosition - transform.position);
		transform.rotation = TargetRotation;
	}
	
	// Stop building process
	void StopBuilding() {
		NowBuilding = false;
		ToggleMovement();
		TargetPosition = transform.position;
	}
}
