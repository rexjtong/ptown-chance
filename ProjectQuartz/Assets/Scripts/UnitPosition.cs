using UnityEngine;
using System.Collections;

public class UnitPosition : MonoBehaviour {
	
	private Vector3 NodePosition;	// Current Position in Map
	private Vector3 NewPosition;	// Changed Position in Map
	// public int GridSize = 1;
	
	void Start () {
		// Initializing Variables
		NodePosition.x = (int)transform.position.x;
   	 	NodePosition.y = (int) transform.position.y;
    	NodePosition.z = (int) transform.position.z;
		
		NewPosition.x = (int) transform.position.x;
   	 	NewPosition.y = (int) transform.position.y;
    	NewPosition.z = (int) transform.position.z;
		
		Messenger.Broadcast<Vector3>("unit position start", NodePosition);	// Sent to MapLayoutManager
	}
	
	void Update () {
		// If position has changed
		if(NewPosition != transform.position) {
			// Set new position
			NewPosition.x = (int) transform.position.x;
   	 		NewPosition.y = (int) transform.position.y;
    		NewPosition.z = (int) transform.position.z;
		}

		// Message Map that position has been changed in Map
		if(!NodePosition.Equals(NewPosition)) {
			Vector3[] PositionChanges = {NodePosition, NewPosition};					// Array to be sent
			Messenger.Broadcast<Vector3[]>("unit position change", PositionChanges);	// Sent to MapLayoutManager
			NodePosition = NewPosition;
		}
	}
}
