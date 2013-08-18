using UnityEngine;
using System.Collections;

public class BuildingCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Unit") || other.CompareTag("Player") || other.CompareTag("OreVein") || other.CompareTag("Building")){
			SendMessageUpwards("changeCount", 1);
		}
	}
	
	void OnTriggerExit(Collider other) {
		if(other.CompareTag("Unit") || other.CompareTag("Player") || other.CompareTag("OreVein") || other.CompareTag("Building")){
			SendMessageUpwards("changeCount", -1);
		}
	}
	
}
