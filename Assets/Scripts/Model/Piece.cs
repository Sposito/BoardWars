using UnityEngine;
using System.Collections;

public enum ItemKind{PAWN,ROOK,KNIGHT,MAGE,KING,QUEEN,MATERIAL}
public enum Player{PLAYER1, PLAYER2, PLAYER3, PLAYER4}

public class Piece: Item  {

	private Position position;
	private ItemKind kind;
	private Player player;
	private string name;
	private int totalHp;
	private int hp;
	private int attack;
	private MovementBehaviour movementBehaviour;
	public  MovementBehaviour Movement{get{return movementBehaviour;}}
	private float quality;
	private Item core;
	private Item shell;
	private Enchantment enchantment;

	#region Getter_Setters
	public string GetName(){
		return name;
	}

	public int GetTotalHP(){
		return totalHp;
	}

	public int GetHP(){
		return hp;
	}

	public int GetAttack(){
		return attack;
	}
	public ItemKind GetKind(){
		return kind;
	}
	public void SetKind(ItemKind k){
		kind = k;
	}

	public Player GetPlayer(){
		return player;
	}
	public void SetPlayer(Player p){
		player = p;
	}

	public Position GetPosition(){
		return position;
	}

	public void SetPosition(int x, int y){
		position = new Position (x, y);
	}

	public void SetPosition(Position position){
		this.position = position;
	}
	#endregion
	public bool ReceiveHit(Piece piece){
		int totalDamage = piece.GetAttack ();

		if (totalDamage >= hp)
			return true;
		else {
			hp -= totalDamage;
			return false;
		}
	}


	public Piece(string name, ItemKind kind, Player player, int hp, int attack, Item core, Item shell, Enchantment enchantment){
		this.name = name;
		this.kind = kind;
		this.position = new Position (0, 0);
		this.player = player;
		this.totalHp = hp;
		this.hp = hp;
		this.attack = attack;
		this.core = core;
		this.shell = shell;



		this.quality = (shell.GetQuality () + core.GetQuality ()) / 2f;
		this.enchantment = enchantment;
		this.movementBehaviour = new MovementBehaviour (this);
	}

	public static Piece BuildStadardWoodPiece(ItemKind kind, Player player, int x, int y){
		string name = "Pine Wood " + kind.ToString ();
		int hp;
		int attack;

		switch (kind) {
		case ItemKind.KING:
			hp = 1;
			attack = 1;
			kind = ItemKind.KING;
				break;
		case ItemKind.KNIGHT:
			hp = 3;
			attack = 2;
			kind = ItemKind.KNIGHT;
				break;
		case ItemKind.MAGE:
			hp = 2;
			attack = 3;
			kind = ItemKind.MAGE;
			break;
		case ItemKind.ROOK:
			hp = 4;
			attack = 1;
			kind = ItemKind.ROOK;
			break;
		case ItemKind.PAWN:
			hp = 1;
			attack = 1;
			kind = ItemKind.PAWN;
			break;
		case ItemKind.QUEEN:
			hp = 3;
			attack = 3;
			kind = ItemKind.QUEEN;
			break;

		default:
			hp = 1;
			attack = 1;
			Debug.LogError ("Invalid Kind");
			break;
		}

		Piece piece = new Piece(name, kind,player,hp,attack,Item.PineWood(),Item.PineWood(), new Enchantment() );
		piece.SetPosition (x, y);
		return piece;
	}

//	private void SetStadardMovement(){
//		switch (kind) {
//		case ItemKind.KING:
//			HighlightMap (position.X, position.Y, BoardMap.ShortSquare (position));
//			break;
//		case ItemKind.KNIGHT:
//			HighlightMap (position.X, position.Y, BoardMap.ShortL (position));
//			break;
//		case ItemKind.ROOK:
//			HighlightMap (position.X, position.Y, BoardMap.BlockedLineMovement (position));
//			break;
//		case ItemKind.MAGE:
//			break;
//		case ItemKind.QUEEN:
//			break;
//		case ItemKind.PAWN:
//			break;
//		default:
//			break;	
//		}
//	}


}