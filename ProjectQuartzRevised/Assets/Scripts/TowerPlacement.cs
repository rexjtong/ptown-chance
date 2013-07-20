using UnityEngine;
using System.Collections;

public class TowerPlacement : MonoBehaviour {
	
	public Material Green;
	public Material Red;
	
	private int collisionCount;
	private bool Buildable = true;
	
	void Awake () {
		Messenger.AddListener("place tower", PlaceTower);
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(collisionCount == 0) {
			Buildable = true;
		}
		else
			Buildable = false;
		if(!Buildable) {
			transform.renderer.material = Red;
		}
		else
			transform.renderer.material = Green;
	}
	
	void PlaceTower() {
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Unit") || other.CompareTag("Player") || other.CompareTag("Terrain") || other.CompareTag("Building")){
			collisionCount++;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if(other.CompareTag("Unit") || other.CompareTag("Player") || other.CompareTag("Terrain") || other.CompareTag("Building")){
			collisionCount--;
		}
	}
	
	public bool isBuildable() {
		return Buildable;
	}
}
