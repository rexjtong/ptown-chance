using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public float MaxHealth;
	public Transform Unit;
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
			if(gameObject.tag == "Enemy") {
				Messenger.Broadcast<GameObject>("enemy died", transform.gameObject);
			}
			else if(gameObject.tag == "FriendlyBuilding" || gameObject.tag == "Tower"){
				Messenger.Broadcast<Vector3>("building died", transform.position);
			}
			Destroy (gameObject);
		}
	}
	
	private void OnHeal(float Heal) {
		CurrentHealth += Heal;
	}
	
	private void OnDamage(float Damage) {
		CurrentHealth -= Damage;
	}
}
