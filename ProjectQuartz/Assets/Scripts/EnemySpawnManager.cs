using UnityEngine;
using System.Collections;

/* This Script is attached to a spawner and will spawn generic enemies at the spawner location
   every (SPAWN_PERIOD +- randPeriod) 
   
   randPeriod is determined at Start() and is re-randomized every time Spawn() is called to spawn an enemy
   at the location of the spawner. 



*/

public class EnemySpawnManager : MonoBehaviour {
	
	// main time holder
	private float timer = 0.0f;
	
	// Base period between enemy spawns
	public float SPAWN_PERIOD = 5.0f;
	// Min additional error added to spawn period
	public float MIN_RAND_PERIOD = -5.0f;	
	// Max additional error added to sapwn period
	public float MAX_RAND_PERIOD = 5.0f;

	private float randPeriod;							// Additional error added onto spawn period
														// is initialized on Start() and is then randomized
	                                                    // again every time spawn() is called.
	
	private bool isSpawning = false;
	
	public Transform enemy;
	
	// Use this for initialization
	void Start () {
	    randPeriod = Random.Range(MIN_RAND_PERIOD, MAX_RAND_PERIOD);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!isSpawning)
	    	timer += Time.deltaTime;
		
		if ((timer >= SPAWN_PERIOD + randPeriod) && !isSpawning)
			Spawn();       
	}
	
	// instantiates an enemy and resets the clock and isSpawning
	void Spawn()
	{
		isSpawning = true;
		
		Instantiate(enemy, transform.position, Quaternion.identity);
		
		timer = 0.0f;
		randPeriod = Random.Range(MIN_RAND_PERIOD, MAX_RAND_PERIOD);
		
		isSpawning = false;
	}
}
