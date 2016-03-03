using UnityEngine;
using System.Collections;

public class PieceBehaviour : MonoBehaviour {
	public enum Player {Player1,Player2,Player3,Player4}
	public Player player;
	public Piece piece;
	public PieceKind pieceKind;
	public Color baseColor = Color.white;
	public Color detailColor = Color.white;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetPieceKind(PieceKind kind){
		pieceKind = kind;
	}
}
