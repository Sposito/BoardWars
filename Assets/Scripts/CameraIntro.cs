using UnityEngine;
using System.Collections;

public class CameraIntro : MonoBehaviour {

	public Vector3 topPos;
	public Vector3 bottomPos;
	public Vector3 middlePos;

	public float speed = 5f;
	void Start () {
		StartCoroutine ("Move");
	}
	
	IEnumerator Move(){
		transform.position = bottomPos;
		Vector3 pos = transform.position;
		while (transform.position.y < topPos.y) {
			pos.y += speed * Time.deltaTime;
			transform.position = pos;
			yield return new WaitForEndOfFrame ();
		}


		while (transform.position.y > middlePos.y) {
			pos.y -= speed * Time.deltaTime;
			transform.position = pos;
			yield return new WaitForEndOfFrame ();
		}
		transform.position = middlePos;

	}
}
