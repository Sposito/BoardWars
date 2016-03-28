using UnityEngine;
using System.Collections;

public class TileCloudBehaviour : MonoBehaviour {
	[Range( .8f,1f)]
	float cycle = 0f;
	float linearcounter;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		linearcounter += Time.deltaTime;
		cycle = 1 - (Mathf.Sin (linearcounter) * 0.02f);

		transform.localScale = Vector3.one * cycle * 5;
	}
}
