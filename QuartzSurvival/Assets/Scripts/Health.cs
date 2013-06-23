using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public float MaxHealth;
	public float CurrentHealth;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void OnDamage(float Damage) {
		CurrentHealth -= Damage;
	}
}
