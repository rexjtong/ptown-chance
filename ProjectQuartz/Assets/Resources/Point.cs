using UnityEngine;
using System.Collections;

public class Point {

	private float x;
	private float y;
	
	public Point() {
		x = 0;
		y = 0;
	}
	
	public Point(float x, float y) {
		this.x = x;
		this.y = y;
	}
	
	public bool Equals(Point p) {
		if(p == null)
			return false;
		else if(p.x == this.x && p.y == this.y) {
			return true;
		}
		else
			return false;
	}
	
	public float GetX() {
		return x;
	}
	
	public float GetY() {
		return y;
	}
}
