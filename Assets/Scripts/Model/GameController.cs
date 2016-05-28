using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum Team{A, B,C,D};
public class GameController  {
	public readonly static float xSpacing = 2.07f;
	public readonly static float ySpacing = 1.69f;
	[SerializeField]
	private  GameState gameState;
	private int turnCounter = 0;
	public int TotalTurns{get {return turnCounter;}}
	public GameController(){
		AddGameState(InitialStateBuilder.CreateInitialGameState () );
	}

//	public int TryToUpdate(GameState gameState){
//		// check if movement is valid
//	}

	public void AddGameState(GameState gameState){
		this.gameState = gameState;
		turnCounter++;
	}

	public GameState GetCurrentState(){
	//	return gameStates [gameStates.Length - 1];
		return gameState;
	}

	public Piece GetPiecebyPos(Position position){
		return GetCurrentState ().GetPiecebyPosition (position);
	}

	public Player GetCurrentPlayer(){
		return GetCurrentState ().GetCurrentPlayer ();
	}



	public string GetGameJSON(){
		return JsonUtility.ToJson (gameState, true);
	}

	public bool CheckIfIsSamePlayer(Position a, Position b){
		return (GetPiecebyPos (a).GetPlayer () == GetPiecebyPos (b).GetPlayer () );
	}

	public EoGMessage TestForEndOfGame(){
		ActivePlayers active = gameState.GetActivePlayers();

		if (!active.isP1onGame && !active.isP3onGame)
			return new EoGMessage (true, Team.B);

		if(!active.isP2onGame && !active.isP4onGame)
			return new EoGMessage (true, Team.A);

		return new EoGMessage(false, Team.D);
	}

	public string GetStringStates(){
		string result = "";

		result += gameState.ToUID() + "\n";
		result += gameState.ToString() + "\n";

		return result;
	}
}

public struct EoGMessage{
	public readonly bool gameOver;
	public readonly Team victoriousTeam;

	public EoGMessage (bool gameOver, Team victoriousTeam){
		this.gameOver = gameOver;
		this.victoriousTeam = victoriousTeam;
	}
}
