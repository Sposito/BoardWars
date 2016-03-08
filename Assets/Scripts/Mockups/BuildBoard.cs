using UnityEngine;
using System.Collections;

public class BuildBoard : MonoBehaviour {

	public GameObject tile;
	public float xSpacing = 2.07f;
	public float ySpacing = 1.69f;

	public Color white = Color.white;
	public Color black = Color.black;
	public Color selectedColor = Color.blue;

	Vector3 tileRight;
	Vector3 tileDown;
	Vector3 currentTile;

	void Start () {
		tileRight = new Vector3 (xSpacing, -ySpacing);
		tileDown = new Vector3 (-xSpacing, -ySpacing);
		currentTile = Vector3.zero;

		Build ();
	}
		
	private void Build(){
		
		int rowCount = 0;
		int layerOrder = 0;
		int orderDirection = 1;

		string columns = "abcdefgh";
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



	private Vector3 GetScenePosition(int x, int y){
		return tileRight * x + tileDown * y;
	}

}