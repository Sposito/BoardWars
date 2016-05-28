using UnityEngine;
using System.Collections;
using GeneralTools;

public class Item  {

	private float quality;
	private Elements element;
	private string name;
	public readonly Color color = Color.magenta;

	public Item (string name, Elements element, float quality){
		this.name = name;
		this.element = element;
		this.quality = quality;
	}

	public Item (string name, Elements element, float quality, Color color){
		this.name = name;
		this.element = element;
		this.quality = quality;
		this.color = color;
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
		return new Item ("Pine Wood", Elements.WOOD, .5f, Hex.ToColor("F0D97D") );
	}

	public static Item Brass(){
		return new Item ("Brass", Elements.METAL, .5f, Hex.ToColor("BFA743"));
	}

	public static Item Ice(){
		return new Item ("Ice", Elements.WATER, .5f, Hex.ToColor("99C5E2"));
	}

	public static Item GreenWax(){
		return new Item ("Green Wax", Elements.WOOD, .5f, Hex.ToColor("BCE299"));
	}

	public static Item RedWax(){
		return new Item ("Red Wax", Elements.FIRE, .5f,Hex.ToColor("F68B8E"));
	}

	public static Item BeeWax(){
		return new Item ("Bee Wax", Elements.EARTH, .5f,Hex.ToColor("E2DA99"));
	}

	public static Item BlackSteel(){
		return new Item ("Black Steel", Elements.METAL, .5f,Hex.ToColor("3F4345"));
	}
}
