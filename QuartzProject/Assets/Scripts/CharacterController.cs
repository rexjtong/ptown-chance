using UnityEngine;
using System.Collections;

/* Script used for movement of the main character. Movement is limited by
 * size of the map.
 * */
public class CharacterController : MonoBehaviour {
	
	private bool MovementOn = true;
	public float MovementSpeed;			// Movement Speed
	private Vector3 TargetPosition;		// Position to be moved to
	private Vector2 TerrainEdge;		// Edges of map
	
	void Awake () {
		Messenger.AddListener<MapNode[,]>("map layout", SetEdges);
		Messenger.AddListener("start building", ToggleMovement);
		Messenger.AddListener("stop building", ToggleMovement);
		Messenger.AddListener("place building", ToggleMovement);
	}
	
	void Start () {
		TargetPosition = transform.position;	// to set initial position
	}
	
	void Update () {
		// if right click set target location to move to
		if(Input.GetMouseButtonDown(1) && MovementOn) {
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
		
		// move to target location
		transform.position = Vector3.MoveTowards(transform.position, TargetPosition, MovementSpeed * Time.deltaTime);
	}
	
	// Set map edges for limiters
	void SetEdges(MapNode[,] MapEdges) {
		this.TerrainEdge = new Vector2(MapEdges.GetLength(0), MapEdges.GetLength(1));
	}
	
	void ToggleMovement() {
		MovementOn = !MovementOn;
	}
}
