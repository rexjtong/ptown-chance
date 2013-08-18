using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour {
	
	public Transform projectorPrefab;		// Used for projector
	
	// Add prebuildings here
	public Transform PreBasicTower;
	public Transform PreQuarryModel;
	
	// Add actual buildings here
	public Transform QuarryModel;
	public Transform BasicTower;
	
	private bool openBuildingMenu = false;	// controls what the building menu button says
	private bool building = false;			// controls whether player is currently buildng
	private bool buildingQuary = false;		// true when player is building quarry
	private bool buildingPlaced = false;	// used to control whether building is placed
	private bool nowPlacing = false;		// true when player is currently moving to build building
	private string buildingMenu_String = "Open Building Menu";	// string for building menu
	private int xRoundedPosition;			// used to round for building grid
	private int zRoundedPosition;			// used to round for building grid
	private Vector3 newPosition;			// used to control projector and building placement
	private Vector3 currentPosition;		// used to control projector and building placement
	private Transform placement;			// transform to store current placing transform
	private Transform ore;					// used to store transform of ore
	private Transform collider;
	
	// Add booleans for different buildings; controls which building is being built
	private bool Building1x1;
	
	void Awake () {
		Messenger.AddListener("cancel building mode", CancelBuilding);	// Listens from PlayerController
		Messenger.AddListener("place building", PlaceBuilding);			// Listens from PlayerController
	}

	void OnGUI() {
		// when game is not in building mode
		if(!building) {
			// button for building menu
			if(GUI.Button(new Rect(0, 0, 150, 75), buildingMenu_String)) {
				// closes building menu
				if(!openBuildingMenu) {
					buildingMenu_String = "Close Building Menu";
					openBuildingMenu = true;
				}
				// opens building menu
				else {
					buildingMenu_String = "Open Building Menu";
					openBuildingMenu = false;
				}
			}
			// buttons appear when building menu is open
			if(openBuildingMenu) {
				// button to build quarry
				if(GUI.Button(new Rect(0, 100, 150, 75), "Quary")) {
					buildingMenu_String = "Open Building Menu";
					openBuildingMenu = false;
					// building = true;
					buildingQuary = true;
					Messenger.Broadcast("building mode");
				}
				// button to build basic tower
				if(GUI.Button(new Rect(0, 200, 150, 75), "Tier 1 Quartz Tower")) {
					buildingMenu_String = "Open Building Menu";
					openBuildingMenu = false;
					// building = true;
					Building1x1 = true;
					Messenger.Broadcast("building mode");
				}
			}
		}
		
		if(buildingQuary) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			var ray_ore = Camera.main.ScreenPointToRay(Input.mousePosition);
			var ore_Layer = 1 << 11;
			RaycastHit hit_ore;
			
			if(Physics.Raycast(ray_ore, out hit_ore, Mathf.Infinity, ore_Layer) && !nowPlacing) {
				ore = hit_ore.transform;
				if(!buildingPlaced){
					Vector3 location = hit_ore.transform.position;
					placement = Instantiate(PreQuarryModel, location, Quaternion.identity) as Transform;
					buildingPlaced = true;
				}
				if(Input.GetMouseButton(0)) {
					Messenger.Broadcast<Vector3>("move to building", placement.transform.position);
					nowPlacing = true;
				}
			}
			else {
				if(buildingPlaced && !nowPlacing){
					Destroy(placement.gameObject);
					buildingPlaced = false;
				}
			}
		}
		if(Building1x1) {
			if(Input.GetMouseButton(0)) {
				BuildingPlacement buildingPlacement = placement.GetComponent<BuildingPlacement>();
				if(buildingPlacement.isBuildable()) {
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
						placement = Instantiate(PreBasicTower) as Transform;
						buildingPlaced = true;
					}

					currentPosition = new Vector3( xRoundedPosition, 0, zRoundedPosition);
					placement.transform.position = new Vector3(xRoundedPosition, hit_ground.point.y, zRoundedPosition);
				}
			}
		}
	}
	
	void CancelBuilding() {
		openBuildingMenu = false;
		buildingPlaced = false;
		Building1x1 = false;
		buildingQuary = false;
		nowPlacing = false;
		Destroy(placement.gameObject);
	}
	
	void PlaceBuilding() {
		if(buildingQuary) {
			Messenger.Broadcast("confirm building");
			buildingPlaced = false;
			Building1x1 = false;
			nowPlacing = false;
			buildingQuary = false;
			collider = Instantiate(QuarryModel, ore.position, Quaternion.identity) as Transform;
			Quarry oreType = collider.GetComponent<Quarry>();
			// Instantiate(ore, ore.position, Quaternion.identity);
			// oreType.oreType = ore.gameObject.name;
			Destroy(ore.gameObject);
			
			Transform buildingCollider = collider.Find("Model");
			AstarPath.active.UpdateGraphs (buildingCollider.collider.bounds);
			Destroy(placement.gameObject);
		}
		
		else {
			BuildingPlacement buildingPlacement = placement.GetComponent<BuildingPlacement>();
			if(buildingPlacement.isBuildable()) {
				Messenger.Broadcast("confirm building");
				buildingPlaced = false;
				Building1x1 = false;
				nowPlacing = false;
				collider = Instantiate(BasicTower, placement.transform.position, Quaternion.identity) as Transform;
			
				Transform buildingCollider = collider.Find("Model");
				AstarPath.active.UpdateGraphs (buildingCollider.collider.bounds);
				Destroy(placement.gameObject);
			}
		}
	}
}
