using UnityEngine;
using System.Collections;

public class TowerPlacement : MonoBehaviour {
	
	public Material Green;
	public Material Red;
	
	private bool Buildable = true;
	
	void Awake () {
		Messenger.AddListener("place tower", PlaceTower);
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!Buildable) {
			transform.renderer.material = Red;
		}
		else
			transform.renderer.material = Green;
	}
	
	void PlaceTower() {
	}
	
	void OnTriggerStay(Collider other) {
		if(other.CompareTag("Unit") || other.CompareTag("Player") || other.CompareTag("Terrain")){
			Buildable = false;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if(other.CompareTag("Unit") || other.CompareTag("Player") || other.CompareTag("Terrain")){
			Buildable = true;
		}
	}
}
