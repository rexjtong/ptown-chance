using UnityEngine;
using System.Collections;

/* Script for setting up building grid. Takes in messages from
 * MapLayoutManager for information on map layout.
 * */
public class GridLayout : MonoBehaviour {
	
	public Transform GridPrefab;	// prefab for grid
	
	void Awake () {
		Messenger.AddListener<MapNode[,]>("map layout", CreateGrid);
	}
	
	void Start () {
	}
	
	// Change later to support toggling grid in "building mode"
	void Update() {
		// When button pressed down show grid
		if(Input.GetButtonDown("ToggleGrid")) {
			ToggleVisibility();
		}
		// When button lifted up hide grid
		if(Input.GetButtonUp("ToggleGrid")) {
			ToggleVisibility();
		}
	}
	
	/* CreateGrid() uses info from MapLayoutManager for dimensions
	 * of map to instantiate the grid.
	 * */
	void CreateGrid(MapNode[,] GridLength) {
		for(int x = 0; x < GridLength.GetLength(0); x++) {
			for(int z = 0; z < GridLength.GetLength(1); z++) {
				Transform gameObject = transform;
				Transform instance = (Transform) Instantiate(GridPrefab, new Vector3(x, 0, z), Quaternion.identity);
				instance.parent = gameObject;
			}
		}
	}
	
	/* Toggles visibility of grid and its child objects on/off
	 * */
	private void ToggleVisibility() {
		// Get Renderer componenets of all children
		Renderer[] renderers = GetComponentsInChildren<Renderer>();
		foreach(Renderer renderer in renderers) {
			renderer.enabled = !renderer.enabled;		// Set all renderers to opposite bool
		}
	}
}
