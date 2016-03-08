using UnityEngine;
using System.Collections;
using System;


public class BoardMap {

	private bool[,] boardMap = new bool[8, 8];


	public BoardMap(bool fill){
		for (int i = 0; i < boardMap.GetLength (0); i++) {
			for (int j = 0; j < boardMap.GetLength (1); j++) {
				SetTile (i, j, fill);
			}
		}
	}
	/// <summary> Get the the tile value in the x, y positions </summary>
	public bool GetTile(int x, int y){
		return boardMap [x, y];
	}
	/// <summary> Get the the tile value in the given Position </summary>
	protected bool GetTile(Position pos){
		return GetTile (pos.GetX (), pos.GetY ());
	}

	/// <summary> Set the the tile value in the x, y positions </summary>
	void SetTile(int x, int y, bool value){
		boardMap [x, y] = value;
	}
	/// <summary> Set the the tile value in given Positions </summary>
	void SetTile(Position pos, bool value){
		SetTile (pos.GetX (), pos.GetY (), value);
	}
	/// <summary> Add the given BoardMap</summary>
	public void Add(BoardMap map){
		for (int i = 0; i < boardMap.GetLength (0); i++) {
			for (int j = 0; j < boardMap.GetLength (1); j++) {
				bool added = GetTile (i, j) || map.GetTile (i, j);
				SetTile (i, j, added);
			}
		}
	}
	/// <summary> Remove the given BoardMap</summary>
	public void Remove(BoardMap map){
		for (int i = 0; i < boardMap.GetLength (0); i++) {
			for (int j = 0; j < boardMap.GetLength (1); j++) {
				bool removed = GetTile(i,j) ? !(GetTile(i,j) && map.GetTile(i,j) ) :false;
				SetTile (i, j, removed);
			}
		}
	}
	/// <summary> Intersects the given BoardMap</summary>
	public void Intersect(BoardMap map){
		for (int i = 0; i < boardMap.GetLength (0); i++) {
			for (int j = 0; j < boardMap.GetLength (1); j++) {
				bool intersected = GetTile (i, j) && map.GetTile (i, j);
				SetTile (i, j, intersected);
			}
		}
	}

	public void Invert(){
		for (int i = 0; i < boardMap.GetLength (0); i++) {
			for (int j = 0; j < boardMap.GetLength (1); j++) {
				SetTile (i, j, !GetTile (i, j));
			}
		}
	}

	#region MovementPAtterns
	public static BoardMap Cross(int x, int y){
		BoardMap cross = new BoardMap (false);

		for (int i = 0; i < 8; i++) {
			if(y != i)
				cross.SetTile (x, i, true);
		}
		for (int i = 0; i < 8; i++){
			if(x != i) 
				cross.SetTile (i, y, true);
		}

		return cross;
		
	}
	public static BoardMap DiagonalCross(int x, int y){ //TODO: check the weird (5,4) behaviour
		BoardMap dCross = new BoardMap (false);
		Position head = new Position (x, y);

		while (head.GetX () > 0 && head.GetY () > 0) {
			head.Move (Directions.NW);
			dCross.SetTile (head, true);
		}
		head.Set (x, y);

		while (head.GetX () < 7 && head.GetY () > 0) {
			head.Move (Directions.NE);
			dCross.SetTile (head, true);
		}
		head.Set (x, y);

		while (head.GetX () > 0 && head.GetY () < 7) {
			head.Move (Directions.SW);
			dCross.SetTile (head, true);
		}
		head.Set (x, y);

		while (head.GetX () < 7 && head.GetY () < 7) {
			head.Move (Directions.SE);
			dCross.SetTile (head, true);
		}

		return dCross;
	}
	public static BoardMap ShortL(int x, int y){
		BoardMap boardmap = new BoardMap (false);

		Position head = new Position (x + 1, y + 2);
		if (head.isValid)
			boardmap.SetTile (head, true);
		 head = new Position (x + 2, y + 1);
		if (head.isValid)
			boardmap.SetTile (head, true);
		 head = new Position (x + 2, y -1);
		if (head.isValid)
			boardmap.SetTile (head, true);
		 head = new Position (x +1, y -2);
		if (head.isValid)
			boardmap.SetTile (head, true);
		 head = new Position (x -1, y -2);
		if (head.isValid)
			boardmap.SetTile (head, true);
		 head = new Position (x -2, y -1);
		if (head.isValid)
			boardmap.SetTile (head, true);
		 head = new Position (x-1, y +2);
		if (head.isValid)
			boardmap.SetTile (head, true);
		 head = new Position (x-2, y + 1);
		if (head.isValid)
			boardmap.SetTile (head, true);
		
		return boardmap;
	
	}
	public static BoardMap ComposedCross(int x, int y){
		BoardMap map = Cross (x, y);
		map.Add (DiagonalCross (x, y));
		return map;
	}
	public static BoardMap ShortSquare(int x, int y){
		BoardMap map = new BoardMap (false);
		var directions = Enum.GetValues (typeof(Directions));
		foreach (Directions d in directions) {
			Position pos = new Position (x, y);
			pos.Move (d);
			if (pos.isValid)
				map.SetTile(pos, true);
		}
		return map;
	}
	public static BoardMap Ahead(int x, int y, Directions direction){

		BoardMap map = new BoardMap (false);
		Position pos = new Position (x, y);
		if (direction.ToString ().Length == 1) {
			pos.Move (direction);
			if (pos.isValid)
				map.SetTile (pos, true);
		} 
		else 
			Debug.LogError ("A straight direction is required (N, S, E , W");
	
		return map;
	}
	public static BoardMap DiagonalAhead(int x, int y, Directions direction){
		BoardMap map = new BoardMap (false);
		Position pos1 = new Position (x, y);
		Position pos2 = new Position (x, y);
		switch (direction) {
		case Directions.N:
			pos1.Move (Directions.NE);
			pos2.Move (Directions.NW);
			break;
		case Directions.S:
			pos1.Move (Directions.SE);
			pos2.Move (Directions.SW);
			break;;
		case Directions.E:
			pos1.Move (Directions.NE);
			pos2.Move (Directions.SE);
			break;
		case Directions.W:
			pos1.Move (Directions.NW);
			pos2.Move (Directions.SW);
			break;
		default:
			Debug.LogError ("A straight direction is required (N, S, E , W");
			break;
		}
		if (pos1.isValid)
			map.SetTile (pos1, true);
		if (pos2.isValid)
			map.SetTile (pos2, true);

		return map;
	}
	#endregion

	public override string ToString(){
		string result = "";
		for (int j = 0; j < 8; j++) 
			for (int i = 0; i < 8; i++) {
				result += GetTile(i,j)?"X ":"  ";
				if (i == 7)
					result += "\n";
			}
		return result;
	}




}
