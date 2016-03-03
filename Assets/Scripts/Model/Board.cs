using UnityEngine;
using System.Collections;

public class Board  {
	Piece[,] pieces = new Piece[8,8];

	public Board (Piece[] pieces){
		for (int i = 0; i < pieces.Length; i++) {
			pieces [i].GetPosition ();
		}
	}
}
