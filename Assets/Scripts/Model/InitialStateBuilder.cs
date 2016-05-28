using UnityEngine;
using System.Collections;


public class InitialStateBuilder  {

	public static GameState CreateInitialGameState(){
		return  new GameState (HardCodedBuild (), Player.PLAYER1,Team.A, 0f, Position.zero, Position.zero);

	}
	public static Piece[] HardCodedBuild(){

		//PLAYER 1
		Piece[] pieces = new Piece[32];
		int i = 0;
		pieces [i] = Piece.BuildStadardRedWaxPiece     (ItemKind.ROOK,   Player.PLAYER1, 0, 0); i++;
		pieces [i] = Piece.BuildStadardRedWaxPiece     (ItemKind.KNIGHT, Player.PLAYER1, 0, 1); i++;
		pieces [i] = Piece.BuildStadardRedWaxPiece     (ItemKind.MAGE,   Player.PLAYER1, 0, 2); i++;
		pieces [i] = Piece.BuildStadardRedWaxPiece     (ItemKind.KING,   Player.PLAYER1, 0, 3); i++;
		for(int j = 0; j< 4; j++){
			pieces [i] = Piece.BuildStadardRedWaxPiece (ItemKind.PAWN,   Player.PLAYER1, 1, j); i++;
		}
		//PLAYER 2
		pieces [i] = Piece.BuildStadardIcePiece     (ItemKind.ROOK,   Player.PLAYER2, 0, 7); i++;
		pieces [i] = Piece.BuildStadardIcePiece     (ItemKind.KNIGHT, Player.PLAYER2, 1, 7); i++;
		pieces [i] = Piece.BuildStadardIcePiece     (ItemKind.MAGE,   Player.PLAYER2, 2, 7); i++;
		pieces [i] = Piece.BuildStadardIcePiece     (ItemKind.KING,   Player.PLAYER2, 3, 7); i++;
		for(int j = 0; j< 4; j++){
			pieces [i] = Piece.BuildStadardIcePiece (ItemKind.PAWN,   Player.PLAYER2, j, 6); i++;
		}
		//PLAYER 3
		pieces [i] = Piece.BuildStadardGreenWaxPiece     (ItemKind.ROOK,   Player.PLAYER3, 7, 7); i++;
		pieces [i] = Piece.BuildStadardGreenWaxPiece     (ItemKind.KNIGHT, Player.PLAYER3, 7, 6); i++;
		pieces [i] = Piece.BuildStadardGreenWaxPiece     (ItemKind.MAGE,   Player.PLAYER3, 7, 5); i++;
		pieces [i] = Piece.BuildStadardGreenWaxPiece     (ItemKind.KING,   Player.PLAYER3, 7, 4); i++;
		for(int j = 0; j < 4 ; j++){ 
			pieces [i] = Piece.BuildStadardGreenWaxPiece (ItemKind.PAWN,   Player.PLAYER3, 6, 7-j); i++;
		}
		//PLAYER 4
		pieces [i] = Piece.BuildStadardBeeWaxPiece     (ItemKind.ROOK,   Player.PLAYER4, 7, 0); i++;
		pieces [i] = Piece.BuildStadardBeeWaxPiece     (ItemKind.KNIGHT, Player.PLAYER4, 6, 0); i++;
		pieces [i] = Piece.BuildStadardBeeWaxPiece     (ItemKind.MAGE,   Player.PLAYER4, 5, 0); i++;
		pieces [i] = Piece.BuildStadardBeeWaxPiece     (ItemKind.KING,   Player.PLAYER4, 4, 0); i++;
		for(int j = 0; j < 4 ; j++){ 
			pieces [i] = Piece.BuildStadardBeeWaxPiece (ItemKind.PAWN,   Player.PLAYER4, 7-j, 1); i++;
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

