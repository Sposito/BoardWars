using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class UIController : MonoBehaviour {
	public GameObject endOfGamePannelGO;
	public Text teamVictoryMessage;
	public Text endOfGameTimeCounter;
	public Text endOfGameTurnCounter;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GameOver (Team team){
		teamVictoryMessage.text = "Team " + team.ToString() + " Victory!";
		endOfGamePannelGO.SetActive (true);
		endOfGameTimeCounter.text = (int)BoardController.timeClock + " seconds";
		endOfGameTurnCounter.text = BoardController.GetCurrentTurn () + "";
	}
}
