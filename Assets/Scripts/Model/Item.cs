using UnityEngine;
using System.Collections;
//ELEMENTS: fire ☲🜂㊋ water ☵🜄㊌  earth ☷🜃㊏ metal㊎ wood㊍
public enum Elements{WATER,FIRE,WOOD,METAL,EARTH}
public class Item  {

	private float quality;
	private Elements element;
	private string name;

	public Item (string name, Elements element, float quality){
		this.name = name;
		this.element = element;
		this.quality = quality;
	}

	public Item(){
	}

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
		

	//TODO MOCKUP FOR ITENS UNTIL SERVER IS UP AND RUNNING
	public static Item PineWood(){
		return new Item ("Pine Wood", Elements.WOOD, .5f);
	}

	public static Item Brass(){
		return new Item ("Brass", Elements.METAL, .5f);
	}
}
