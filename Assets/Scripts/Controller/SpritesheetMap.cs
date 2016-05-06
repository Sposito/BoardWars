using UnityEngine;
using System;

public class SpritesheetMap{
	private Sprite[] sprites;
	private string[] names;

	public SpritesheetMap(string pathToResource){
		sprites = Resources.LoadAll<Sprite> (pathToResource);
		names = new string[sprites.Length];

		for (int i = 0; i < sprites.Length; i++) 
			names [i] = sprites [i].name;
	}

	/// <summary>Returns the sprite  associated with the given name </summary>
	public Sprite GetByName(string name){
		return sprites [Array.IndexOf (names, name)];
	}
}