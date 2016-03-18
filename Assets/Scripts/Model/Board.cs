using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board  {
	Piece[,] pieces = new Piece[8,8];

	public Board (Piece[] pieces){
		for (int i = 0; i < pieces.Length; i++) {
			Position pos = pieces [i].GetPosition ();
			this.pieces [pos.X, pos.Y] = pieces [i];

		}
	}

	public Piece GetPiece(Position pos){
		if (pieces[pos.X, pos.Y] != null)
			return pieces[pos.X, pos.Y];
		else
			return null;

	}

	public void SetPiece(Position pos, Piece piece){
		if (pos.isValid)
			pieces [pos.X,   pos.Y] = piece;
		else
			Debug.LogError ("Invalid Position");

	}

	public void RemovePiece(Position pos){
		if (pos.isValid)
			pieces [pos.X, pos.Y] = null;
		else
			Debug.LogError ("Invalid Position");

	}

	public Piece[] GetPieces(){
		List<Piece> list = new List<Piece>();
		foreach (Piece p in pieces) {
			if (p != null) {
				if (p.GetPlayer () == Player.PLAYER1)
					list.Add (p);
			}
		}

		foreach (Piece p in pieces) {
			if (p != null) {
				if (p.GetPlayer () == Player.PLAYER2)
					list.Add (p);
			}
		}

		foreach (Piece p in pieces) {
			if (p != null) {
				if (p.GetPlayer () == Player.PLAYER3)
					list.Add (p);
			}
		}

		foreach (Piece p in pieces) {
			if (p != null) {
				if (p.GetPlayer () == Player.PLAYER4)
					list.Add (p);
			}
		}

		return list.ToArray ();
	}
}
