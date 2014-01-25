using UnityEngine;
using System.Collections;

public class Tile {

	public Point point;
	int gScore;
	int hScore;
	int fScore;
	public int cost;



	public Tile(int a, int b, int c)
	{
		point = new Point(a, b);
		cost = c;
	}
}

//maybe remove
public class Point{
	public int x;
	public int y;

	public Point(int a, int b){
		x = a;
		y = b;
	}	
}
