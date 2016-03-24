using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIController : MonoBehaviour {

	static List<GameObject> GUIs;
	
	// Update is called once per frame

	public static void BuildInfo(){
		Piece[] pieces = BoardController.GetCurrentState ().GetPieces ();

		foreach (Piece p in pieces) {
			GUIs.Add( (GameObject)Instantiate(GameObject.Find("LifeBar"),p.GetPosition().ToScenePosition(),Quaternion.identity));
		}
	}
	void Update () {
	
	}
}
