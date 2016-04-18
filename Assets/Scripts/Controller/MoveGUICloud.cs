using UnityEngine;
using System.Collections;

public class MoveGUICloud : MonoBehaviour {
	Vector3 size;
	float pingPong = 0;
	Vector3 v3;
	// Use this for initialization
	void Start () {
		v3 = new Vector3 (Random.value, Random.value, 0f).normalized;
		size = transform.localScale;
		transform.localScale = transform.localScale * Random.Range (.9f, 1.1f);
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position += v3 * Time.deltaTime * .5f;
	}
}
