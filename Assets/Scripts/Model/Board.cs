using UnityEngine;
using System.Collections;

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
}
