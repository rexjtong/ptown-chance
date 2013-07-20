using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour {
	
	public Transform projectorPrefab;
	public Transform PreTier1Quartz;
	public Transform Tier1Quartz;
	
	private bool openBuildingMenu = false;
	private bool building = false;
	private bool projectorPlaced = false;
	private bool buildingPlaced = false;
	private bool nowPlacing = false;
	private string buildingMenu_String = "Open Building Menu";
	private int xRoundedPosition;
	private int zRoundedPosition;
	private Vector3 newPosition;
	private Vector3 currentPosition;
	private float Tower1x1YPlacement;
	private Transform placement;
	private Transform projector;
	
	private bool BuildingTower1x1;
	
	void Awake () {
		Messenger.AddListener("cancel building mode", CancelBuilding);
		Messenger.AddListener("place building", PlaceBuilding);
	}
	
	// Use this for initialization
	void Start () {
		Renderer renderer = PreTier1Quartz.GetComponent<Renderer>();
		Tower1x1YPlacement = (renderer.bounds.max.y - renderer.bounds.min.y) / 2;
	}
	
	void OnGUI() {
		if(!building) {
			if(GUI.Button(new Rect(0, 0, 150, 75), buildingMenu_String)) {
				if(!openBuildingMenu) {
					buildingMenu_String = "Close Building Menu";
					openBuildingMenu = true;
				}
				else {
					buildingMenu_String = "Open Building Menu";
					openBuildingMenu = false;
				}
			}
			if(openBuildingMenu) {
				if(GUI.Button(new Rect(0, 100, 150, 75), "Tier 1 Quartz Tower")) {
					buildingMenu_String = "Open Building Menu";
					openBuildingMenu = false;
					building = true;
					BuildingTower1x1 = true;
					Messenger.Broadcast("building mode");
				}
			}
		}
		
		if(building) {
			if(BuildingTower1x1) {
				if(Input.GetMouseButton(0)) {
					TowerPlacement towerPlacement = placement.GetComponent<TowerPlacement>();
					if(towerPlacement.isBuildable()) {
						Messenger.Broadcast<Vector3>("move to building", placement.transform.position);
						nowPlacing = true;
					}
				}
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				var ray_ground = Camera.main.ScreenPointToRay(Input.mousePosition);
				var ground_Layer = 1 << 8;
				RaycastHit hit_ground;
				
				if(Physics.Raycast(ray_ground, out hit_ground, Mathf.Infinity, ground_Layer) && !nowPlacing) {
					Vector3 nodePosition = new Vector3((int) hit_ground.point.x, (int) hit_ground.point.y, (int) hit_ground.point.z);
						
					if((int) hit_ground.point.x % 2 == 0)
						xRoundedPosition = (int) nodePosition.x - 1;
					else
						xRoundedPosition = (int) nodePosition.x;
		
					if((int) hit_ground.point.z % 2 == 0)
						zRoundedPosition = (int) nodePosition.z - 1;
					else
						zRoundedPosition = (int) nodePosition.z;
						
					newPosition = new Vector3( xRoundedPosition, 0, zRoundedPosition);
					
					if(currentPosition != newPosition) {
						if(!buildingPlaced) {
							placement = Instantiate(PreTier1Quartz) as Transform;
							buildingPlaced = true;
						}
						if(!projectorPlaced) {
							projector = Instantiate(projectorPrefab) as Transform;
							projector.forward = new Vector3(0, -1, 0);
							projector.parent = transform;
							projectorPlaced = true;
						}
						// BroadcastMessage("DestroySelf");
						currentPosition = new Vector3( xRoundedPosition, 0, zRoundedPosition);
						placement.transform.position = new Vector3(xRoundedPosition, hit_ground.point.y + Tower1x1YPlacement, zRoundedPosition);
						projector.transform.position = new Vector3(xRoundedPosition, 2, zRoundedPosition);
						projector.name = "Node(" + projector.position.x + "," + projector.position.z + ")";
					}
				}
			}
		}
	}
	
	void CancelBuilding() {
		openBuildingMenu = false;
		building = false;
		projectorPlaced = false;
		buildingPlaced = false;
		BuildingTower1x1 = false;
		nowPlacing = false;
		Destroy(placement.gameObject);
		Destroy(projector.gameObject);
	}
	
	void PlaceBuilding() {
		TowerPlacement towerPlacement = placement.GetComponent<TowerPlacement>();
		if(towerPlacement.isBuildable()) {
			Messenger.Broadcast("confirm building");
			projectorPlaced = false;
			buildingPlaced = false;
			building = false;
			BuildingTower1x1 = false;
			nowPlacing = false;
			Instantiate(Tier1Quartz, placement.transform.position, Quaternion.identity);
			AstarPath.active.UpdateGraphs (placement.collider.bounds);
			Destroy(placement.gameObject);
			Destroy(projector.gameObject);
		}
	}
}
