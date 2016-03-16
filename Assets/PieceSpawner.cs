using UnityEngine;
using System.Collections;

public class PieceSpawner : MonoBehaviour {

	public GameObject basePiece ;
	void Start () {
		//GameState initial = new GameState (InitialStateBuilder.HardCodedBuild (), Player.PLAYER1, 0f, Position.zero, Position.zero);
		Piece[] pieces = InitialStateBuilder.HardCodedBuild ();

		BoardMap map = new BoardMap (false);

//		for (int i = 0; i < pieces.Length; i++)
//			print (pieces [i].GetPosition ().ToString ());
	}
	// Update is called once per frame
	void Update () {
	
	}
}
