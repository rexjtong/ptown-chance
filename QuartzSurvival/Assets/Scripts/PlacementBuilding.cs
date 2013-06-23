using UnityEngine;
using System.Collections;

public class PlacementBuilding : MonoBehaviour {
	
	public Transform PlacedBuilding;
	private Vector3 TargetPosition;		// Position to be moved to
	private Vector2 TerrainEdge;		// Edges of map
	public int GridSize = 1;
	private bool Buildable;
	
	void Awake() {
		Messenger.AddListener("place building", PlaceBuilding);
		Messenger.AddListener("stop building", StopBuilding);
		Messenger.AddListener<bool>("building color", SetBuildableColor);
	}
	
	// Use this for initialization
	void Start () {
		TargetPosition = transform.position;
	}
	
	void Update () {
		Plane PlayerPlane = new Plane(Vector3.up, transform.position);
		Ray TargetRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		float HitDistance = 0.0f;
			
		if(PlayerPlane.Raycast(TargetRay, out HitDistance)) {
			Messenger.Broadcast<Vector3>("need buildable", transform.position);
			
			//if(TargetPoint.x >= .5 && TargetPoint.z >= -.3 && TargetPoint.x <= TerrainEdge.x && TargetPoint.z <= TerrainEdge.y) {
				TargetPosition = TargetRay.GetPoint(HitDistance);
			//}
		}
		
		TargetPosition.x = Mathf.Round(TargetPosition.x/GridSize) * GridSize - .5f;
   	 	TargetPosition.y = Mathf.Round(TargetPosition.y/GridSize) * GridSize;
    	TargetPosition.z = Mathf.Round(TargetPosition.z/GridSize) * GridSize + 2f;
		// move to target location
		transform.position = TargetPosition;
	}
	
	void PlaceBuilding() {
		Instantiate(PlacedBuilding, transform.position, Quaternion.identity);
		Messenger.Broadcast<Vector3>("building created", transform.position);
		Messenger.RemoveListener("place building", PlaceBuilding );
		Messenger.RemoveListener("stop building", StopBuilding );
		Messenger.RemoveListener<bool>("building color", SetBuildableColor);
		Destroy (gameObject);
	}
	
	void StopBuilding() {
		Messenger.RemoveListener("place building", PlaceBuilding );
		Messenger.RemoveListener("stop building", StopBuilding );
		Messenger.RemoveListener<bool>("building color", SetBuildableColor);
		Destroy (gameObject);
	}
	
	void SetBuildableColor(bool Buildable) {
		Renderer renderer = GetComponent<Renderer>();
		if(Buildable) {
			renderer.material.color = Color.green;
		}
		if(!Buildable)
		{
			renderer.material.color = Color.red;
		}
	}
}
