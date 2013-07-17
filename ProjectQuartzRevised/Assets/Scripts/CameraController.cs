using UnityEngine;
using System.Collections;

// Script for camera movement. Camera should not move past set limits
public class CameraController : MonoBehaviour {

	public float MovementSpeed;		// Variable amount for movement speed
	public float MoveEdge;			// how many pixels away from edge for camera movement
	public bool CameraPan;			// panning enabled
	private Vector2 CameraLimits;	// camera pan limits
	
	void Awake () {
	}
	
	void LateUpdate () {
		// Check if button CameraUp is pressed
		if(Input.GetButton("CameraUp")) {
			// Move camera up
			float NewPosition = transform.position.z + MovementSpeed * Time.deltaTime;
			transform.position = new Vector3(transform.position.x, transform.position.y, NewPosition);
		}
		// Check if button CameraDown is pressed
		if(Input.GetButton("CameraDown")) {
			// Move camera down
			float NewPosition = transform.position.z - MovementSpeed * Time.deltaTime;
			transform.position = new Vector3(transform.position.x, transform.position.y, NewPosition);
		}
		// Check if button CameraLeft is pressed
		if(Input.GetButton("CameraLeft")) {
			// Move camera left
			float NewPosition = transform.position.x - MovementSpeed * Time.deltaTime;
			transform.position = new Vector3(NewPosition, transform.position.y, transform.position.z);
		}
		// Check if button CameraRight is pressed
		if(Input.GetButton("CameraRight")) {
			// Move camera right
			float NewPosition = transform.position.x + MovementSpeed * Time.deltaTime;
			transform.position = new Vector3(NewPosition, transform.position.y, transform.position.z);
		}
		if(CameraPan) {
			// Check if mouse position passes y bottom threshold
			if(Input.mousePosition.y <= 0 + MoveEdge) {
				// Move camera down
				float NewPosition = transform.position.z - MovementSpeed * Time.deltaTime;
				transform.position = new Vector3(transform.position.x, transform.position.y, NewPosition);
			}
			// Check if mouse position passes y top threshold
			if(Input.mousePosition.y >= Screen.height - MoveEdge) {
				// Move camera up
				float NewPosition = transform.position.z + MovementSpeed * Time.deltaTime;
				transform.position = new Vector3(transform.position.x, transform.position.y, NewPosition);
			}
			// Check if mouse position passes x left threshold
			if(Input.mousePosition.x <= 0 + MoveEdge) {
				// Move camera left
				float NewPosition = transform.position.x - MovementSpeed * Time.deltaTime;
				transform.position = new Vector3(NewPosition, transform.position.y, transform.position.z);
			}
			// Check if mouse position passes x right threshold
			if(Input.mousePosition.x >= Screen.width - MoveEdge) {
				// Move camera right
				float NewPosition = transform.position.x + MovementSpeed * Time.deltaTime;
				transform.position = new Vector3(NewPosition, transform.position.y, transform.position.z);
			}
		}
	}
}
