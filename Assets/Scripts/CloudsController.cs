using UnityEngine;
using System.Collections;

public class CloudsController : MonoBehaviour {

	public GameObject[] clouds;

	public float density = 1f;
	void Start () {
		AddClouds ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void AddClouds(){
		Vector3 maxPosition = Camera.main.ScreenToWorldPoint (new Vector3( (float)Screen.width, (float)Screen.height, 0f) );
		Vector3 minPosition = Camera.main.ScreenToWorldPoint (Vector3.zero);

		float aspectRatio = (float)Screen.height / (float)Screen.width;

		int xSteps = (int)(density * 10);
		int ySteps = (int)(density * 10 * aspectRatio);

		for (int i = 0; i <= xSteps; i++) {
			for (int j = 0; j <= ySteps; j++) {
				float x = Mathf.Lerp (minPosition.x, maxPosition.x, (float)i / xSteps);
				float y = Mathf.Lerp (minPosition.y, maxPosition.y, (float)j / ySteps);

				GameObject cloud =(GameObject)Instantiate (clouds[Random.Range(0,3)], new Vector3 (x, y), Quaternion.identity);
				cloud.GetComponent<CloudBehaviour> ().customScale = true;
				cloud.transform.localScale = Vector3.one * Random.Range (4f, 6f);
			}
		}


	}
}
