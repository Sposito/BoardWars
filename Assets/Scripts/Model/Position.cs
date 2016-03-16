
using UnityEngine;
using System;
public enum Directions{N,NE,E,SE,S,SW,W,NW}

public class Position  {

	private int x;
	private int y;

	public int X  { get { return x; } }
	public int Y  { get { return y; } }

		

	public static Position zero = new Position (0, 0);

	public Position(int x, int y){
		this.x = x;
		this.y = y;
	}

	public void Set(int x, int y){
		this.x = x;
		this.y = y;
	}
	#region Obsolete
	[Obsolete("Method is deprecated, please use X acessor instead.")]
	public int GetX(){
		return x;
	}
	[Obsolete("Method is deprecated, please use Y acessor instead.")]
	public int GetY(){
		return y;
	}
	#endregion
	public bool isValid{get{
			if (x < 8 && x >= 0 && y < 8 && y >= 0)
				return true;
			else
				return false;
		}
		set{ }
	}

	public Vector3 ToScenePosition(){
		
		Vector3 right = new Vector3 (BoardController.xSpacing, -BoardController.ySpacing);
		Vector3 down = new Vector3 (-BoardController.xSpacing, -BoardController.ySpacing);

		return right * x + down * y;
	}

	public void Move(Directions dir){
		switch (dir) {
		case Directions.N:
			Set (x, y + 1);
			return;
		case Directions.NE:
			Set (x + 1, y + 1);
			return;
		case Directions.E:
			Set (x + 1, y);
			return;
		case Directions.SE:
			Set (x + 1, y - 1);
			return;
		case Directions.S:
			Set (x, y - 1);
			return;
		case Directions.SW:
			Set (x - 1, y - 1);
			return;
		case Directions.W:
			Set (x - 1, y);
			return;
		case Directions.NW:
			Set (x - 1, y + 1);
			return;
		}
	}
	public override string ToString (){
		string letters = "ABCDEFGH";
		return "" + letters [x] + (y+1);
	} 
}