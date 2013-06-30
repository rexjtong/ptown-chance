using UnityEngine;
using System.Collections;

public class NodeComparator {

	public int Compare(MapNode One, MapNode Two) {
		if(One.GetFScore() < Two.GetFScore ())
			return -1;
		else if(One.GetFScore() > Two.GetFScore())
			return 1;
		else return 0;
	}
}
