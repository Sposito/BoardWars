using UnityEngine;
using System.Collections;

public class LightiningController : MonoBehaviour {

	SpriteRenderer volumetric1; //Lightining Texture Sprites are child of this controller object
	SpriteRenderer volumetric2;

	Color v1Color; //initial color of sprite 1
	Color v2Color; //initial color of sprite 2
	Color transparent; //defnition of "transparent" to be interpolated
	float v1Transparency = 1f;
	float v2Transparency = 1f;
	public float cycleCounter = 0f; //float that comes from 0 to 2π (in radians)

	[SerializeField]
	private float localSpeed = 4f;

	void Start () {
		//Start will basically define the variables values in the beggining of game
		volumetric1 = transform.FindChild("VolumetricLightsPhase1").GetComponent<SpriteRenderer>();
		volumetric2 = transform.FindChild("VolumetricLightsPhase2").GetComponent<SpriteRenderer>();
		v1Color = volumetric1.color;
		v2Color = volumetric2.color;
		transparent = new Color (.78f, .7f, .67f, .3f);
	}

	void Update () {
		//looops trought the circle based on its own speed and game rhythm
		cycleCounter += Mathf.Deg2Rad * BoardController.rythmRate * Time.deltaTime * localSpeed;
		if (cycleCounter > 360f * Mathf.Deg2Rad)
			cycleCounter -= 360 * Mathf.Deg2Rad;

		v1Transparency = Mathf.Sin (cycleCounter);
		v1Transparency = Mathf.Abs (v1Transparency);

		v2Transparency = Mathf.Cos (cycleCounter * 2);// TODO: using 1 - sin would save a cos calculation per cicle;
		v2Transparency = Mathf.Abs (v2Transparency);

		volumetric1.color = Color.Lerp (v1Color, transparent, v1Transparency);
		volumetric2.color = Color.Lerp (v2Color, transparent, v2Transparency);
	}
}