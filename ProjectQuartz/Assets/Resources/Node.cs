using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Node {

	protected Point Position;
	protected bool Passable;
	protected bool Visible;
	
	protected List<Node> Connections;
	protected List<Edge> Edges;
	
	protected Node Parent = new MapNode();
	
	protected int fScore = 0;
	protected int gScore = 2000000;
	protected int[] kScore = {200000, 2000000};
	protected int rhsScore = 2000000;
	
	public Node() {
		Parent = this;
	}
	
	public Node(int x, int y, bool Pass, int MaxConnections){
		Position = new Point(x, y);
		Passable = Pass;
		Connections = new List<Node>();
		Edges = new List<Edge>();
		Visible = true;
		Parent = this;
	}
	
	public abstract bool AddConnection(Node n, int Length);
	
	public abstract bool ConnectionExists(Node n);
	
	public abstract Edge DeleteConnection(Node n);
	
	Node[] GetAllConnections() {
		return Connections.ToArray();
	}
	
	Edge[] GetAllEdges() {
		return Edges.ToArray();
	}
	
	public Node[] GetConnections() {
		List<Node> Result = new List<Node>();
		for(int i = 0; i < Connections.Count; i++) {
			Node n = Connections[i];
			if(n.IsPassable() && n != null) {
				Result.Add(n);
			}
		}
		
		return Result.ToArray();
	}
	
	public Edge[] GetEdges() {
		List<Edge> Result = new List<Edge>();
		Edge[] EdgeArray = Edges.ToArray();
		foreach(Edge e in EdgeArray) {
			if(e.GetEnd(this).IsPassable() && e != null) {
				Result.Add (e);
			}
		}
		
		return Result.ToArray();
	}
	
	public bool Equals(Node n) {
		if(n == null)
			return false;
		else
			return (Position.Equals(n.GetPosition())) && (Passable == n.IsPassable());
	}
	
	public double GetDistance(Node n) {
		return (double) Mathf.Sqrt(Mathf.Pow(Position.GetX() - n.GetPosition().GetX(), 2) + Mathf.Pow(Position.GetY () - n.GetPosition().GetY(), 2));
	}
	
	public void SetPosition(int x, int y) {
		Position = new Point(x, y);
	}
	
	public Point GetPosition() {
		return Position;
	}
	
	public void SetPassable(bool Value) {
		Passable = Value;
	}
	
	public bool IsPassable() {
		return Passable;
	}
	
	public int GetFScore() {
		return fScore;
	}
	
	public void SetFScore(int Score) {
		fScore = Score;
	}
	
	public int GetGScore() {
		return gScore;
	}
	
	public void SetGScore(int Score) {
		gScore = Score;
	}
	
	public Node GetParent() {
		return Parent;
	}
	
	public void SetParent(Node n) {
		Parent = n;
	}
	
	public int[] GetKScore() {
		return kScore;
	}
	
	public void SetKScore(int[] Score) {
		kScore = Score;
	}
	
	public void SetKScore(int One, int Two) {
		kScore[0] = One;
		kScore[1] = Two;
	}
	
	public int GetRhsScore() {
		return rhsScore;
	}
	
	public void SetRhsScore(int Score) {
		rhsScore = Score;
	}
	
	bool GetVisibility() {
		return Visible;
	}
	
	public void SetVisibility(bool Value) {
		Visible = Value;
	}
}
