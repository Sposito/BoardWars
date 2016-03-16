using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameState : IEnumerable<Piece> {

	private Piece[] pieces;
	Board board;
	private Player currentPlayer;
	private float time;


	private Position actorPosition;
	private Position targetPosition; 
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
}
