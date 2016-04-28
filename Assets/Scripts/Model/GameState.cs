using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameState : IEnumerable<Piece> {

	protected Piece[] pieces;
	protected Board board;
	protected Player currentPlayer;
	protected float time;


	protected Position actorPosition;
	private Position targetPosition; 

	protected bool[] activePlayers;
	public GameState(){ // provisory contructor
	
	}

	public IEnumerator<Piece> GetEnumerator(){
		List<Piece> list = new List<Piece> (pieces);
		return list.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator(){
		return GetEnumerator ();
	}

	public GameState(Piece[] pieces, Player currentPlayer, float time, Position actorPosition, Position targetPosition){
		this.pieces = pieces;
		this.currentPlayer = currentPlayer;
		this.time = time;

		this.actorPosition = actorPosition;
		this.targetPosition = targetPosition;

		//Initialize activeplayers vector
		this.activePlayers = new bool[4];
		for (int i = 0; i < activePlayers.Length; i++)
			this.activePlayers [i] = true;

		board = new Board (pieces);
	}
	public GameState(GameState g){
		this.pieces = g.pieces;
		this.currentPlayer = g.currentPlayer;
		this.time = g.time;

		this.actorPosition = g.actorPosition;
		this.targetPosition = g.targetPosition;

		this.activePlayers = g.activePlayers;

		board = new Board (pieces);
	}




	public Piece[] GetPieces(){
		return pieces;
	}

	public Piece[] GetPieces(Player player){
		List<Piece> playerPieces = new List<Piece> ();
		foreach (Piece p in pieces) {
			if (p.GetPlayer () == player)
				playerPieces.Add (p);
		}
		return playerPieces.ToArray ();
	}

	public float GetTime(){
		return time;
	}

	public Player GetCurrentPlayer(){
		return currentPlayer;
	}

	public Piece GetPiecebyPosition(Position pos){
		return board.GetPiece (pos);
	}

	public void NextPlayer(){ //TODO: WARNING!!! IF FOR SOME REASON ALL THE KINGS DIE AT ONCE IT WILL CRASH in an infinite loop!!!
		if ((int)currentPlayer < 3)
			currentPlayer++;
		else
			currentPlayer = Player.PLAYER1;

		if (!activePlayers [(int)currentPlayer]) {
			NextPlayer ();
		}
	}

	public void MovePiece(Position oldPosition, Position newPosition, bool destroy){
		Piece targetPiece = board.GetPiece (newPosition);

		Piece piece = board.GetPiece (oldPosition);
		piece.SetPosition (newPosition);
		board.SetPiece (newPosition, piece);
		board.RemovePiece (oldPosition);

		pieces = board.GetPieces ();

		if (destroy) {
			ItemKind targetKind = targetPiece.GetKind ();

			if (targetKind == ItemKind.KING)
				RemovePlayer (targetPiece.GetPlayer() );
		}
	}
	private void RemovePiece(Position position){
		Piece piece = board.GetPiece (position);


		board.RemovePiece (position);
		pieces = board.GetPieces ();

		if (piece.GetKind () == ItemKind.KING)
			RemovePlayer (piece.GetPlayer() );
	}

	private void RemovePlayer(Player player){
		List<Piece> tempPieces = new List<Piece> ();
		foreach (Piece p in pieces) {
			if (p.GetPlayer () != player)
				tempPieces.Add (p);
		}

		activePlayers [(int)player] = false;
		pieces = tempPieces.ToArray ();
		board = new Board (pieces);

	}
		
	public override string ToString(){
		string result = "";

		for (int i = 0; i < 4; i++) {
			if (activePlayers [i])
				result += "Player " + (i + 1) + " active   | ";
		}
		result += "\n";
		foreach (Piece p in pieces) {
			result += p.GetPlayer().ToString() + " " + p.GetName() + " : " + p.GetPosition().ToString() + "\n";
		}

		return result;
	}

	public int ToUID (){
		return GetHashCode();
	}

}