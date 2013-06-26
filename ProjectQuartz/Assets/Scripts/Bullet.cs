using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public float Speed;
	public float Damage;
	
	private Quaternion TargetRotation;
	private Transform Target;
	
	void Awake () {
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Target) {
			transform.LookAt(Target.position);
			transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
		}
		else {
			rigidbody.velocity = transform.forward * Speed;
		}
	}
	
	public void SetTarget(Transform Target) {
		this.Target = Target;
	}
	
	void OnTriggerEnter(Collider Other) {
		if(Other.gameObject.tag == "Enemy") {
			Other.gameObject.SendMessage("OnDamage", Damage);
			Destroy (gameObject);
		}
	}
}
