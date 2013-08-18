using UnityEngine;
using System.Collections;

public class Quarry : MonoBehaviour {
	
	public string oreType;
	
	private bool redOre;
	private int redOreStored = 0;
	
	// Use this for initialization
	void Start () {
		if(oreType == "RedOreVein") {
			redOre = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(redOre) {
			InvokeRepeating("IncreaseRedOre", 0.5f, 2.0f);
		}
	}
	
	void IncreaseRedOre() {
		redOreStored += 5;
	}
	
	void OnGUI() {
		GUI.Box(new Rect(500,500,50,50), redOreStored.ToString());
	}
}
