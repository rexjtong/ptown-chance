using UnityEngine;
using System.Collections;

public class Temporary : MonoBehaviour {
	
	public float LifeTime;
	
	// Update is called once per frame
	void Update () {
		LifeTime -= Time.deltaTime;
		
		if(LifeTime <= 0) {
			Destroy (gameObject);
		}
	}
}
