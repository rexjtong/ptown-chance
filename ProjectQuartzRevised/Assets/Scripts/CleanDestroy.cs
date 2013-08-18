using UnityEngine;
using System.Collections;

public class CleanDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnDestroy() {
		Transform[] child = GetComponentsInChildren<Transform>();
		
		foreach(Transform childObject in child) {
			Destroy(childObject.gameObject);
		}
		Destroy(gameObject);
	}
}
