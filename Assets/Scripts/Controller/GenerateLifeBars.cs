using UnityEngine;
using System.Collections;

public class GenerateLifeBars : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		for (int i = 0; i < 31; i++) {
			GameObject go = (GameObject)Instantiate (transform.GetChild (0).gameObject, Vector3.zero, Quaternion.identity);
			go.transform.SetParent (transform);
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
