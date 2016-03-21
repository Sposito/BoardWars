using UnityEngine;
using System.Collections;
using System;

public class TypeTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		print(System.Runtime.InteropServices.Marshal.SizeOf (Vector3.zero));
		Floats floats = new Floats (2f, 5f, 2f);
		print(System.Runtime.InteropServices.Marshal.SizeOf (floats));
		print(sizeof(float));

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

struct Floats{

	float a,b,c;
public Floats (float a, float b, float c){
		this.a = a;
		this.b = b;
		this.c = c;
	}

}
