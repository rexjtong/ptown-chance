using UnityEngine;
using System.Collections;

// Script for camera movement. Camera should not move past set limits
public class CameraMovement : MonoBehaviour {
	
	public float MovementSpeed;		// Variable amount for movement speed
	public float MoveEdge;			// how many pixels away from edge for camera movement
	public bool CameraPan;			// panning enabled
	private Vector2 CameraLimits;	// camera pan limits
	
	void Awake () {
		Messenger.AddListener<MapNode[,]>("map layout", SetCameraLimits);
	}
	
	void Update () {
		// Check if button CameraUp is pressed
		if(Input.GetButton("CameraUp") && transform.position.z <= CameraLimits.y) {
			// Move camera up
			float NewPosition = transform.position.z + MovementSpeed * Time.deltaTime;
			transform.position = new Vector3(transform.position.x, transform.position.y, NewPosition);
		}
		// Check if button CameraDown is pressed
		if(Input.GetButton("CameraDown") && transform.position.z >= -3) {
			// Move camera down
			float NewPosition = transform.position.z - MovementSpeed * Time.deltaTime;
			transform.position = new Vector3(transform.position.x, transform.position.y, NewPosition);
		}
		// Check if button CameraLeft is pressed
		if(Input.GetButton("CameraLeft") && transform.position.x >= 5) {
			// Move camera left
			float NewPosition = transform.position.x - MovementSpeed * Time.deltaTime;
			transform.position = new Vector3(NewPosition, transform.position.y, transform.position.z);
		}
		// Check if button CameraRight is pressed
		if(Input.GetButton("CameraRight") && transform.position.x <= CameraLimits.x) {
			// Move camera right
			float NewPosition = transform.position.x + MovementSpeed * Time.deltaTime;
			transform.position = new Vector3(NewPosition, transform.position.y, transform.position.z);
		}
		if(CameraPan) {
			// Check if mouse position passes y bottom threshold
			if(Input.mousePosition.y <= 0 + MoveEdge && transform.position.z >= -3) {
				// Move camera down
				float NewPosition = transform.position.z - MovementSpeed * Time.deltaTime;
				transform.position = new Vector3(transform.position.x, transform.position.y, NewPosition);
			}
			// Check if mouse position passes y top threshold
			if(Input.mousePosition.y >= Screen.height - MoveEdge && transform.position.z <= CameraLimits.y) {
				// Move camera up
				float NewPosition = transform.position.z + MovementSpeed * Time.deltaTime;
				transform.position = new Vector3(transform.position.x, transform.position.y, NewPosition);
			}
			// Check if mouse position passes x left threshold
			if(Input.mousePosition.x <= 0 + MoveEdge && transform.position.x >= 5) {
				// Move camera left
				float NewPosition = transform.position.x - MovementSpeed * Time.deltaTime;
				transform.position = new Vector3(NewPosition, transform.position.y, transform.position.z);
			}
			// Check if mouse position passes x right threshold
			if(Input.mousePosition.x >= Screen.width - MoveEdge && transform.position.x <= CameraLimits.x) {
				// Move camera right
				float NewPosition = transform.position.x + MovementSpeed * Time.deltaTime;
				transform.position = new Vector3(NewPosition, transform.position.y, transform.position.z);
			}
		}
	}
	
	// Set limits for camera movement
	void SetCameraLimits(MapNode[,] MapEdges) {
		this.CameraLimits = new Vector2(MapEdges.GetLength(0)-3, MapEdges.GetLength(1)-7);
	}
}
