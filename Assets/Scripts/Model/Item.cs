using UnityEngine;
using System.Collections;
public enum Elements{WATER,FIRE,WOOD,METAL,EARTH}
public class Item  {

	private float quality;
	private Elements element;

	protected void SetQuality (float quality){
		this.quality = quality;
	}
	public float GetQuality(){
		return quality;
	}

	public Elements GetElement(){
		return  element;
	}

	public void SetElement(Elements element){
		this.element = element;
	}
}
