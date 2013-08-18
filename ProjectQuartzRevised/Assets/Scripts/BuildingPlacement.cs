using UnityEngine;
using System.Collections;

public class BuildingPlacement : MonoBehaviour {
	
	public Material Green;
	public Material Red;
	
	private int collisionCount;
	private bool Buildable = true;
	private Renderer renderer;
	
	void Awake () {
		Messenger.AddListener("place tower", PlaceTower);
	}
	
	// Use this for initialization
	void Start () {
		Renderer[] rendererArr = transform.GetComponentsInChildren<Renderer>();
		renderer = rendererArr[0];
	}
	
	// Update is called once per frame
	void Update () {
		if(collisionCount == 0) {
			Buildable = true;
		}
		else
			Buildable = false;
		if(!Buildable) {
			renderer.material = Red;
		}
		else
			renderer.material = Green;
	}
	
	void PlaceTower() {
	}
	
	public bool isBuildable() {
		return Buildable;
	}
	
	void changeCount(int change) {
		collisionCount += change;
	}
	
	void OnDestroy() {
		Transform model = transform.Find("Model");
		Transform towerCollider = transform.Find("Tower Collider");
		Destroy(model);
		Destroy(towerCollider);
	}
}
