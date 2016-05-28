using UnityEngine;
using System.Collections;

public class MovementBehaviour  {
	Piece piece;
	Player player ;


//	public delegate BoardMap AttackableFields (Position position);
//	public delegate BoardMap HighLight1 (Position position);
//	public delegate BoardMap HighLight2(Position position);
	public MovementBehaviour(Piece piece){
		this.piece = piece;
		player = piece.GetPlayer ();
	}

	public BoardMap Highlight{get{ 
			Position position = piece.GetPosition ();
			BoardMap map = BoardMap.Empty;
			switch (piece.GetKind ()) {
			case ItemKind.KING:
				map = BoardMap.ShortSquare (position);
				break;
			case ItemKind.KNIGHT:
				map = BoardMap.ShortL (position);
				break;
			case ItemKind.ROOK:
					map = BoardMap.BlockedLineMovement (position, BoardController.GetPiecessMap(), false);
				break;
			case ItemKind.MAGE:
					map = BoardMap.BlockedLineMovement (position,BoardController.GetPiecessMap(), true);
				break;
			case ItemKind.QUEEN:
				map = BoardMap.BlockedLineMovement (position, BoardController.GetPiecessMap(), false);
				map.Add(BoardMap.BlockedLineMovement (position, BoardController.GetPiecessMap(), true));
				break;
			case ItemKind.PAWN:
				BoardMap enemiesMap = BoardController.GetEnemiesMap (piece.GetPlayer ());
				map = BoardMap.DiagonalAhead (position, player);
				map.Add ( BoardMap.DiagonalAhead (position, GetComplementarPlayer(player) ));
				map.Intersect (enemiesMap);
				BoardMap map2 = BoardMap.Ahead (position, player );
				map2.Add ( BoardMap.Ahead (position, GetComplementarPlayer(player) ));
				map2.Remove (enemiesMap);
				map.Add (map2);
				break;
			default:
				break;	
			}

			BoardMap playersPieces = BoardController.GetPiecessMap(player);
			map.Remove (playersPieces);
			return map;
		}
		}

	Player GetComplementarPlayer(Player player){
		Player p = player;
		switch (player){
		case Player.PLAYER1:
			p = Player.PLAYER4;
			break;
		case Player.PLAYER2:
			p = Player.PLAYER1;
			break;
		case Player.PLAYER3:
			p = Player.PLAYER2;
			break;
		case Player.PLAYER4:
			p = Player.PLAYER3;
			break;
		default:
			Debug.LogWarning ("Invalid Player");
			break;
		}
		return p;
	}

}