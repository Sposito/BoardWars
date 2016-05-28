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

	protected bool isTeamGame = true;
	protected Team currentTeam = Team.A;

	public static int  skipcounter = 2;

	public IEnumerator<Piece> GetEnumerator(){
		List<Piece> list = new List<Piece> (pieces);
		return list.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator(){
		return GetEnumerator ();
	}

	public GameState(Piece[] pieces, Player currentPlayer,Team currentTeam, float time, Position actorPosition, Position targetPosition){
		this.pieces = pieces;
		this.currentPlayer = currentPlayer;
		this.currentTeam = currentTeam;
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
		this.currentTeam = g.currentTeam;
		this.time = g.time;

		this.actorPosition = g.actorPosition;
		this.targetPosition = g.targetPosition;

		this.activePlayers = g.activePlayers;

		board = new Board (pieces);
	}

	public ActivePlayers GetActivePlayers(){
		return new ActivePlayers (activePlayers);
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
		if (!isTeamGame) {
			Next ();
		} 

		else {
			NextTeam ();

		}
		
	}

	void SimpleNext(){
		if ((int)currentPlayer < 3)
			currentPlayer++;
		else
			currentPlayer = Player.PLAYER1;

	}

	void Next(){
		if ((int)currentPlayer < 3)
			currentPlayer++;
		else
			currentPlayer = Player.PLAYER1;

		if (!activePlayers [(int)currentPlayer]) {
			NextPlayer ();
		}
	}
	void NextTeam(){

		if ((int)currentPlayer < 3)
			currentPlayer++;
		else
			currentPlayer = Player.PLAYER1;

		if (!activePlayers [(int)currentPlayer]) {
			SimpleNext ();
			SimpleNext ();
		}

	}



	private Team CheckPlayersTeam(Player player){
		return ((int)player % 2 == 0) ? Team.A : Team.B; 
	} 
	private bool IsConsecutiveTeam(Player last, Player current ){
		return ((int)last + (int)current)   % 2 == 0;
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

public struct ActivePlayers{
	public readonly bool isP1onGame;
	public readonly bool isP2onGame;
	public readonly bool isP3onGame;
	public readonly bool isP4onGame;


	public ActivePlayers (bool isP1onGame, bool isP2onGame, bool isP3onGame, bool isP4onGame){
		this.isP1onGame = isP1onGame;
		this.isP2onGame = isP2onGame;
		this.isP3onGame = isP3onGame;
		this.isP4onGame = isP4onGame;
	}

	public ActivePlayers (bool[] activePlayers){
		this.isP1onGame = activePlayers[0];
		this.isP2onGame = activePlayers[1];
		this.isP3onGame = activePlayers[2];
		this.isP4onGame = activePlayers[3];
	}
	
}