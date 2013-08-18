using UnityEngine;
using System.Collections;
using Pathfinding;
using System;

public class PlayerController : MonoBehaviour {
	
	public float speed = 100;					//The AI's speed per second
	public Path path;							//The calculated path
	public float nextWaypointDistance = .5f;	//The max distance from the AI to a waypoint for it to continue to the next waypoint
    
	private Vector3 targetPosition;				//The point to move to
    private Seeker seeker;
    private CharacterController controller;
	private int currentWaypoint = 0;			//The waypoint we are currently moving towards
	private bool nowPlacing = false;
	private bool nowBuilding = false;
	private bool cancelFirstClick = false;
 
	void Awake () {
		Messenger.AddListener<Vector3>("move to building", MoveToPlacement);
		Messenger.AddListener("building mode", SetBuildingMode);
		Messenger.AddListener("confirm building", ConfirmBuilding);
	}
	
    public void Start () {
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
        targetPosition = transform.position;
		seeker.StartPath (transform.position, targetPosition, OnPathComplete);
    }
    
    public void OnPathComplete (Path p) {
        Debug.Log ("Yey, we got a path back. Did it have an error? "+p.error);
        if (!p.error) {
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
        }
    }
 
    public void FixedUpdate () {
		if(!nowBuilding) {
			if(Input.GetMouseButtonDown(1) && !cancelFirstClick) {
				path = null;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				var ray_ground = Camera.main.ScreenPointToRay(Input.mousePosition);
				var ground_Layer = 1 << 8;
				RaycastHit hit_ground;
				if(Physics.Raycast(ray_ground, out hit_ground, Mathf.Infinity, ground_Layer)) {
					targetPosition = hit_ground.point;
					seeker.StartPath (transform.position, targetPosition, OnPathComplete);
				}
			}
			if(cancelFirstClick) {
				path = null;
				cancelFirstClick = false;
			}
		}
		
		if(nowBuilding) {
			if(Input.GetMouseButtonDown(1)) {
				nowBuilding = false;
				nowPlacing = false;
				Messenger.Broadcast("cancel building mode");
			}
		}
		
		if(nowPlacing) {
			if(Math.Abs(transform.position.x - targetPosition.x) <= 10 && Math.Abs(transform.position.z - targetPosition.z) <= 10) {
				currentWaypoint = path.vectorPath.Count;
				Messenger.Broadcast("place building");
			}
		}
		
        if (path == null) {
            return;
        }
        
        if (currentWaypoint >= path.vectorPath.Count) {
            Debug.Log ("End Of Path Reached");
            return;
        }
        
        //Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized * speed * Time.fixedDeltaTime;
        controller.SimpleMove (dir);
        
        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance (transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
            currentWaypoint++;
            return;
        }
    }
	
	void MoveToPlacement(Vector3 Location) {
		nowPlacing = true;
		targetPosition = Location;
		seeker.StartPath (transform.position, targetPosition, OnPathComplete);
	}
	
	void SetBuildingMode() {
		nowBuilding = true;
		cancelFirstClick = true;
	}
	
	void ConfirmBuilding() {
		nowPlacing = false;
		nowBuilding = false;
		cancelFirstClick = false;
		// nowBuilding = false;
	}
} 