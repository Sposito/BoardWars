using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class LifeBarBehaviour : MonoBehaviour {

	GameObject barGO;
	RectTransform rectTranform;
	List<Image> barSprites;

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
			Piece[] pieces = BoardController.GetCurrentState ().GetPieces ();
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
		totalHP = BoardController.GetCurrentState ().GetPiecebyPosition (position).GetTotalHP ();
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
		if (pieceToFollow == null)
			Destroy (gameObject);
		else {
			transform.position = pieceToFollow.transform.position + Vector3.down * 1.07f;
			UpdateLife ();
		}
	}

void	UpdateLife(){
		try{
			int hp = pieceToFollow.GetComponent<PieceBehaviour>().piece.GetHP();
			if (lastHP != hp){
				lastHP = hp;

					transform.GetChild(totalHP - hp - 1).gameObject.GetComponent<Image>().color = Color.black;
				
			}
		}
		catch{

		}
	}

}
