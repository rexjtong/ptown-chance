using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Grid {

	protected Node[,] GridArray;
	protected int xLength;
	protected int yLength;
	protected Node Pos;
	
	private const int Cardinal = 10;
	private const int Diagonal = 14;
	
	public Grid ( int x, int y) {
		xLength = x;
		yLength = y;
		GridArray = new Node[xLength,yLength];
	}
	
	public int GetX() {
		return xLength;
	}
	
	public int GetY() {
		return yLength;
	}
	
	public Node GetPos() {
		return Pos;
	}
	
	public void SetPos(Node s) {
		Pos = s;
	}
	
	public Node GetNode(Point c) {
		int TempX = (int) c.GetX();
		int TempY = (int) c.GetY();
		if(TempX >= 0 && TempX < xLength && TempY >= 0 && TempY < yLength) {
			return GridArray[TempX, TempY];
		}
		return null;
	}
	
	public Node GetNode(int x, int y) {
		if( x >= 0 && x < xLength && y >= 0 && y < yLength) {
			return GridArray[x, y];
		}
		return null;
	}
	
	public bool HasNode(Point c) {
		return (GetNode(c) != null);
	}
	
	public Node[] GetNeighbors(Node n) {
		return n.GetConnections();
	}
	
	public int GetEdgeLength(Node a, Node b) {
		if(a == null || b == null)
			return 0;
		foreach(Edge e in a.GetEdges()) {
			if(e == null)
				continue;
			if(e.GetEnd(a).Equals(b))
				return e.GetLength();
		}
		
		return 0;
	}
	
	public bool LinkNodes(Node a, Node b, int Length) {
		if(a.ConnectionExists(b) || b.ConnectionExists(a) || a == null || b == null)
			return false;
		else {
			a.AddConnection(b, Length);
			b.AddConnection(a, Length);
			return true;
		}
	}
	
	/*
	public Node[] GetVision(Node n, int Sight) {
		int x = (int) n.GetPosition().GetX();
		int y = (int) n.GetPosition().GetY();
		List<Node> Visible = new List<Node>();
		
		for(int AddX = -1 * Sight; AddX <= Sight; AddX++) {
			for(int AddY = -1 * Sight; AddY <= Sight; AddY++) {
				Node Temp = GetNode(x + AddX, y + AddY);
				if(Temp != null && (Temp.GetDistance(n) <= Sight)) {
					Visible.Add(Temp);
				}
			}
		}
		
		Node[] Update = Visible.ToArray();
		
		Visible.Clear();
		
		foreach(Node i in Update) {
			if(i.GetVisibility())
		}
	}
	*/
	
	public Node[] GetAdjacent(Node n) {
		int x = (int) n.GetPosition().GetX();
		int y = (int) n.GetPosition().GetY();
		List<Node> Neighbors = new List<Node>();
		
		for(int AddX = -1; AddX <= 1; AddX++) {
			for(int AddY = -1; AddY <= 1; AddY++) {
				Node Temp = GetNode(x + AddX, y + AddY);
				
				if(Temp != null && (AddX != 0 || AddY != 0))
					Neighbors.Add(Temp);
			}
		}
		
		return Neighbors.ToArray();
	}
	
	public void ResetGrid() {
		for(int x = 0; x < xLength; x++) {
			for(int y = 0; y < yLength; y++) {
				Node Current = GridArray[x, y];
				Current.SetGScore(2000000);
				Current.SetRhsScore(2000000);
				Current.SetFScore(0);
				Current.SetKScore(2000000, 2000000);
				Current.SetParent(Current);
			}
		}
	}
	
	public void CreateStandard() {
		for(int x = 0; x < xLength; x++) {
			for(int y = 0; y < yLength; y++) {
				GridArray[x, y] = new MapNode(x, y, true);
			}
		}
		for(int x = 0; x < xLength; x++) {
			for(int y = 0; y < yLength; y++) {
				Node Current = GetNode(x, y);
				Node[] Neighbors = GetAdjacent(Current);
				foreach(Node n in Neighbors) {
					if(n.GetPosition().GetX() == x || n.GetPosition().GetY() == y)
						LinkNodes(Current, n, Cardinal);
					else
						LinkNodes(Current, n, Diagonal);
				}
			}
		}
	}
	
	public void CreateRandom() {
		double LinkProb = 0.3;
		for(int x = 0; x < xLength; x++) {
			for(int y = 0; y < yLength; y++) {
				GridArray[x, y] = new MapNode(x, y, true);
			}
		}
		
		for(int x = 0; x < xLength; x++) {
			for(int y = 0; y < yLength; y++) {
				Node Current = GridArray[x, y];
				Node[] Neighbors = GetAdjacent(Current);
				System.Random random = new System.Random();
				foreach(Node n in Neighbors) {
					if(random.NextDouble() < LinkProb) {
						if(n.GetPosition().GetX() == Current.GetPosition().GetX()
							|| n.GetPosition().GetY() == Current.GetPosition().GetY())
							LinkNodes(Current, n, Cardinal);
						else
							LinkNodes(Current, n, Diagonal);
					}
				}
			}
		}
	}
}
