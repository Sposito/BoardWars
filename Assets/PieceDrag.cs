using UnityEngine;
using System.Collections;

public class PieceDrag : MonoBehaviour {
	
	SpriteRenderer sr;
	void Start () {
		print (Physics.queriesHitTriggers);
	}
	
	// Update is called once per frame
	void OnMouseDrag () {
		
		Vector3 v3 = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		v3.z = 0;
		transform.position = v3;
		}





}