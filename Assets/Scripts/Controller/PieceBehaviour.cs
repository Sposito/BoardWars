using UnityEngine;
using System.Collections;
using GeneralTools;
using System;

public class PieceBehaviour : MonoBehaviour {
	//public enum Player {Player1,Player2,Player3,Player4}

	static Sprite[] sprites;
	public static string[] names;

	public Player player;
	public Piece piece{
		get{ 
			string positionString = transform.parent.gameObject.name;
			Position position =  Position.FromString (positionString); 
			return BoardController.GetPiece(position);
		}
	}
	public ItemKind pieceKind;
	public Color baseColor = Color.white;
	public Color detailColor = Color.gray;
	public Color[] playersColor = new Color[4];

	static private bool spriteLoaded = false;

	// Use this for initialization
	void Awake () {
		playersColor [0] = Hex.ToColor ("B5677E");
		playersColor [1] = Hex.ToColor ("677EB5");
		playersColor [2] = Hex.ToColor ("69B89F");
		playersColor [3] = Hex.ToColor ("B59C67");

		if (!spriteLoaded) {
			LoadSprites ();
			spriteLoaded = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetPieceKind(ItemKind kind){
		pieceKind = kind;
	}

	private void LoadSprites(){
		sprites = Resources.LoadAll<Sprite> ("Sprites/pieces");
		names = new string[sprites.Length];

		for (int i = 0; i < sprites.Length; i++) 
			names [i] = sprites [i].name;
	}

	public void SetPlayer (Player player){
		this.player = player;
	}

//	public void Move(Position position){
//		transform.position = position.ToScenePosition ();
//		transform.SetParent (GameObject.Find (position.ToString ()).transform);
//	}
	public void BuildPiece(){
		string baseName = pieceKind.ToString() + "_Base";
		string detailName = pieceKind.ToString() + "_Detail";
		GetComponent<SpriteRenderer> ().color = playersColor [(int) player];
	
		transform.GetChild(0).GetComponent<SpriteRenderer> ().sprite = sprites[Array.IndexOf (names, baseName)];
		transform.GetChild (0).GetComponent<SpriteRenderer> ().color = baseColor;
		transform.GetChild(1).GetComponent<SpriteRenderer> ().sprite = sprites[Array.IndexOf (names, detailName)];
		transform.GetChild (1).GetComponent<SpriteRenderer> ().color = detailColor;

		//GetComponent<SpriteRenderer> ().color = 
	}

	public void Move(Position position){
		StartCoroutine(MoveToPosition(position));
	}

	public void MoveAndDestroy(MoveAndDestroyMessage message){
		
		StartCoroutine(MoveToPositionAndDestroy(message));

	}

	public void MoveAndBack(Position position){
		StartCoroutine(MoveAndBackToPosition(position));
	}

	private IEnumerator MoveToPositionAndDestroy(MoveAndDestroyMessage message){
		Position position = message.position;
		float speed = 1f;

		Vector3 startPosition = transform.position;
		Vector3 endPosition = position.ToScenePosition ();
		Vector3 startPositionAbove = transform.position + Vector3.up * BoardController.ySpacing;
		Vector3 endPositionAbove = endPosition  + Vector3.up * BoardController.ySpacing;

	
		float moveDistance = (endPositionAbove - startPositionAbove).sqrMagnitude;
		float baseDistance = (Vector3.right * BoardController.xSpacing).sqrMagnitude;
		moveDistance = moveDistance / baseDistance;

		float progress = 0f;

		MoveLayer (+500);

		while (transform.position != startPositionAbove) {
			float distance = startPositionAbove.magnitude - startPosition.magnitude;
			transform.position = Vector3.Lerp (startPosition, startPositionAbove, progress);
			progress += speed * 4 * Time.deltaTime ;
			print (progress);
			if (progress > 1f)
				progress = 1f;

			yield return new WaitForEndOfFrame();
		}
		progress = 0f;


		while (transform.position != endPositionAbove) {
			transform.position = Vector3.MoveTowards (transform.position, endPositionAbove, .5f);
			yield return new WaitForEndOfFrame();
		}


		while (transform.position != endPosition) {
			transform.position = Vector3.Lerp (endPositionAbove, endPosition, progress);
			progress += speed * 3*Time.deltaTime ;

			if (progress > 1f)
				progress = 1f;

			yield return new WaitForEndOfFrame();
		}
		SetLayer (position);
		transform.SetParent (GameObject.Find (position.ToString ()).transform);
		Destroy (message.gameObject);
	}

	private IEnumerator MoveToPosition(Position position){
		float speed = 1f;

		Vector3 startPosition = transform.position;
		Vector3 endPosition = position.ToScenePosition ();
		Vector3 startPositionAbove = transform.position + Vector3.up * BoardController.ySpacing;
		Vector3 endPositionAbove = endPosition  + Vector3.up * BoardController.ySpacing;


		float moveDistance = (endPositionAbove - startPositionAbove).sqrMagnitude;
		float baseDistance = (Vector3.right * BoardController.xSpacing).sqrMagnitude;
		moveDistance = moveDistance / baseDistance;

		float progress = 0f;

		MoveLayer (+500);

		while (transform.position != startPositionAbove) {
			float distance = startPositionAbove.magnitude - startPosition.magnitude;
			transform.position = Vector3.Lerp (startPosition, startPositionAbove, progress);
			progress += speed * 4 * Time.deltaTime ;
			print (progress);
			if (progress > 1f)
				progress = 1f;

			yield return new WaitForEndOfFrame();
		}
		progress = 0f;


		while (transform.position != endPositionAbove) {
			transform.position = Vector3.MoveTowards (transform.position, endPositionAbove, .5f);
			yield return new WaitForEndOfFrame();
		}


		while (transform.position != endPosition) {
			transform.position = Vector3.Lerp (endPositionAbove, endPosition, progress);
			progress += speed * 3*Time.deltaTime ;

			if (progress > 1f)
				progress = 1f;

			yield return new WaitForEndOfFrame();
		}
		SetLayer (position);
		transform.SetParent (GameObject.Find (position.ToString ()).transform);
	}
	


	private IEnumerator MoveAndBackToPosition(Position position){
		float speed = 1f;

		Vector3 startPosition = transform.position;
		Vector3 endPosition = position.ToScenePosition ();
		Vector3 startPositionAbove = transform.position + Vector3.up * BoardController.ySpacing;
		Vector3 endPositionAbove = endPosition  + Vector3.up * BoardController.ySpacing;


		float moveDistance = (endPositionAbove - startPositionAbove).sqrMagnitude;
		float baseDistance = (Vector3.right * BoardController.xSpacing).sqrMagnitude;
		moveDistance = moveDistance / baseDistance;

		float progress = 0f;

		MoveLayer (+500);

		while (transform.position != startPositionAbove) {
			float distance = startPositionAbove.magnitude - startPosition.magnitude;
			transform.position = Vector3.Lerp (startPosition, startPositionAbove, progress);
			progress += speed * 4 * Time.deltaTime ;
			print (progress);
			if (progress > 1f)
				progress = 1f;

			yield return new WaitForEndOfFrame();
		}
		progress = 0f;


		while (transform.position != endPositionAbove) {
			transform.position = Vector3.MoveTowards (transform.position, endPositionAbove, .5f);
			yield return new WaitForEndOfFrame();
		}


		while (transform.position != endPosition) {
			transform.position = Vector3.Lerp (endPositionAbove, endPosition, progress);
			progress += speed * 3*Time.deltaTime ;
			print (progress);
			if (progress > 1f)
				progress = 1f;

			yield return new WaitForEndOfFrame();
		}

		while (transform.position != endPositionAbove) {
			transform.position = Vector3.Lerp ( endPosition,endPositionAbove, progress);
			progress += speed * 3*Time.deltaTime ;
			print (progress);
			if (progress > 1f)
				progress = 1f;

			yield return new WaitForEndOfFrame();
		}

		while (transform.position != startPositionAbove) {
			transform.position = Vector3.MoveTowards (transform.position, startPositionAbove, .5f);
			yield return new WaitForEndOfFrame();
		}

		while (transform.position != startPosition) {
			
			transform.position = Vector3.Lerp ( startPositionAbove,startPosition, progress);
			progress += speed * 4 * Time.deltaTime ;
			print (progress);
			if (progress > 1f)
				progress = 1f;

			yield return new WaitForEndOfFrame();
		}

		SetLayer (position);
		transform.SetParent (GameObject.Find (position.ToString ()).transform);
	}

	private void MoveLayer(int amount){
		GetComponent<SpriteRenderer> ().sortingOrder += amount;
		transform.GetChild (0).GetComponent<SpriteRenderer> ().sortingOrder += amount;
		transform.GetChild (1).GetComponent<SpriteRenderer> ().sortingOrder += amount;
	}

	private void SetLayer(Position position){
		GetComponent<SpriteRenderer> ().sortingOrder = 100 + position.GetOrderLayer();
		transform.GetChild (0).GetComponent<SpriteRenderer> ().sortingOrder = 101 + position.GetOrderLayer ();
		transform.GetChild (1).GetComponent<SpriteRenderer> ().sortingOrder = 102 + position.GetOrderLayer();
	}


}

public struct MoveAndDestroyMessage{
	public GameObject gameObject;
	public Position position;
	public MoveAndDestroyMessage (GameObject gameObject, Position position){
		this.gameObject = gameObject;
		this.position = position;
	}
}
