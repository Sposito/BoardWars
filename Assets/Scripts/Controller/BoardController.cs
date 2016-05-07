using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

enum States{PRESELECTION, PIECECHOICE, TARGETCHOICE,POSELECTION}
public class BoardController : NetworkBehaviour {
	#region GlobalVariables
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
	public static UIController uIController;

	private static bool hasPieceSelected = false;

	private static Position firstClickPos;

	private static Piece clickedPiece; //
	private static Position clickedPosition;

	static GameController gameController;

	public static bool isGameRunning = false;

	public static float timeClock;
	#endregion

	void Update(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			print(gameController.GetStringStates ());
		}
			
	}

	void Start(){
		gameController = new GameController ();
		gameObject.AddComponent<BuildBoard> ();
		GUI.SetActive (true);
		isGameRunning = true;
		uIController = GetComponent<UIController> ();
		timeClock = Time.unscaledTime;

	}
		
	public static void Click(Position position){
		//the following two lines give class scope access to the actions taken by intern functions
		clickedPiece = gameController.GetPiecebyPos (position);
		clickedPosition = position;

		if (hasPieceSelected) { // HAVE YOU SELECTED A VALID PIECE BEFORE?
			if (highlight.GetTile (position.X, position.Y) ) {// HAVE YOU CLICKED IN A VALID MOVEABLE POSITION?
				if (clickedPiece == null)  //THE VALID POSITION U CLICKED IS AN EMPTY TILE?
					MoveIfIsanEmptyTile (clickedPiece, position); //JUST MOVE THE PIECE TO THERE
				
				else  //There is an attackable piece on the cliked postion
					AttackPiece();
			}
			hasPieceSelected = false;
		} 

		else { // Here you select the current piece
			if (clickedPiece != null) {
				Player player = clickedPiece.GetPlayer ();
				Player currentPlayer = GetCurrentPlayer ();
				if (player == currentPlayer && !clickedPiece.Movement.Highlight.IsEmpty) {
					hasPieceSelected = true;
					firstClickPos = position;
				}
			}
		}
	}

	static void AttackPiece(){
		if (!gameController.CheckIfIsSamePlayer (firstClickPos, clickedPosition)) { // Check if the target isnt from you team TODO: WHEN WE HAVE HEALING WORKING THIS MAY BE A PROBLEM
			if (clickedPiece.ReceiveHit (gameController.GetPiecebyPos (firstClickPos))) { // here we make the test and update the hp
				//Remove all gameobjects if a king is captured

				RemovePiecesIfKing (clickedPiece);

				GameState newGameState = new GameState(gameController.GetCurrentState ());
				newGameState.MovePiece (firstClickPos, clickedPosition, true);
				newGameState.NextPlayer ();
				gameController.AddGameState (newGameState);

				GameObject attackerPieceGO = GameObject.Find (firstClickPos.ToString ()).transform.GetChild (0).gameObject;
				GameObject defenderPieceGO = GameObject.Find (clickedPosition.ToString ()).transform.GetChild (0).gameObject;
				attackerPieceGO.GetComponent<PieceBehaviour> ().MoveAndDestroy (new MoveAndDestroyMessage (defenderPieceGO, clickedPosition));

			} else {
				GameState newGameState = new GameState(gameController.GetCurrentState ());
				newGameState.NextPlayer ();

				GameObject pieceGameObject = GetPieceGameOBjectbyPosition (firstClickPos);
				gameController.AddGameState (newGameState);


				pieceGameObject.GetComponent<PieceBehaviour> ().MoveAndBack (clickedPosition);
			}

			TestForEndOfGame ();
		}
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
				int p = j + (i * 8); // p is the linear postion in the array
				if (map.GetTile (i, j)) 
					squares [p].Select ();
				else 
					squares [p].UnSelect ();
			}
		}
	}

	public static void HighlightMap( BoardMap map, Color color){
		for(int i = 0; i < 8; i++){
			for(int j = 0; j < 8; j++){
				int p = j + (i * 8); // p is the linear postion in the array
				if (map.GetTile (i, j)) 
					squares [p].Select (color);
				else 
					squares [p].UnSelect ();
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

		HighlightMap (highlight);
	}

	static GameObject GetPieceGameOBjectbyPosition(Position position){
		return GameObject.Find (position.ToString ()).transform.GetChild (0).gameObject;
	}

	static void RemovePiecesIfKing(Piece piece){
		if (piece.GetKind() == ItemKind.KING) {
			Piece[] piecesTobeRemoved = gameController.GetCurrentState ().GetPieces (piece.GetPlayer ());

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
	public static int GetCurrentTurn(){
		return gameController.TotalTurns;
	}
	public  static void TestForEndOfGame (){
		EoGMessage message = gameController.TestForEndOfGame ();

		if (message.gameOver) {
			isGameRunning = false;
			ClearHighliths ();
			timeClock = Time.unscaledTime - timeClock;
			uIController.GameOver (message.victoriousTeam);
		}
	}

	public void RestartGame(){
		UnityEngine.SceneManagement.SceneManager.LoadScene (0, UnityEngine.SceneManagement.LoadSceneMode.Single);
	}
}