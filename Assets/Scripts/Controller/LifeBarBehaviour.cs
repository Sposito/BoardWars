using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class LifeBarBehaviour : MonoBehaviour {

	GameObject barGO;
	RectTransform rectTranform;
	List<Image> barSprites;
	bool isDestroyed = false;

	GameObject pieceToFollow;
	static int assignNo = 0;

	int totalHP;
	int lastHP;
	public Position position = Position.zero;

	void Start () {
		barGO = transform.GetChild (0).gameObject;
		//barSprites.Add(transform.GetChild (0).gameObject.GetComponent<Image>());
		rectTranform = GetComponent<RectTransform> ();
	
		lock (this) { 
			Piece[] pieces = BoardController.GetPieces ();
			Piece piece = pieces [assignNo++];
			position = piece.GetPosition ();
		}

		BuildBar (position);
		transform.position = position.ToScenePosition () + Vector3.down * 1.07f;
		pieceToFollow = GameObject.Find (position.ToString ()).transform.GetChild (0).gameObject;
		lastHP = totalHP;
		transform.localScale = new Vector3 (-1f, 1f, 1f);
	}
	
	public void BuildBar(Position pos){
		position = pos;
		totalHP = BoardController.GetPiece(position).GetTotalHP ();
		for (int i = 1; i < totalHP; i++) {
			GameObject newBar = (GameObject)Instantiate (barGO, Vector3.zero,	Quaternion.identity);
			newBar.transform.SetParent (transform);
//			barSprites.Add (newBar.GetComponent<Image>());
		}
		rectTranform.sizeDelta = new Vector2 (.5f * totalHP, rectTranform.sizeDelta.y);

//		foreach (Image image in barSprites)
//			image.color = Color.green;
	}

	void RestartBar(){
		for (int i = 1; i < transform.childCount; i++){
			Destroy(transform.GetChild(i));
		}
		rectTranform.sizeDelta = new Vector2 (.5f, rectTranform.sizeDelta.y);

	}

	void Update(){ //TODO: OPTIMIZATION >>> TAKE IT OUT OF UPDATE WILL SAVE SOME CYCLES;
		if (pieceToFollow == null) {
			Destroy (gameObject);
			isDestroyed = true;
		}
		else {
			transform.position = pieceToFollow.transform.position + Vector3.down * 1.07f;
			PieceBehaviour pieceBehaviour = pieceToFollow.GetComponent<PieceBehaviour> ();
			if (pieceBehaviour.piece != null && !isDestroyed)
				UpdateLife ();
		}
	}

	void UpdateLife(){
			//PieceBehaviour pieceBehaviour = pieceToFollow.GetComponent<PieceBehaviour> ();

			int hp = pieceToFollow.GetComponent<PieceBehaviour> ().piece.GetHP();
			if (lastHP != hp){
				lastHP = hp;

			bool rangeCheck = (totalHP - hp - 1) <= totalHP && (totalHP - hp - 1) >= 0; //TODO: UNDERSTAND WHAT IS GOING ON HERE!!!
				
			if(rangeCheck)
					transform.GetChild(totalHP - hp-1).gameObject.GetComponent<Image>().color = Color.black;
			}
	}
}