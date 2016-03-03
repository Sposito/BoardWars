using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
[CustomEditor (typeof(PieceBehaviour))]
public class PieceEditor : Editor {
	Sprite[] sprites;
	string[] names;





	public override void OnInspectorGUI (){
		PieceBehaviour pieceBehaviour = (PieceBehaviour)target;

		if (sprites == null)
			LoadSprites ();
		
		if (DrawDefaultInspector() ) {
			string baseName = pieceBehaviour.pieceKind.ToString() + "_Base";
			string detailName = pieceBehaviour.pieceKind.ToString() + "_Detail";

			pieceBehaviour.transform.GetChild(0).GetComponent<SpriteRenderer> ().sprite = sprites[Array.IndexOf (names, baseName)];
			pieceBehaviour.transform.GetChild (0).GetComponent<SpriteRenderer> ().color = pieceBehaviour.baseColor;
			pieceBehaviour.transform.GetChild(1).GetComponent<SpriteRenderer> ().sprite = sprites[Array.IndexOf (names, detailName)];
			pieceBehaviour.transform.GetChild (1).GetComponent<SpriteRenderer> ().color = pieceBehaviour.detailColor;

			pieceBehaviour.GetComponent<SpriteRenderer> ().color = PlayersColor.GetColorByPlayer (pieceBehaviour.player);

//		

		//	pieceBehaviour.gameObject.GetComponent<SpriteRenderer> ().sprite = sprites[Array.IndexOf (names, "Pawn_Base")];
			//pieceBehaviour.gameObject.GetComponent<SpriteRenderer> ().color = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
		}
		if(GUILayout.Button("Load Sprites")){
			LoadSprites ();
		}
	}

	private void LoadSprites(){
		sprites = Resources.LoadAll<Sprite> ("Sprites/pieces");
		names = new string[sprites.Length];
				
		for (int i = 0; i < sprites.Length; i++) 
			names [i] = sprites [i].name;
	}

}

