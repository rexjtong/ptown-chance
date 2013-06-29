using UnityEngine;
using System.Collections;

public class UnitPositionManager : MonoBehaviour {
	
	private Vector3 CurrentPosition;	// Current Position in Map
	private Vector3 NewPosition;		// Changed Position in Map
	Vector3 OldMinPosition;
	Vector3 OldMaxPosition;
	Vector3 NewMinPosition;
	Vector3 NewMaxPosition;
	Vector3[] LocationArray;
	
	void Start () {
		// Initializing Variables
		Renderer renderer = gameObject.GetComponent<Renderer>();
		OldMinPosition = new Vector3(renderer.bounds.min.x, renderer.bounds.min.y, renderer.bounds.min.z);
		OldMaxPosition = new Vector3(renderer.bounds.max.x, renderer.bounds.max.y, renderer.bounds.max.z);
		NewMinPosition = new Vector3(renderer.bounds.min.x, renderer.bounds.min.y, renderer.bounds.min.z);
		NewMaxPosition = new Vector3(renderer.bounds.max.x, renderer.bounds.max.y, renderer.bounds.max.z);
		CurrentPosition = transform.position;
		NewPosition = transform.position;
		LocationArray = new Vector3[4]{OldMinPosition, OldMaxPosition, NewMinPosition, NewMaxPosition};
		
		// Messenger.Broadcast<Vector3>("unit position start", CurrentPosition);	// Sent to MapLayoutManager
		Messenger.Broadcast<Vector3[]>("unit position start", LocationArray);
	}
	
	void Update () {
		// If position has changed
		if(NewPosition != transform.position) {
			NewMinPosition = new Vector3(renderer.bounds.min.x, renderer.bounds.min.y, renderer.bounds.min.z);
			NewMaxPosition = new Vector3(renderer.bounds.max.x, renderer.bounds.max.y, renderer.bounds.max.z);
			LocationArray = new Vector3[4]{OldMinPosition, OldMaxPosition, NewMinPosition, NewMaxPosition};
			NewPosition = transform.position;
			
		}

		// Message Map that position has been changed in Map
		if(!CurrentPosition.Equals (NewPosition)) {
			// Vector3[] PositionChanges = {CurrentPosition, NewPosition};
			// Messenger.Broadcast<Vector3[]>("unit position change", PositionChanges);
			Messenger.Broadcast<Vector3[]>("unit position change", LocationArray);
			CurrentPosition = NewPosition;
			OldMinPosition = new Vector3(renderer.bounds.min.x, renderer.bounds.min.y, renderer.bounds.min.z);
			OldMaxPosition = new Vector3(renderer.bounds.max.x, renderer.bounds.max.y, renderer.bounds.max.z);
		}
	}
}
