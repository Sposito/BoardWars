using UnityEngine;
using System.Collections;
using UnityEditor;
using GeneralTools;

public class BWSettings : EditorWindow {
	public static  Color playerBaseColor = Color.white;
	[MenuItem("BoardWars/Settings")]
	static void Init(){
		BWSettings window = (BWSettings)EditorWindow.GetWindow( typeof(BWSettings));
			window.Show();
	}

	void OnGUI(){
		playerBaseColor = (EditorGUILayout.ColorField ("Base Color", playerBaseColor));
		PlayersColor.player1 = playerBaseColor;

		if (GUILayout.Button ("Update"))
			PlayersColor.Update ();
		
			
	}
}

static class PlayersColor{
	
	public static Color player1 = Hex.ToColor("69B89F");
	public static Color player2 = Hex.ToColor("B5677E");
	public static Color player3 = Hex.ToColor("B59C67");
	public static Color player4 = Hex.ToColor("677EB5");

	public static Color GetColorByPlayer(PieceBehaviour.Player p){
		switch (p) {
		case PieceBehaviour.Player.Player1:
			return player1;
		case PieceBehaviour.Player.Player2:
			return player2;
		case PieceBehaviour.Player.Player3:
			return player3;
		case PieceBehaviour.Player.Player4:
			return player4;
		default:
			Debug.Log ("Unknow enum");
			return Color.magenta;
		}
	}

	public static void Update(){
		
		player1 = BWSettings.playerBaseColor;


		float h, s, v;

		Color.RGBToHSV (player1, out h, out s, out v);
		h += 0.25f;
		player2 = Color.HSVToRGB (h, s, v);
		h += 0.25f;
		player3 = Color.HSVToRGB (h, s, v);
		h += 0.25f;
		player4 = Color.HSVToRGB (h, s, v);
	}
}
