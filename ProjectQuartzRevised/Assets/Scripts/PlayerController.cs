using UnityEngine;
using System.Collections;
using Pathfinding;

public class PlayerController : MonoBehaviour {
	
	public float speed = 100;					//The AI's speed per second
	public Path path;							//The calculated path
	public float nextWaypointDistance = 3;		//The max distance from the AI to a waypoint for it to continue to the next waypoint
    
	private Vector3 targetPosition;				//The point to move to
    private Seeker seeker;
    private CharacterController controller;
	private int currentWaypoint = 0;			//The waypoint we are currently moving towards
 
    public void Start () {
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
        targetPosition = transform.position;
    }
    
    public void OnPathComplete (Path p) {
        Debug.Log ("Yey, we got a path back. Did it have an error? "+p.error);
        if (!p.error) {
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
        }
    }
 
    public void Update () {
		if(Input.GetMouseButtonDown(1)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			var ray_ground = Camera.main.ScreenPointToRay(Input.mousePosition);
			var ground_Layer = 1 << 8;
			RaycastHit hit_ground;
			if(Physics.Raycast(ray_ground, out hit_ground, Mathf.Infinity, ground_Layer)) {
				targetPosition = hit_ground.point;
				seeker.StartPath (transform.position,targetPosition, OnPathComplete);
			}
		}
        if (path == null) {
            //We have no path to move after yet
            return;
        }
        
        if (currentWaypoint >= path.vectorPath.Count) {
            Debug.Log ("End Of Path Reached");
            return;
        }
        
        //Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
        controller.SimpleMove (dir);
        
        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
            currentWaypoint++;
            return;
        }
    }
} 