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
		
		Messenger.Broadcast<Vector3>("unit position start", NodePosition);
		
		// NodePosition.x = Mathf.Round(transform.position.x/GridSize) * GridSize - .5f;
   	 	// NodePosition.y = Mathf.Round(transform.position.y/GridSize) * GridSize;
    	// NodePosition.z = Mathf.Round(transform.position.z/GridSize) * GridSize;
		
		// NewPosition.x = Mathf.Round(transform.position.x/GridSize) * GridSize - .5f;
   	 	// NewPosition.y = Mathf.Round(transform.position.y/GridSize) * GridSize;
    	// NewPosition.z = Mathf.Round(transform.position.z/GridSize) * GridSize;
	}
	
	void Update () {
		// Change position
		if(NewPosition != transform.position) {
			NewPosition.x = (int) transform.position.x;
   	 		NewPosition.y = (int) transform.position.y;
    		NewPosition.z = (int) transform.position.z;
		}
		
		// NewPosition.x = Mathf.Round(transform.position.x/GridSize) * GridSize - .5f;
   	 	// NewPosition.y = Mathf.Round(transform.position.y/GridSize) * GridSize;
    	// NewPosition.z = Mathf.Round(transform.position.z/GridSize) * GridSize;
		
		// Message Map that position has been changed in Map
		if(!NodePosition.Equals(NewPosition)) {
			Vector3[] PositionChanges = {NodePosition, NewPosition};
			Messenger.Broadcast<Vector3[]>("unit position change", PositionChanges);
			NodePosition = NewPosition;
		}
	}
}
