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
		Plane PlayerPlane = new Plane(Vector3.up, new Vector3(0f, 0f, 0f));
		Ray TargetRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		float HitDistance = 0.0f;
			
		if(PlayerPlane.Raycast(TargetRay, out HitDistance)) {
			Messenger.Broadcast<Vector3>("need buildable", transform.position);
			
			TargetPosition = TargetRay.GetPoint(HitDistance);
		}
		
		int x = (int) TargetPosition.x;
		int y = (int) TargetPosition.y;
		int z = (int) TargetPosition.z;
		
		Vector3 NewPosition = new Vector3(x + .5f, y + 1, z);
		
		// TargetPosition.x = Mathf.Round(TargetPosition.x/GridSize) * GridSize + .5f;
   	 	// TargetPosition.y = Mathf.Round(TargetPosition.y/GridSize) * GridSize + 1f;
    	// TargetPosition.z = Mathf.Round(TargetPosition.z/GridSize) * GridSize;
		
		// move to target location
		// transform.position = TargetPosition;
		
		transform.position = NewPosition;
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
