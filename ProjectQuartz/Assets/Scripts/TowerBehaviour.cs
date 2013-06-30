using UnityEngine;
using System.Collections;

public class TowerBehaviour : MonoBehaviour {
	
	public static Object BulletPrefab = Resources.Load ("Bullet");
	public float BuildingSpeed;
	public float ReloadTime;
	public float ShotHeight;
	
	private bool FinishedBuilding;
	private Vector3 StartingPosition;
	private Vector3 CompletedPosition;
	private float BuildingHeight;
	private GameObject NewBullet;
	private Transform Target;
	private float NextFireTime;
	
	// Use this for initialization
	void Start () {
		Renderer renderer = gameObject.GetComponent<Renderer>();
		BuildingHeight = renderer.bounds.max.y - renderer.bounds.min.y;
		StartingPosition = new Vector3(transform.position.x, 0 - BuildingHeight/2, transform.position.z);
		CompletedPosition = new Vector3(transform.position.x, BuildingHeight/2, transform.position.z);
		transform.position = StartingPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if(!FinishedBuilding) {
			transform.position = Vector3.MoveTowards(transform.position, CompletedPosition, Time.deltaTime * BuildingSpeed);
			if(transform.position == CompletedPosition) {
				FinishedBuilding = true;
			}
		}
		if(FinishedBuilding) {
			if(Target) {
				if(NextFireTime <= Time.time)
				{
					FireBullet();
				}
			}
		}
	}
	
	void FireBullet() {
		NewBullet = Instantiate(BulletPrefab, new Vector3(transform.position.x, ShotHeight, transform.position.z), Quaternion.identity) as GameObject;
		NextFireTime = Time.time + ReloadTime;
		Bullet BulletScript = NewBullet.GetComponent<Bullet>();
		BulletScript.SetTarget(Target);
	}
	
	void OnTriggerEnter(Collider Other) {
		
		if(Other.gameObject.tag == "Enemy") {
			Target = Other.gameObject.transform;
		}
	}
	
	void OnTriggerExit(Collider Other) {
		if(Other.gameObject.transform == Target) {
			Target = null;
		}
	}

	void OnTriggerStay(Collider Other) {
		if(Other.gameObject.tag == "Enemy") {
			Target = Other.gameObject.transform;
		}
	}
}
