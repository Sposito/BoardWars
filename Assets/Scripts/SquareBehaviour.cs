using UnityEngine;
using System.Collections;

public class SquareBehaviour : MonoBehaviour {
	SpriteRenderer spriteRenderer;
	Color baseColor;
	public Color brightColor;

	public float transitionSpeed;
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerable BrightOn(){
		
		float t = 0;
		while (t <= 1) {
			t += transitionSpeed * Time.deltaTime;
			spriteRenderer.color = Color.Lerp (baseColor, brightColor, t);
			yield return new WaitForEndOfFrame ();
		}
	}
}
