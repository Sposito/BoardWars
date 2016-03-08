using UnityEngine;
using System.Collections;

public class GameController  {

	private GameState[] gameStates;


	public GameController(){
		
	}

//	public int TryToUpdate(GameState gameState){
//		// check if movement is valid
//	}

	private void AddGameState(GameState gameState){
		GameState[] temporaryGameStates = new GameState[gameStates.Length + 1];
		for (int i = 0; i < gameStates.Length; i++) {
			temporaryGameStates [i] = gameStates [i];
		}
		temporaryGameStates [temporaryGameStates.Length - 1] = gameState;

		gameStates = temporaryGameStates;
	}
}
