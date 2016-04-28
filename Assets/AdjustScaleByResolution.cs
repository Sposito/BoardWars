using UnityEngine;

public class AdjustScaleByResolution : MonoBehaviour {
	void Start () {
		transform.localScale *= Screen.currentResolution.width / 2048f;
		print((int)( (float)Screen.currentResolution.width / 2048f));
	}
}