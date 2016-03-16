using UnityEngine;
using System.Collections;

public class BoardController : MonoBehaviour {
	static SquareBehaviour[] squares;
	static BoardMap map;

	public readonly static float xSpacing = 2.07f;
	public readonly static float ySpacing = 1.69f;


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
	public static void SetActiveSquare(Position position){
		Piece piece = gameController.GetPiecebyPos (position);
		if (piece == null) {
			HighlightMap ( BoardMap.SinglePosition (position));

		}
		else {
			HighlightMap (piece.Movement.Highlight);

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
}
