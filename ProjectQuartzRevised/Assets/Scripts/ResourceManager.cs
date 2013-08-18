using UnityEngine;
using System.Collections;

public class ResourceManager : MonoBehaviour {
	
	private int ore = 0;
	private int gold = 0;
	
	void OnGUI() {
		GUI.Box(new Rect(775, 20, 100, 20), "Gold: " + gold);
		GUI.Box(new Rect(900, 20, 100, 20), "Ore: " + ore);
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void ChangeGold(int change) {
		gold += change;
	}
	
	void ChangeOre(int change) {
		ore += change;
	}
}
