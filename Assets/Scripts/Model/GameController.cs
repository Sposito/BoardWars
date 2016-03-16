using UnityEngine;
using System.Collections;
using System;
public class GameController  {
	public readonly static float xSpacing = 2.07f;
	public readonly static float ySpacing = 1.69f;

	private GameState[] gameStates;


	public GameController(){
		AddGameState(InitialStateBuilder.CreateInitialGameState () );
	}

//	public int TryToUpdate(GameState gameState){
//		// check if movement is valid
//	}

	private void AddGameState(GameState gameState){
		int lenght = (gameStates == null) ? 0 : gameStates.Length;
		GameState[] temporaryGameStates = new GameState[lenght + 1];
		for (int i = 0; i < lenght; i++) {
			temporaryGameStates [i] = gameStates [i];
		}
		temporaryGameStates [temporaryGameStates.Length - 1] = gameState;


		gameStates = temporaryGameStates;

	}

	public GameState GetCurrentState(){
		return gameStates [gameStates.Length - 1];
	}

	public Piece GetPiecebyPos(Position position){
		return GetCurrentState ().GetPiecebyPosition (position);
	}
}
