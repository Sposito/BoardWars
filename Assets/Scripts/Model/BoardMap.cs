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

	public static BoardMap Empty{ get{return new BoardMap (false);}}

	public bool IsEmpty{
		get{
			foreach (bool b in boardMap) {
				if (b)
					return false;
			}
				
			return true;
		}
	}

	/// <summary> Get the the tile value in the x, y positions </summary>
	public bool GetTile(int x, int y){
		return boardMap [x, y];
	}
	/// <summary> Get the the tile value in the given Position </summary>
	protected bool GetTile(Position pos){
		return GetTile (pos.X, pos.Y );
	}

	/// <summary> Set the the tile value in the x, y positions </summary>
	public void SetTile(int x, int y, bool value){
		boardMap [x, y] = value;
	}
	/// <summary> Set the the tile value in given Positions </summary>
	public void SetTile(Position pos, bool value){
		SetTile (pos.X , pos.Y, value);
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

		while (head.X > 0 && head.Y > 0) {
			head.Move (Directions.SW);
			dCross.SetTile (head, true);
		}
		head.Set (x, y);

		while (head.X < 7 && head.Y > 0) {
			head.Move (Directions.SE);
			dCross.SetTile (head, true);
		}
		head.Set (x, y);

		while (head.X > 0 && head.Y < 7) {
			head.Move (Directions.NW);
			dCross.SetTile (head, true);
		}
		head.Set (x, y);

		while (head.X < 7 && head.Y < 7) {
			head.Move (Directions.NE);
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
	public static BoardMap ShortL(Position position){
		return ShortL (position.X, position.Y);
	}

	public static BoardMap ComposedCross(int x, int y){
		BoardMap map = Cross (x, y);
		map.Add (DiagonalCross (x, y));
		return map;
	}

	public static BoardMap SinglePosition(int x, int y){
		BoardMap map = BoardMap.Empty;
		map.SetTile (x, y, true);
		return map;
	}
	public static BoardMap SinglePosition(Position pos){
		return SinglePosition (pos.X, pos.Y);
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
	public static BoardMap ShortSquare(Position position){
		return ShortSquare (position.X, position.Y);
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

	public static BoardMap Ahead(Position position, Player player){
		Directions direction = Directions.N;

		switch (player) {
		case Player.PLAYER1:
			direction = Directions.E;
			break;
		case Player.PLAYER2:
			direction = Directions.S;
			break;
		case Player.PLAYER3:
			direction = Directions.W;
			break;
		case Player.PLAYER4:
			direction = Directions.N;
			break;
		default:
			break;
		}

		return  Ahead (position.X, position.Y, direction);
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
			break;
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
	public static BoardMap DiagonalAhead(Position position, Player player){
		Directions direction = Directions.N;
		switch (player) {
		case Player.PLAYER1:
			direction = Directions.E;
			break;
		case Player.PLAYER2:
			direction = Directions.S;
			break;
		case Player.PLAYER3:
			direction = Directions.W;
			break;
		case Player.PLAYER4:
			direction = Directions.N;
			break;
		default:
			break;
		}

		return DiagonalAhead (position.X, position.Y, direction);
	}
		
	public static BoardMap BlockedLineMovement(int x, int y, BoardMap blockedMap, bool diagonal){
		
		BoardMap map = BoardMap.Empty;
		Directions[]  cross = { Directions.N, Directions.E, Directions.S, Directions.W };
		Directions[] xCross = { Directions.NE, Directions.NW, Directions.SE, Directions.SW };
		Directions[] directions = diagonal ? xCross : cross;
		foreach (Directions d in directions) {
			Position head = new Position(x,y);
			head.Move (d);
			while (head.isValid) {
				
				if (!blockedMap.GetTile (head)) {
					map.SetTile (head, true);
					head.Move (d);
				} 
				else {
					map.SetTile (head, true);
					break;
				}
			}
		}
		return map;
	}
	public static BoardMap BlockedLineMovement(Position position, BoardMap blockedMap,bool diagonal){
		return BlockedLineMovement (position.X, position.Y, blockedMap, diagonal);
	}

	#endregion

	/// <summary> Returns a BoardMap with all occuppied places by a given Piece vector.</summary>
	public static BoardMap OcuppiedPlaces(Piece[] pieces){
		BoardMap map = new BoardMap (false);
		for (int i = 0; i < pieces.Length; i++) {
			Position pos = pieces[i].GetPosition ();
			if (pos.isValid)
				map.SetTile (pos, true);
		}
		return map;
	}
	/// <summary> Returns a BoardMap with all specific player's occuppied places by a given Piece vector.</summary>
	public static BoardMap OcuppiedPlaces(Piece[] pieces, Player player){
		BoardMap map = new BoardMap (false);
		for (int i = 0; i < pieces.Length; i++) {
			Position pos = pieces[i].GetPosition ();
			if (pos.isValid && pieces[i].GetPlayer() == player)
				map.SetTile (pos, true);
		}
		return map;
	}

	public static BoardMap OcuppiedEnemyPlaces(Piece[] pieces, Player player){
		BoardMap map = new BoardMap (false);
		for (int i = 0; i < pieces.Length; i++) {
			Position pos = pieces[i].GetPosition ();
			if (pos.isValid && pieces[i].GetPlayer() != player)
				map.SetTile (pos, true);
		}
		return map;
	}

	//public static BoardMap RookMovement(Position position, BoardMap pieceMap){
	
	//}
	/// <summary> Visual string representation of a boardmap </summary>
	public override string ToString(){
		string result = "";
		for (int j = 0; j < 8; j++) 
			for (int i = 0; i < 8; i++) {
				result += GetTile(i,7 - j)?"X ":"  ";
				if (i == 7)
					result += "\n";
			}
		return result;
	}
		
}