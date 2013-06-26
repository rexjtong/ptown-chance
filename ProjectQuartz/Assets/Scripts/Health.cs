using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public float MaxHealth;
	private float CurrentHealth;
	
	// Use this for initialization
	void Start () {
		CurrentHealth = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if(CurrentHealth >= MaxHealth) {
			CurrentHealth = MaxHealth;
		}
		if(CurrentHealth <= 0) {
			GameObject Parent = transform.parent.gameObject;
			Destroy (Parent);
		}
	}
	
	private void OnHeal(float Heal) {
		CurrentHealth += Heal;
	}
	
	private void OnDamage(float Damage) {
		CurrentHealth -= Damage;
	}
}
