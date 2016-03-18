﻿using UnityEngine;
using System.Collections;

public class BoardController : MonoBehaviour {
	static SquareBehaviour[] squares;
	static BoardMap map;

	public readonly static float xSpacing = 2.07f;
	public readonly static float ySpacing = 1.69f;

	private static BoardMap highlight;
	private static BoardMap redHighlight;
	private static BoardMap blueHighlight;

	private static bool hasPieceSelected = false;

	private static Position firstClickPos;

	static GameController gameController;

	void Start(){
		gameController = new GameController ();
		gameObject.AddComponent<BuildBoard> ();
	}

	public static GameState GetCurrentState(){
		return  gameController.GetCurrentState();
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
			Player currentPlayer = gameController.GetCurrentState ().GetCurrentPlayer ();
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
		return BoardMap.OcuppiedEnemyPlaces (GetCurrentState ().GetPieces (), player);
	}

	public static BoardMap GetPiecessMap(){
		return BoardMap.OcuppiedPlaces (GetCurrentState ().GetPieces ());
	}

	public static BoardMap GetPiecessMap(Player player){
		Piece[] pieces = GetCurrentState ().GetPieces (player);
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
				if (piece == null) {
					GameState newGameState = gameController.GetCurrentState ();
					newGameState.MovePiece (firstClickPos, position);
					newGameState.NextPlayer ();
					gameController.AddGameState (newGameState);
					GameObject pieceGameObject = GameObject.Find (firstClickPos.ToString ()).transform.GetChild (0).gameObject;
					pieceGameObject.transform.position = position.ToScenePosition ();
					pieceGameObject.transform.SetParent (GameObject.Find (position.ToString ()).transform);


					//GameObject.Find(firstClickPos.ToString()).transform.position = position.ToScenePosition ();
					//GameObject.Find("Board").GetComponent<BuildBoard>().AddPieces(false);

				}
			}
			hasPieceSelected = false;
			print (gameController.GetStringStates ());
		} 

		else {
			
			if (piece != null) {
				Player player = piece.GetPlayer ();
				Player currentPlayer = GetCurrentState ().GetCurrentPlayer ();
		 
				if (player == currentPlayer && !piece.Movement.Highlight.IsEmpty) {
					print ("Click");
					hasPieceSelected = true;
					firstClickPos = position;
				}
			}
		}
	}
}
