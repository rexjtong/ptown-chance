using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* This class is used as nodes for a 2D array. It stores information about
 * that node in the map.
 * */
public class MapNode {
	
	bool Visible = false;			// can node be seen
	bool Passable = true;		// can node be moved on
	bool Buildable = true;			// can node be builded on
	Transform TerrainPrefab;		// prefab used for the node
	
	private static int MaxConnections = 8;
	
	public MapNode() {
	}
	
	public MapNode(int x, int y, bool Pass) {
	}
	
	/* Single argument constructor sets prefab used for node.
	 * Traversable, buildable, but not visible
	 * */
	public MapNode(Transform TerrainPrefab) {
		this.TerrainPrefab = TerrainPrefab;
	}
	
	//*************************************************************//
	// Mutator methods
	public void SetVisible(bool Value) {
		Visible = Value;
	}
	
	public void SetPassable(bool Value) {
		Passable = Value;
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
	
	public bool IsPassable() {
		return Passable;
	}
	
	public bool IsVisible() {
		return Visible;
	}
	
	public Transform GetTerrain() {
		return TerrainPrefab;
	}
	
	/*
	public override bool AddConnection(Node n, int Length) {
		if(ConnectionExists(n)) {
			return false;
		}
		for(int i = 0; i < Connections.Count; i++) {
			if(Connections[i] == null) {
				Connections[i] = n;
				Edges[i] = new Edge(this, n, Length);
				return true;
			}
		}
		
		if(Connections.Count >= MaxConnections) {
			return false;
		}
		
		Connections.Add(n);
		Edges.Add (new Edge(this, n, Length));
		return true;
	}
	
	// Possibly written wrong
	public override bool ConnectionExists(Node n) {
		foreach(Node t in Connections) {
			if(t != null && t.Equals (n)) {
				return true;
			}
		}
		return false;
	}
	
	public override Edge DeleteConnection(Node n) {
		for(int i = 0; i < Connections.Count; i++) {
			if(Connections[i].Equals(n)) {
				Connections.RemoveAt(i);
				Edge e = Edges[i];
				Edges.RemoveAt(i);
				return e;
			}
		}
		
		return null;
	}
	*/
}