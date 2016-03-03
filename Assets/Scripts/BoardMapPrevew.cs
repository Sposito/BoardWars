using UnityEngine;
using System.Collections;

public class BoardMapPrevew : MonoBehaviour {

	void Start(){
		print(BoardMap.DiagonalCross (5, 4).ToString ());
	}
}
