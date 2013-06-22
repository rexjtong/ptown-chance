using System.Collections;

/*
 * Edge objects for connecting adjacent nodes in the grid
 */
public class Edge {
	private int Length;
	private MapNode Begin;
	private MapNode End;

	public Edge(MapNode First, MapNode Last){
		Begin = First;
		End = Last;
		Length = 10;
	}

	public Edge(MapNode First, MapNode Last, int S){
		Begin = First;
		End = Last;
		Length = S;
	}

	// Checks equality by looking at endpoints and length. Order of endpoints matters
	public bool Equals(Edge e){
		return (Begin.Equals(E.GetBegin()) && End.Equals(E.GetEnd()) && Length == E.GetLength());
	}

	public int GetLength(){
		return length;
	}

	// Takes in one end of the edge and returns the other end.
	// Returns null if the passed-in node is not part of this edge
	public MapNode GetEnd(Node A){
		if(A.Equals(Begin))
			return End;
		else if(A.Equals(End))
			return Begin;
		else
			return null;
	}

	public MapNode GetBegin(){
		return Begin;
	}

	public MapNode GetEnd(){
		return End;
	}
}