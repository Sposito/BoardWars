using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class GameController  {
	public readonly static float xSpacing = 2.07f;
	public readonly static float ySpacing = 1.69f;
	[SerializeField]
	private  List<GameState> gameStates;

	public GameController(){
		gameStates = new List<GameState> ();
		AddGameState(InitialStateBuilder.CreateInitialGameState () );
	}

//	public int TryToUpdate(GameState gameState){
//		// check if movement is valid
//	}

	public void AddGameState(GameState gameState){
//		int lenght = (gameStates == null) ? 0 : gameStates.Length;
//		GameState[] temporaryGameStates = new GameState[lenght + 1];
//		for (int i = 0; i < lenght; i++) {
//			temporaryGameStates [i] = gameStates [i];
//		}
//		temporaryGameStates [temporaryGameStates.Length - 1] = gameState;
//
//
//		gameStates = temporaryGameStates;

		gameStates.Add(gameState);
	}

	public GameState GetCurrentState(){
	//	return gameStates [gameStates.Length - 1];
		return gameStates[gameStates.Count -1];
	}

	public Piece GetPiecebyPos(Position position){
		return GetCurrentState ().GetPiecebyPosition (position);
	}

	public Player GetCurrentPlayer(){
		return GetCurrentState ().GetCurrentPlayer ();
	}



	public string GetGameJSON(){
		return JsonUtility.ToJson (gameStates.ToArray(), true);
	}

	public bool CheckIfIsSamePlayer(Position a, Position b){
		return (GetPiecebyPos (a).GetPlayer () == GetPiecebyPos (b).GetPlayer () );
	}
	public string GetStringStates(){
		string result = "";
		foreach (GameState s in gameStates) {
			result += s.ToUID() + "\n";
			result += s.ToString() + "\n";
		}
		return result;
	}
}
