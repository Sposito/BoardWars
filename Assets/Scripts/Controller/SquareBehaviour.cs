using UnityEngine;
using System.Collections;

public class SquareBehaviour : MonoBehaviour {
	SpriteRenderer spriteRenderer;
	Color baseColor;
	public Color brightColor;
	public Position position;

	public float transitionSpeed = 1f;
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		baseColor = spriteRenderer.color;
		MakeBrighter ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void MakeBrighter(){
		float h = 0f; float s = 0f; float v = 0f;
		Color.RGBToHSV (spriteRenderer.color, out h, out s,out  v);
		v += .2f;

		brightColor = Color.HSVToRGB (h, s, v);

	}

	public void UnSelect(){
		if(spriteRenderer != null) //Avoid null reference errors if spriteRender is not initialized
			spriteRenderer.color = baseColor;
		if (transform.childCount > 0) 
			transform.GetChild (0).GetComponent<PieceBehaviour> ().SetStroke (false);
	}

	public void Select(){
		if(spriteRenderer != null) //Avoid null reference errors if spriteRender is not initialized
			spriteRenderer.color = GeneralTools.Hex.ToColor("F7FE82");
		if (transform.childCount > 0) 
			print ("HAS PIECE");//transform.GetChild (0).GetComponent<PieceBehaviour> ().SetStroke (true);

	}

	public void Select(Color color){
		if(spriteRenderer != null) //Avoid null reference errors if spriteRender is not initialized
			spriteRenderer.color = color;
		if (transform.childCount > 0) 
			print ("HAS PIECE");//transform.GetChild (0).GetComponent<PieceBehaviour> ().SetStroke (true);
		
	}

	void OnMouseEnter(){
		if (BoardController.isGameRunning) {
			BoardController.SetHighlightSquare (position);
			if (transform.childCount > 0) 
				transform.GetChild (0).GetComponent<PieceBehaviour> ().SetStroke (true);
		}


	}



	void OnMouseDown(){
		#if UNITY_IPHONE
		OnMouseEnter();
		#endif

		if(BoardController.isGameRunning)
			BoardController.Click (position);
	}

	IEnumerator BrightOn(){

		float t = 0;
		while (t <= 1) {
			t += transitionSpeed * Time.deltaTime;
			spriteRenderer.color = Color.Lerp (baseColor, Color.magenta, t);

			yield return new WaitForEndOfFrame ();
		}
	}

	IEnumerator BrightOff(){
		float t = 0;
		while (t <= 1) {
			t += transitionSpeed * Time.deltaTime;
			spriteRenderer.color = Color.Lerp (brightColor, baseColor, t);
			yield return new WaitForEndOfFrame ();
		}
	}


}
