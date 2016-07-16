using UnityEngine;
using System.Collections;

public class BreathBehaviour : MonoBehaviour {

	Transform t1;
	Transform t2;



	float cycleCounter;
	void Start () {
		t1 = transform.GetChild (0);
		t2 = transform.GetChild (1);
		cycleCounter = 360f * Mathf.Deg2Rad * Random.value;
	}
	
	// Update is called once per frame
	void Update () {
		cycleCounter += Mathf.Deg2Rad * BoardController.rythmRate * Time.deltaTime * 100;
		if (cycleCounter > 360f * Mathf.Deg2Rad)
			cycleCounter -= 360f * Mathf.Deg2Rad;

		t1.localScale = new Vector3(1f, (1 + Mathf.Sin (cycleCounter) / 40),1f);
		t2.localScale = new Vector3(1f, (1 + Mathf.Sin (cycleCounter) / 40),1f);
	}
}
