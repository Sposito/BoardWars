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
	Vector3 offset = new Vector3 (0f, -1.07f);
	PieceBehaviour pieceBehaviour;

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
		transform.position = position.ToScenePosition () + offset;
		pieceToFollow = GameObject.Find (position.ToString ()).transform.GetChild (0).gameObject;
		lastHP = totalHP;
		transform.localScale = new Vector3 (-1f, 1f, 1f);
		pieceBehaviour = pieceToFollow.GetComponent<PieceBehaviour> ();
	}
	
	public void BuildBar(Position pos){
		position = pos;
		totalHP = BoardController.GetPiece(position).GetTotalHP ();
		for (int i = 1; i < totalHP; i++) {
			GameObject newBar = (GameObject)Instantiate (barGO, Vector3.zero,	Quaternion.identity);
			newBar.transform.SetParent (transform);

		}
		rectTranform.sizeDelta = new Vector2 (.5f * totalHP, rectTranform.sizeDelta.y);


	}

	void RestartBar(){
		for (int i = 1; i < transform.childCount; i++){
			Destroy(transform.GetChild(i));
		}
		rectTranform.sizeDelta = new Vector2 (.5f, rectTranform.sizeDelta.y);

	}

	void Update(){ //TODO: OPTIMIZATION >>> TAKE IT OUT OF UPDATE WILL SAVE SEVERAL CYCLES;
		if (pieceToFollow == null) {
			Destroy (gameObject);
			isDestroyed = true;
		}
		else {
			transform.position = pieceToFollow.transform.position + offset;
			//PieceBehaviour pieceBehaviour = pieceToFollow.GetComponent<PieceBehaviour> ();
			if (pieceBehaviour.piece != null && !isDestroyed)
				UpdateLife ();
		}
	}

	void UpdateLife(){
			//PieceBehaviour pieceBehaviour = pieceToFollow.GetComponent<PieceBehaviour> ();

		int hp = pieceBehaviour.piece.GetHP();
		if (lastHP != hp){
			lastHP = hp;
			int marker = totalHP - hp - 1; // first black bar postion in the child hierarchy
			bool rangeCheck = marker <= totalHP && marker >= 0; //assure if marker is valid child postion
				
			if (rangeCheck) { //TODO: VER ISSO AQUI!
				for(int i = 0; i <= marker; i++)
					transform.GetChild (i).gameObject.GetComponent<Image> ().color = Color.black;
			}
		}
	}

	void OnDestroy(){
		assignNo = 0;
	}
}