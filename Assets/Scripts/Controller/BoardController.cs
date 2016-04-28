using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum States{PRESELECTION, PIECECHOICE, TARGETCHOICE,POSELECTION}
public class BoardController : MonoBehaviour {
	static SquareBehaviour[] squares;
	static BoardMap map;
	public static Piece currentPiece;

	public static float rythmRate = 1f;

	public readonly static float xSpacing = 2.07f;
	public readonly static float ySpacing = 1.69f;

	private static BoardMap highlight;
	private static BoardMap redHighlight;
	private static BoardMap blueHighlight;
	private static BoardMap playerHighlight;

	public GameObject GUI;

	private static bool hasPieceSelected = false;

	private static Position firstClickPos;

	static GameController gameController;
	void Update(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			print(gameController.GetStringStates ());
		}
			
	}
	
	void Start(){
		gameController = new GameController ();
		gameObject.AddComponent<BuildBoard> ();
		GUI.SetActive (true);
	}

	public void SetSquareBehaviours(SquareBehaviour[] squareBehaviours){
		squares = squareBehaviours;
	}

	public static void SetHighlightSquare(Position position){
		currentPiece = GetPiece (position);
		if (hasPieceSelected)
			return;
		
		Piece piece = gameController.GetPiecebyPos (position);
		if (piece == null) {
			ClearHighliths();
			highlight = BoardMap.SinglePosition (position);
			HighlightMap (highlight, GeneralTools.Hex.ToColor ("EAD562FF"));

		}
		else {
			Player currentPlayer = GetCurrentPlayer ();
			Player piecePlayer = gameController.GetPiecebyPos (position).GetPlayer ();
			ClearHighliths ();

			if (currentPlayer == piecePlayer) {
				blueHighlight = BoardMap.SinglePosition (position);
				highlight = gameController.GetPiecebyPos (position).Movement.Highlight;
			} 
		
			try{	
				HighlightMap (highlight);
			}
			catch{
			
			}
		}
	}
		
	public static void HighlightMap( BoardMap map){
		for(int i = 0; i < 8; i++){
			for(int j = 0; j < 8; j++){
				if (map.GetTile(i,j))
					squares[j + (i * 8)].Select();
				else
					squares[j + (i * 8)].UnSelect();
			}
		}
	}

	public static void HighlightMap( BoardMap map, Color color){
		for(int i = 0; i < 8; i++){
			for(int j = 0; j < 8; j++){
				if (map.GetTile(i,j))
					squares[j + (i * 8)].Select(color);
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
		if (hasPieceSelected) { // HAVE YOU SELECTED A VALID PIECE BEFORE?
			if (highlight.GetTile (position.X, position.Y) ) {// HAVE YOU CLICKED IN A VALID MOVEABLE POSITION?
				if (piece == null) { //THE VALID POSITION U CLICKED IS AN EMPTY TILE?
					MoveIfIsanEmptyTile (piece, position); //JUST MOVE THE PIECE TO THERE
				}

				else { //It is not empty?
					if (!gameController.CheckIfIsSamePlayer (firstClickPos, position)) { // Check if the target isnt from you team TODO: WHEN WE HAVE HEALING WORKING THIS MAY BE A PROBLEM
						if (piece.ReceiveHit (gameController.GetPiecebyPos (firstClickPos))) { // here we make the test and update the hp
							//Remove all gameobjects if a king is captured

							RemovePiecesIfKing (piece);

							GameState newGameState = new GameState(gameController.GetCurrentState ());
							newGameState.MovePiece (firstClickPos, position, true);
							newGameState.NextPlayer ();
							gameController.AddGameState (newGameState);

							GameObject attackerPieceGO = GameObject.Find (firstClickPos.ToString ()).transform.GetChild (0).gameObject;
							GameObject defenderPieceGO = GameObject.Find (position.ToString ()).transform.GetChild (0).gameObject;
							attackerPieceGO.GetComponent<PieceBehaviour> ().MoveAndDestroy (new MoveAndDestroyMessage (defenderPieceGO, position));

						} else {
							GameState newGameState = new GameState(gameController.GetCurrentState ());
							newGameState.NextPlayer ();

							GameObject pieceGameObject = GetPieceGameOBjectbyPosition (firstClickPos);
							gameController.AddGameState (newGameState);


							pieceGameObject.GetComponent<PieceBehaviour> ().MoveAndBack (position);
						}
					}
				}
			}
			hasPieceSelected = false;
		} 

		else { // Here you select the current piece
			if (piece != null) {
				Player player = piece.GetPlayer ();
				Player currentPlayer = GetCurrentPlayer ();
				if (player == currentPlayer && !piece.Movement.Highlight.IsEmpty) {
					hasPieceSelected = true;
					firstClickPos = position;
				}
			}
		}
	}

	static GameObject GetPieceGameOBjectbyPosition(Position position){
		return GameObject.Find (position.ToString ()).transform.GetChild (0).gameObject;
	}

	static void RemovePiecesIfKing(Piece piece){
		if (piece.GetKind() == ItemKind.KING) {
			Piece[] piecesTobeRemoved = gameController.GetCurrentState ().GetPieces (piece.GetPlayer ());
			//List<Position> piecePositions = new List<Position> ();
			foreach (Piece p in piecesTobeRemoved) {
				if (p.GetKind () != ItemKind.KING) { // AVOIDS NULL REFERENCE EXCEPTION CAUSED BY TRYING TO REMOVE THE KING TWICE
					GameObject go = GameObject.Find (p.GetPosition ().ToString ()).transform.GetChild (0).gameObject;
					go.GetComponent<PieceBehaviour> ().StartCoroutine ("DestroyPiece");
				}
			}
		}
	}

	static void MoveIfIsanEmptyTile(Piece piece, Position position){
		if (piece == null) {
			//INTERNAL LOGIC
			GameState newGameState = new GameState(gameController.GetCurrentState ());
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
