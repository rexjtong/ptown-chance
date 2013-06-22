using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* This class is used as nodes for a 2D array. It stores information about
 * that node in the map.
 * */
public class MapNode {
	
	Point position;
	bool Visible = false;			// can node be seen
	bool Traversable = true;		// can node be moved on
	bool Buildable = true;			// can node be builded on
	Transform TerrainPrefab;		// prefab used for the node
	
	private const int maxConnection = 8;
	List<Node> Connections;
	List<Edge> Edges;
	
	Node parent;
	
	int fScore = 0;
	int gScore = 2000000;
	int[] kScore = {2000000, 2000000};
	int rhsScore = 2000000;
	
	/* Single argument constructor sets prefab used for node.
	 * Traversable, buildable, but not visible
	 * */
	public MapNode(Transform TerrainPrefab) {
		this.TerrainPrefab = TerrainPrefab;
	}
	
	public MapNode(Transform TerrainPrefab, int x, int y, bool pass) {
		Traversable = pass;
		position = new Point(x, y);
	}
	
	//*************************************************************//
	// Mutator methods
	public void SetVisible(bool Value) {
		Visible = Value;
	}
	
	public void SetTraversable(bool Value) {
		Traversable = Value;
	}
	
	public void SetBuildable(bool Value) {
		Buildable = Value;
	}
	
	public void SetTerrainNode(Transform prefab) {
		this.TerrainPrefab = prefab;
	}
	
	//*********************************************************//
	// Accessor methods
	public bool IsBuildable() {
		return Buildable;
	}
	
	public bool IsTraversable() {
		return Traversable;
	}
	
	public bool IsVisible() {
		return Visible;
	}
	
	public Transform GetTerrain() {
		return TerrainPrefab;
	}
	
	//***************************************************************//
	// Methods for pathfinding
	public boolean AddConnection(MapNode N, int Length) {
		// Check if connection already exists
		if(connectionExists(n))
			return false;
		// Checks for empty spots, and fills them in if present
		for (int i = 0; i < connections.size(); i++){
			if(connections.get(i) == null){
				connections.set(i, n);
				edges.set(i, new Edge(this, n, length));
				return true;
			}
		}

		// If there were no empty spots and the array is at max size, cannot add any more
		if (connections.size() >= maxConnections){
			return false;
		}
		// Expand array size to include new element, if it is not yet max size
		connections.add(n);
		edges.add(new Edge(this, n, length));
		return true;
	}
}