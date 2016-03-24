using UnityEngine;
using System.Collections;

public class BuildBoard : MonoBehaviour {

	public GameObject tile;
	public float xSpacing = BoardController.xSpacing;
	public float ySpacing = BoardController.ySpacing;

	public Color white = GeneralTools.Hex.ToColor("A2AD5DC0");
	public Color black = GeneralTools.Hex.ToColor("C6CB68B6");
	public Color selectedColor = GeneralTools.Hex.ToColor ("EAD562FF");

	Vector3 tileRight;
	Vector3 tileDown;
	Vector3 currentTile;

	void Start () {
		tileRight = new Vector3 (xSpacing, -ySpacing);
		tileDown = new Vector3 (-xSpacing, -ySpacing);
		currentTile = Vector3.zero;
		tile =(GameObject) Resources.Load ("Prefabs/square");

		Build ();
		AddPieces ();
	}
		
	private void Build(){
		
		int rowCount = 0;
		int layerOrder = 0;
		int orderDirection = 1;

		string columns = "ABCDEFGH";
		string rows = "12345678";

		int nextRow = 7;
		int nextColumn = 0;

		GameObject currentGO;
		SquareBehaviour[] sqBehaviour = new SquareBehaviour[64];

		for (int i = 0; i < 64; i++){
			currentGO = (GameObject)Instantiate (tile, currentTile, Quaternion.identity);
			currentGO.GetComponent<SpriteRenderer> ().sortingOrder = layerOrder;
			currentGO.GetComponent<SpriteRenderer> ().color = (i % 2 == 0)? white:black;
			currentGO.name = (columns [nextColumn] + "" + rows [nextRow]) ;
			currentGO.transform.SetParent (transform);
			sqBehaviour[nextRow + nextColumn * 8] = currentGO.GetComponent<SquareBehaviour> ();
			currentGO.GetComponent<SquareBehaviour> ().position = new Position (nextColumn, nextRow);
			if (rowCount < 7) {
				//Defines where the next tile will be placed
				currentTile += tileRight;
				rowCount++;
				//Define next tile LayerOrder
				layerOrder += orderDirection;
				//Define the  parameters which will define the next gameObject's name
				nextColumn += orderDirection;
			}
			else{
				//Defines where the next tile will be placed
				currentTile += tileDown;
				tileRight *= -1;
				rowCount = 0;
				//Define next tile LayerOrder
				layerOrder += 1;
				orderDirection *= -1;
				//Define the  parameters which will define the next gameObject's name
				nextRow--;
					// -> its making use of the orderDirection reversal executed in the aboves step
			}
		}

		GetComponent<BoardController> ().SetSquareBehaviours (sqBehaviour);
	}

	public void AddPieces(){
		GameState gameState = BoardController.GetCurrentState ();
		GameObject pieceGO = (GameObject)Resources.Load ("Prefabs/piece");
			
		foreach (Piece p in gameState) {
			string name = p.GetPosition ().ToString ();
			GameObject currentPiece = (GameObject)Instantiate (pieceGO, transform.Find (name).position, Quaternion.identity);
			currentPiece.transform.SetParent (transform.Find (name));

			PieceBehaviour pieceBehaviour =	currentPiece.GetComponent<PieceBehaviour> ();
			pieceBehaviour.SetPieceKind (p.GetKind ());
			pieceBehaviour.SetPlayer (p.GetPlayer ());
			pieceBehaviour.BuildPiece ();
		}

	}

	public void AddPieces(bool b){
		GameState gameState = BoardController.GetCurrentState ();
		GameObject pieceGO = (GameObject)Resources.Load ("Prefabs/piece");
		foreach (Piece p in gameState) {
			string name = p.GetPosition ().ToString ();
			GameObject currentPiece = (GameObject)Instantiate (pieceGO, transform.Find (name).position, Quaternion.identity);
			if(b)
				currentPiece.transform.SetParent (transform.Find (name));

			PieceBehaviour pieceBehaviour =	currentPiece.GetComponent<PieceBehaviour> ();
			pieceBehaviour.SetPieceKind (p.GetKind ());
			pieceBehaviour.SetPlayer (p.GetPlayer ());
			pieceBehaviour.BuildPiece ();

		}

	}

	private Vector3 GetScenePosition(int x, int y){
		return tileRight * x + tileDown * y;
	}

}