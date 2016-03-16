using UnityEngine;
using System.Collections;


public class InitialStateBuilder  {

	public static GameState CreateInitialGameState(){
		return  new GameState (HardCodedBuild (), Player.PLAYER1, 0f, Position.zero, Position.zero);

	}
	public static Piece[] HardCodedBuild(){


		Piece[] pieces = new Piece[32];
		int i = 0;
		pieces [i] = Piece.BuildStadardWoodPiece     (ItemKind.ROOK,   Player.PLAYER1, 0, 0); i++;
		pieces [i] = Piece.BuildStadardWoodPiece     (ItemKind.KNIGHT, Player.PLAYER1, 0, 1); i++;
		pieces [i] = Piece.BuildStadardWoodPiece     (ItemKind.MAGE,   Player.PLAYER1, 0, 2); i++;
		pieces [i] = Piece.BuildStadardWoodPiece     (ItemKind.KING,   Player.PLAYER1, 0, 3); i++;
		for(int j = 0; j< 4; j++){
			pieces [i] = Piece.BuildStadardWoodPiece (ItemKind.PAWN,   Player.PLAYER1, 1, j); i++;
		}

		pieces [i] = Piece.BuildStadardWoodPiece     (ItemKind.ROOK,   Player.PLAYER2, 0, 7); i++;
		pieces [i] = Piece.BuildStadardWoodPiece     (ItemKind.KNIGHT, Player.PLAYER2, 1, 7); i++;
		pieces [i] = Piece.BuildStadardWoodPiece     (ItemKind.MAGE,   Player.PLAYER2, 2, 7); i++;
		pieces [i] = Piece.BuildStadardWoodPiece     (ItemKind.KING,   Player.PLAYER2, 3, 7); i++;
		for(int j = 0; j< 4; j++){
			pieces [i] = Piece.BuildStadardWoodPiece (ItemKind.PAWN,   Player.PLAYER2, j, 6); i++;
		}

		pieces [i] = Piece.BuildStadardWoodPiece     (ItemKind.ROOK,   Player.PLAYER3, 7, 7); i++;
		pieces [i] = Piece.BuildStadardWoodPiece     (ItemKind.KNIGHT, Player.PLAYER3, 7, 6); i++;
		pieces [i] = Piece.BuildStadardWoodPiece     (ItemKind.MAGE,   Player.PLAYER3, 7, 5); i++;
		pieces [i] = Piece.BuildStadardWoodPiece     (ItemKind.KING,   Player.PLAYER3, 7, 4); i++;
		for(int j = 0; j < 4 ; j++){ 
			pieces [i] = Piece.BuildStadardWoodPiece (ItemKind.PAWN,   Player.PLAYER3, 6, 7-j); i++;
		}

		pieces [i] = Piece.BuildStadardWoodPiece     (ItemKind.ROOK,   Player.PLAYER4, 7, 0); i++;
		pieces [i] = Piece.BuildStadardWoodPiece     (ItemKind.KNIGHT, Player.PLAYER4, 6, 0); i++;
		pieces [i] = Piece.BuildStadardWoodPiece     (ItemKind.MAGE,   Player.PLAYER4, 5, 0); i++;
		pieces [i] = Piece.BuildStadardWoodPiece     (ItemKind.KING,   Player.PLAYER4, 4, 0); i++;
		for(int j = 0; j < 4 ; j++){ 
			pieces [i] = Piece.BuildStadardWoodPiece (ItemKind.PAWN,   Player.PLAYER4, 7-j, 1); i++;
		}

		return pieces;

	}

	public static BoardMap PrintPieces(){
		Piece[] pieces = HardCodedBuild ();
		BoardMap map = new BoardMap (false);
		for (int i = 0; i < pieces.Length; i++) {
			//Debug.Log (pieces [i].GetName () + ", " + pieces [i].GetPosition ().ToString ());
			map.SetTile(pieces[i].GetPosition(), true);
		}


		Debug.Log (map.ToString ());
		return map;
		//Piece p = Piece.BuildStadardWoodPiece(ItemKind.KING, Player.PLAYER1,0,0);
		//Debug.Log ( p.GetKind());
	}

}

