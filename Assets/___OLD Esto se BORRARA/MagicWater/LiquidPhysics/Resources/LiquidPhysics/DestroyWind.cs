using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWind : MonoBehaviour {

	public float t;

	// Use this for initialization
	void Start () {


		GameObject.Destroy (gameObject, t);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
