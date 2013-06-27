using UnityEngine;
using System.Collections;

public class UnitPositionManager : MonoBehaviour {
	
	private Vector3 CurrentPosition;	// Current Position in Map
	private Vector3 NewPosition;	// Changed Position in Map;
	
	void Start () {
		// Initializing Variables
		CurrentPosition = transform.position;
		NewPosition = transform.position;
		/*
		CurrentPosition.x = (int)transform.position.x;
   	 	CurrentPosition.y = (int) transform.position.y;
    	CurrentPosition.z = (int) transform.position.z;
		
		NewPosition.x = (int) transform.position.x;
   	 	NewPosition.y = (int) transform.position.y;
    	NewPosition.z = (int) transform.position.z;
    	*/
		
		Messenger.Broadcast<Vector3>("unit position start", CurrentPosition);	// Sent to MapLayoutManager
	}
	
	void Update () {
		// If position has changed
		if(NewPosition != transform.position) {
			NewPosition = transform.position;
		}
		/*
		if(NewPosition.x != (int) transform.position.x || NewPosition.y != (int) transform.position.y || NewPosition.z != (int) transform.position.z) {
			// Set new position
			NewPosition.x = (int) transform.position.x;
   	 		NewPosition.y = (int) transform.position.y;
    		NewPosition.z = (int) transform.position.z;
		} */

		// Message Map that position has been changed in Map
		if(!CurrentPosition.Equals (NewPosition)) {
			Vector3[] PositionChanges = {CurrentPosition, NewPosition};
			Messenger.Broadcast<Vector3[]>("unit position change", PositionChanges);
			CurrentPosition = NewPosition;
		}
		/*
		if(!CurrentPosition.Equals(NewPosition)) {
			Vector3[] PositionChanges = {CurrentPosition, NewPosition};					// Array to be sent
			Messenger.Broadcast<Vector3[]>("unit position change", PositionChanges);	// Sent to MapLayoutManager
			CurrentPosition = NewPosition;
		}
		*/
	}
}
