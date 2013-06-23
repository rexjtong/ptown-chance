using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* This class is used as nodes for a 2D array. It stores information about
 * that node in the map.
 * */
public class MapNode {
	
	bool Visible = false;			// can node be seen
	bool Traversable = true;		// can node be moved on
	bool Buildable = true;			// can node be builded on
	Transform TerrainPrefab;		// prefab used for the node
	
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
}