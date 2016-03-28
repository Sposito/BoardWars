using UnityEngine;
using System.Collections;

public class SpriteOnStageCheker : MonoBehaviour {

	public Vector3 minViweableWorldPosition;
	public Vector3 maxViweableWorldPosition;

	SpriteRenderer spriteRenderer ;


	public bool isOnStage;
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		SetMinMax ();

	}

	// Update is called once per frame
	void Update () {
		isOnStage = IsOnStage ();
	}

	void SetMinMax(){
		minViweableWorldPosition = Camera.main.ScreenToWorldPoint (Vector3.zero);
		maxViweableWorldPosition = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height));
	}

	void StepOutofStage(){
		Vector3 WorldCenterPosition = (minViweableWorldPosition + maxViweableWorldPosition) / 2f;

	}

	protected bool IsOnStage(){
		float x = spriteRenderer.sprite.rect.width / spriteRenderer.sprite.pixelsPerUnit /2 * transform.lossyScale.x;
		float y = spriteRenderer.sprite.rect.height / spriteRenderer.sprite.pixelsPerUnit/2 * transform.lossyScale.y;

		return (
			transform.position.x - x < maxViweableWorldPosition.x &&
			transform.position.x + x > minViweableWorldPosition.x &&
			transform.position.y - y < maxViweableWorldPosition.y &&
			transform.position.y + y > minViweableWorldPosition.y);
	}

}
