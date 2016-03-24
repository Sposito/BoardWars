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
			return BoardController.GetCurrentState ().GetPiecebyPosition (position);
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

	public void Move(Position position){
		transform.position = position.ToScenePosition ();
		transform.SetParent (GameObject.Find (position.ToString ()).transform);
	}
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
}
