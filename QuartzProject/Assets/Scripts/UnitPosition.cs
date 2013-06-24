using UnityEngine;
using System.Collections;

public class UnitPosition : MonoBehaviour {
	
	private Vector3 NodePosition;
	private Vector3 NewPosition;
	// public int GridSize = 1;
	
	// Use this for initialization
	void Start () {
		NodePosition.x = (int)transform.position.x + .5f;
   	 	NodePosition.y = (int) transform.position.y;
    	NodePosition.z = (int) transform.position.z;
		
		NewPosition.x = (int) transform.position.x + .5f;
   	 	NewPosition.y = (int) transform.position.y;
    	NewPosition.z = (int) transform.position.z;
		
		// NodePosition.x = Mathf.Round(transform.position.x/GridSize) * GridSize - .5f;
   	 	// NodePosition.y = Mathf.Round(transform.position.y/GridSize) * GridSize;
    	// NodePosition.z = Mathf.Round(transform.position.z/GridSize) * GridSize;
		
		// NewPosition.x = Mathf.Round(transform.position.x/GridSize) * GridSize - .5f;
   	 	// NewPosition.y = Mathf.Round(transform.position.y/GridSize) * GridSize;
    	// NewPosition.z = Mathf.Round(transform.position.z/GridSize) * GridSize;
	}
	
	// Update is called once per frame
	void Update () {
		NewPosition.x = (int) transform.position.x + .5f;
   	 	NewPosition.y = (int) transform.position.y;
    	NewPosition.z = (int) transform.position.z;
		
		// NewPosition.x = Mathf.Round(transform.position.x/GridSize) * GridSize - .5f;
   	 	// NewPosition.y = Mathf.Round(transform.position.y/GridSize) * GridSize;
    	// NewPosition.z = Mathf.Round(transform.position.z/GridSize) * GridSize;
		
		if(!NodePosition.Equals(NewPosition)) {
			Vector3[] PositionChanges = {NodePosition, NewPosition};
			Messenger.Broadcast<Vector3[]>("unit position change", PositionChanges);
			NodePosition = NewPosition;
		}
	}
}
