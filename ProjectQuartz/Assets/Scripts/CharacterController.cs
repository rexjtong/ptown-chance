using UnityEngine;
using System.Collections;
using System;

/* Script used for movement of the main character. Movement is limited by
 * size of the map.
 * */
public class CharacterController : MonoBehaviour {
	
	private bool MovementOn = true;		// can character move
	public float MovementSpeed;			// Movement Speed
	private Vector3 TargetPosition;		// Position to be moved to
	private Vector2 TerrainEdge;		// Edges of map
	private bool NowBuilding = false;	// true when building grid is on
	private bool CancelFirstClick = false;		// To cancel first click when canceling building grid
	
	void Awake () {
		// Add listeners
		Messenger.AddListener<MapNode[,]>("map layout", SetEdges);
		Messenger.AddListener("start building", ToggleMovement);
		Messenger.AddListener("stop building", StopBuilding);
		Messenger.AddListener("place building", ToggleMovement);
		Messenger.AddListener<Vector3>("building coordinates", MoveToBuilding);
	}
	
	void Start () {
		TargetPosition = transform.position;	// to set initial position
	}
	
	void Update () {
		// Check if building grid is not on
		if(!NowBuilding) {
			// if right click set target location to move to
			if(Input.GetMouseButtonDown(1) && MovementOn && !CancelFirstClick) {
				Plane PlayerPlane = new Plane(Vector3.up, transform.position);
				Ray TargetRay = Camera.main.ScreenPointToRay (Input.mousePosition);
				float HitDistance = 0.0f;
			
				if(PlayerPlane.Raycast(TargetRay, out HitDistance)) {
					Vector3 TargetPoint = TargetRay.GetPoint(HitDistance);
					if(TargetPoint.x >= .5 && TargetPoint.z >= -.3 && TargetPoint.x <= TerrainEdge.x - .5 && TargetPoint.z <= TerrainEdge.y - 1) {
						TargetPosition = TargetRay.GetPoint(HitDistance);
						var TargetRotation = Quaternion.LookRotation(TargetPoint - transform.position);
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
			if(Math.Abs(transform.position.x - TargetPosition.x) <= 1 && Math.Abs(transform.position.z - TargetPosition.z) <= 1) {
				TargetPosition = transform.position;			// stop
				Messenger.Broadcast("place building");			// broadcast place building
				NowBuilding = false;							// turn off building mode
			}
		}
		
		// move to target location
		transform.position = Vector3.MoveTowards(transform.position, TargetPosition, MovementSpeed * Time.deltaTime);
	}
	
	// Set map edges for limiters
	void SetEdges(MapNode[,] MapEdges) {
		this.TerrainEdge = new Vector2(MapEdges.GetLength(0), MapEdges.GetLength(1));
	}
	
	// toggle movement on and off
	void ToggleMovement() {
		MovementOn = !MovementOn;
		CancelFirstClick = true;		// set to cancel first right click
	}
	
	// Move to the building location
	void MoveToBuilding(Vector3 Location) {
		NowBuilding = true;
		TargetPosition = new Vector3(Location.x, Location.y + .5f, Location.z);
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
