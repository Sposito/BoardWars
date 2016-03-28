using UnityEngine;
using System.Collections;

public class CloudBehaviour : MonoBehaviour {

	public Vector2 speed;
	public bool customScale = false;


	void Start () {
		Rn ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (speed);
	}

	void Rn(){
		speed = new Vector3 (Random.Range (0f, 1f), Random.Range (0f, 1f), 0f);
		speed.Normalize ();
		speed *= .001f;
		if(!customScale)
			transform.localScale = Random.Range (0.9f, 1.4f) * Vector3.one;
		
	}


	void RespawnCloud(){
		
	}
}
