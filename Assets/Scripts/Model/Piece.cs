using UnityEngine;
using System.Collections;

public enum PieceKind{PAWN,ROOK,KNIGHT,MAGE,KING,QUEEN}
public enum Player{PLAYER1, PLAYER2, PLAYER3, PLAYER4}

public class Piece  {

	private Position position;
	private PieceKind kind;
	private Player player;
	private int totalHp;
	private int hp;
	private int attack;
	private MovementBehaviour movementBehaviour;
	private float quality;
	private Item core;
	private Item shell;
	private Enchantment enchantment;

	#region Getter_Setters
	public PieceKind GetKind(){
		return kind;
	}
	public void SetKind(PieceKind k){
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

	public Piece(PieceKind kind, Player player, int hp, int attack, Item core, Item shell, Enchantment enchantment){
		this.kind = kind;
		this.position = new Position (0, 0);
		this.player = player;
		this.totalHp = hp;
		this.hp = hp;
		this.attack = attack;
		this.core = core;
		this.shell = shell;

		this.movementBehaviour = MovementBehaviourFactory.Generate ();

		this.quality = (shell.GetQuality () + core.GetQuality ()) / 2f;
		this.enchantment = enchantment;

	}


}