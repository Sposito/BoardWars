using UnityEngine;
using System.Collections;

public class GameState  {

	private Piece[] pieces;
	private Player currentPlayer;
	private float time;

	private Position actorPosition;
	private Position targetPosition; 
	public GameState(){ // provisory contructor
	
	}
	public GameState(Piece[] pieces, Player currentPlayer, float time, Position actorPosition, Position targetPosition){
		this.pieces = pieces;
		this.currentPlayer = currentPlayer;
		this.time = time;

		this.actorPosition = actorPosition;
		this.targetPosition = targetPosition;
	}


	public Piece[] GetPieces(){
		return pieces;
	}
	public float GetTime(){
		return time;
	}

	public Player GetCurrentPlayer(){
		return currentPlayer;
	}
}
