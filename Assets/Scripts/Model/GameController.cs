using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class GameController  {
	public readonly static float xSpacing = 2.07f;
	public readonly static float ySpacing = 1.69f;
	[SerializeField]
	private  GameState gameState;

	public GameController(){
		AddGameState(InitialStateBuilder.CreateInitialGameState () );
	}

//	public int TryToUpdate(GameState gameState){
//		// check if movement is valid
//	}

	public void AddGameState(GameState gameState){
		this.gameState = gameState;
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
	public string GetStringStates(){
		string result = "";

		result += gameState.ToUID() + "\n";
		result += gameState.ToString() + "\n";

		return result;
	}
}
