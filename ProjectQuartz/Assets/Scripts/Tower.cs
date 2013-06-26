using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {
	
	public static Object BulletPrefab = Resources.Load ("Bullet");
	public float BuildingTime;
	public float ReloadTime;
	public float ShotHeight;
	
	private GameObject NewBullet;
	private Transform Target;
	private float NextFireTime;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Target) {
			if(NextFireTime <= Time.time)
			{
				FireBullet();
			}
		}
	}
	
	void FireBullet() {
		NewBullet = Instantiate(BulletPrefab, new Vector3(transform.position.x, transform.position.y + ShotHeight, transform.position.z), Quaternion.identity) as GameObject;
		NextFireTime = Time.time + ReloadTime;
		Bullet BulletScript = NewBullet.GetComponent<Bullet>();
		BulletScript.SetTarget(Target);
	}
	
	void OnTriggerEnter(Collider Other) {
		
		if(Other.gameObject.tag == "Enemy") {
			Target = Other.gameObject.transform;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if(other.gameObject.transform == Target) {
			Target = null;
		}
	}
}
