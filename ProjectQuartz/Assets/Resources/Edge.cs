using UnityEngine;
using System.Collections;

public class Edge {

	private int Length;
	private Node Begin;
	private Node End;
	
	public Edge(Node First, Node Last) {
		Begin = First;
		End = Last;
		Length = 10;
	}
	
	public Edge(Node First, Node Last, int s) {
		Begin = First;
		End = Last;
		Length = s;
	}
	
	public int GetLength() {
		return Length;
	}
	
	public Node GetEnd() {
		return End;
	}
	
	public Node GetBegin() {
		return Begin;
	}
	
	public Node GetEnd(Node a) {
		if(a.Equals(Begin)) {
			return End;
		}
		else if(a.Equals(End)) {
			return Begin;
		}
		else {
			return null;
		}
	}
	
	public bool Equals(Edge e) {
		return (Begin.Equals(e.GetBegin()) && End.Equals(e.GetEnd()) && Length == e.GetLength());
	}
}
