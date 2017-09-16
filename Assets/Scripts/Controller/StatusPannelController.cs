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
		nameText = transform.Find ("Name").GetComponent<Text> ();
		attackText = transform.Find ("AttackText").GetComponent<Text> ();
		defenseText = transform.Find ("DefenseText").GetComponent<Text> ();
		elementCoreIcon = transform.Find ("Element Atk").GetComponent<Image> ();
		elementShellIcon = transform.Find ("Element Def").GetComponent<Image> ();
		elementCoreText = transform.Find ("Core Text").GetComponent<Text> ();
		elementShellText = transform.Find ("Shell Text").GetComponent<Text> ();
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
