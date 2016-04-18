using UnityEngine;
using System.Collections;
using GeneralTools;
//ELEMENTS: fire ☲🜂㊋ water ☵🜄㊌  earth ☷🜃㊏ metal㊎ wood㊍
public enum Elements{WATER,FIRE,WOOD,METAL,EARTH} //㊌㊋㊍㊎㊏

public class Element  {
	public static Color GetColor(Elements elements){
		switch (elements) {
		case Elements.WATER:
			return Hex.ToColor ("A7DBD9");
		case Elements.FIRE:
			return Hex.ToColor ("F1916E");
		case Elements.WOOD:
			return Hex.ToColor ("99B075");
		case Elements.METAL:
			return Hex.ToColor ("C3C2BE");
		case Elements.EARTH:
			return Hex.ToColor ("5D371D");
		default:
			return Color.grey;
		}

	}
	public static string GetSymbol(Elements elements){
		switch (elements) {
		case Elements.WATER:
			return "㊌";
		case Elements.FIRE:
			return "㊋";
		case Elements.WOOD:
			return "㊍";
		case Elements.METAL:
			return "㊎";
		case Elements.EARTH:
			return "㊏";
		default:
			return "error";
		}

	}

}
