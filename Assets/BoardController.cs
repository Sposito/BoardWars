using UnityEngine;
using System.Collections;

public class BoardController : MonoBehaviour {
	static SquareBehaviour[] squares;
	static BoardMap map;

	void Update(){
		if(Input.GetKeyUp(KeyCode.Space)){
			
			SetMap(5,5);
		}
			
	}

	public void SetSquareBehaviours(SquareBehaviour[] squareBehaviours){
		squares = squareBehaviours;
	
	}

	public static void SetMap(int x, int y){
		map = BoardMap.DiagonalAhead(x,y, Directions.S);
		for(int i = 0; i < 8; i++){
			for(int j = 0; j < 8; j++){
				if (map.GetTile(i,j))
					squares[j + (i * 8)].Select();
				else
					squares[j + (i * 8)].UnSelect();
					
			}
		}
	}

}
