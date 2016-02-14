using UnityEngine;
using System.Collections;

public class BuildBoard : MonoBehaviour {

	public GameObject tile;
	public float xSpacing = 2.07f;
	public float ySpacing = 1.69f;

	public Color white = Color.white;
	public Color black = Color.black;


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


		GameObject currentGO;

		for (int i = 0; i < 64; i++){
			currentGO = (GameObject)Instantiate (tile, currentTile, Quaternion.identity);
			currentGO.GetComponent<SpriteRenderer> ().sortingOrder = layerOrder;
			currentGO.GetComponent<SpriteRenderer> ().color = (i % 2 == 0)? white:black;

			if (rowCount < 7) {
				//Defines where the next tile will be placed
				currentTile += tileRight;
				rowCount++;
				//Define next tile LAyerOrder
				layerOrder += orderDirection;

			}
			else{
				//Defines where the next tile will be placed
				currentTile += tileDown;
				tileRight *= -1;
				rowCount = 0;
				//Define next tile LayerOrder
				layerOrder += 1;
				orderDirection *= -1;


			}


		}
	}





}
