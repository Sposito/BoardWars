using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StatusPannelController : MonoBehaviour {
	Text nameText;
	Text attackText;
	Text defenseText;
	Image elementCoreIcon;
	Image elementShellIcon;
	Text elementCoreText;
	Text elementShellText;
	Text currentPlayer;
	Image playerBanner;
	// Use this for initialization
	void Start () {
		nameText = transform.FindChild ("Name").GetComponent<Text> ();
		attackText = transform.FindChild ("AttackText").GetComponent<Text> ();
		defenseText = transform.Find ("DefenseText").GetComponent<Text> ();
		elementCoreIcon = transform.FindChild ("Element Atk").GetComponent<Image> ();
		elementShellIcon = transform.FindChild ("Element Def").GetComponent<Image> ();
		elementCoreText = transform.FindChild ("Core Text").GetComponent<Text> ();
		elementShellText = transform.FindChild ("Shell Text").GetComponent<Text> ();
		currentPlayer = GameObject.Find("CurrentPlayer").GetComponent<Text> ();
		playerBanner = GameObject.Find("PlayerBanner").GetComponent<Image> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (BoardController.currentPiece != null) {
			nameText.text = BoardController.currentPiece.GetName ();
			attackText.text = "x" + BoardController.currentPiece.GetAttack ();
			defenseText.text = "x" + BoardController.currentPiece.GetTotalHP ();
			elementCoreIcon.color = Element.GetColor (BoardController.currentPiece.GetCoreElement());
			elementShellIcon.color = Element.GetColor (BoardController.currentPiece.GetShellElement());
			elementCoreText.text = Element.GetSymbol(BoardController.currentPiece.GetCoreElement());
			elementShellText.text = Element.GetSymbol(BoardController.currentPiece.GetShellElement());

		}
		currentPlayer.text = BoardController.GetCurrentPlayer ().ToString ();
		playerBanner.color = PieceBehaviour.playersColor[(int)BoardController.GetCurrentPlayer ()];
	}
}
