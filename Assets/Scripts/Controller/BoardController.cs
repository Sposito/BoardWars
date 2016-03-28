﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BoardController : MonoBehaviour {
	static SquareBehaviour[] squares;
	static BoardMap map;

	public readonly static float xSpacing = 2.07f;
	public readonly static float ySpacing = 1.69f;

	private static BoardMap highlight;
	private static BoardMap redHighlight;
	private static BoardMap blueHighlight;

	public GameObject GUI;

	private static bool hasPieceSelected = false;

	private static Position firstClickPos;

	static GameController gameController;

	void Start(){
		gameController = new GameController ();
		gameObject.AddComponent<BuildBoard> ();
		GUI.SetActive (true);

	}

	void Update(){
		
	}

	public void SetSquareBehaviours(SquareBehaviour[] squareBehaviours){
		squares = squareBehaviours;
	
	}

	public static void SetHighlightSquare(Position position){
		if (hasPieceSelected)
			return;
		
		Piece piece = gameController.GetPiecebyPos (position);
		if (piece == null) {
			ClearHighliths();
			highlight = BoardMap.SinglePosition (position);

		}
		else {
			Player currentPlayer = GetCurrentPlayer ();
			Player piecePlayer = gameController.GetPiecebyPos (position).GetPlayer ();
			ClearHighliths ();

			if (currentPlayer == piecePlayer) {
				blueHighlight = BoardMap.SinglePosition (position);
				highlight = gameController.GetPiecebyPos (position).Movement.Highlight;
			}
				
			HighlightMap (highlight);

		}
	}
		
	public static void HighlightMap( BoardMap map){
		
		//map = BoardMap.Cross(x,y);
		for(int i = 0; i < 8; i++){
			for(int j = 0; j < 8; j++){
				if (map.GetTile(i,j))
					squares[j + (i * 8)].Select();
				else
					squares[j + (i * 8)].UnSelect();
					
			}
		}
	}

	public static BoardMap GetEnemiesMap(Player player ){
		return BoardMap.OcuppiedEnemyPlaces (GetPieces (), player);
	}

	public static BoardMap GetPiecessMap(){
		return BoardMap.OcuppiedPlaces (GetPieces ());
	}

	public static BoardMap GetPiecessMap(Player player){
		Piece[] pieces = GetPieces (player);
		return BoardMap.OcuppiedPlaces (pieces);
	}

	private static void ClearHighliths(){
		highlight = BoardMap.Empty;
		redHighlight = BoardMap.Empty;
		blueHighlight = BoardMap.Empty;
	}

	public static void Click(Position position){
		Piece piece = gameController.GetPiecebyPos (position);
		if (hasPieceSelected) {
			if (highlight.GetTile (position.X, position.Y)) {
				//MoveIfIsanEmptyTile (piece, position);
				if (piece == null) {
//					GameState newGameState = gameController.GetCurrentState ();
//					newGameState.MovePiece (firstClickPos, position, false);
//					newGameState.NextPlayer ();
//					gameController.AddGameState (newGameState);
//
//					GameObject pieceGameObject = GameObject.Find (firstClickPos.ToString ()).transform.GetChild (0).gameObject;
//					pieceGameObject.transform.position = position.ToScenePosition ();
//					pieceGameObject.transform.SetParent (GameObject.Find (position.ToString ()).transform);
					MoveIfIsanEmptyTile (piece, position);
					print (gameController.GetGameJSON ());
				}

				else {
					if (piece.ReceiveHit (gameController.GetPiecebyPos (firstClickPos))) { // here we make the test and update the hp
						//Remove all gameobjects if a king is captured
						RemovePiecesIfKing(piece);

						GameState newGameState = gameController.GetCurrentState ();
						newGameState.MovePiece (firstClickPos, position, true);
						//newGameState.MovePiece (position, firstClickPos);
						newGameState.NextPlayer ();
						gameController.AddGameState (newGameState);



						GameObject pieceGameObject = GameObject.Find (firstClickPos.ToString ()).transform.GetChild (0).gameObject;
						GameObject pieceGO = GameObject.Find (position.ToString ()).transform.GetChild (0).gameObject;
						pieceGameObject.GetComponent<PieceBehaviour> ().MoveAndDestroy (new MoveAndDestroyMessage(pieceGO, position));


//						GameObject pieceGameObject = GameObject.Find (firstClickPos.ToString ()).transform.GetChild (0).gameObject;
//						pieceGameObject.transform.position = position.ToScenePosition ();
//						pieceGameObject.transform.SetParent (GameObject.Find (position.ToString ()).transform);

					

					} else {
						GameState newGameState = gameController.GetCurrentState ();
						newGameState.NextPlayer ();
						gameController.AddGameState (newGameState);

						GameObject pieceGameObject = GameObject.Find (firstClickPos.ToString ()).transform.GetChild (0).gameObject;
						pieceGameObject.GetComponent<PieceBehaviour> ().MoveAndBack (position);
					}
						
				
				}
			}
			hasPieceSelected = false;
			print (gameController.GetStringStates ());
		} 

		else {
			
			if (piece != null) {
				Player player = piece.GetPlayer ();
				Player currentPlayer = GetCurrentPlayer ();
		 
				if (player == currentPlayer && !piece.Movement.Highlight.IsEmpty) {
					print ("Click");
					hasPieceSelected = true;
					firstClickPos = position;
				}
			}
		}
	}

	static void RemovePiecesIfKing(Piece piece){
		if (piece.GetKind() == ItemKind.KING) {
			Piece[] piecesTobeRemoved = gameController.GetCurrentState ().GetPieces (piece.GetPlayer ());
			//List<Position> piecePositions = new List<Position> ();
			foreach (Piece p in piecesTobeRemoved) {
				Destroy (GameObject.Find (p.GetPosition().ToString ()).transform.GetChild (0).gameObject);
			}
		}
	}

	static void MoveIfIsanEmptyTile(Piece piece, Position position){
		if (piece == null) {
			//INTERNAL LOGIC
			GameState newGameState = gameController.GetCurrentState ();
			newGameState.MovePiece (firstClickPos, position, false);
			newGameState.NextPlayer ();
			gameController.AddGameState (newGameState);

			//SCENE LOGIC
			GameObject pieceGameObject = GameObject.Find (firstClickPos.ToString ()).transform.GetChild (0).gameObject;
			pieceGameObject.GetComponent<PieceBehaviour> ().Move (position);
		

		}
	}

	public static GameState GetCurrentState(){
		return gameController.GetCurrentState ();
	}

	public static Player GetCurrentPlayer(){
		return GetCurrentState ().GetCurrentPlayer ();
	}

	public static Piece[] GetPieces(){
		return GetCurrentState ().GetPieces ();
	}

	public static Piece[] GetPieces(Player player){
		return GetCurrentState ().GetPieces (player);
	}

	public static Piece GetPiece(Position position){
		return GetCurrentState ().GetPiecebyPosition (position);
	}
}
