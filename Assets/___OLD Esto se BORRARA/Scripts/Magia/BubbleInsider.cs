using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleInsider : MonoBehaviour {

	public bool dentro;
	public float bubbleGravity;

	float newGravity;
	float oldGravity;

	// Use this for initialization
	void Start () {
		dentro = false;
		oldGravity = gameObject.GetComponent <Rigidbody2D> ().gravityScale;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (dentro) {
			gameObject.GetComponent <Renderer> ().material.color = Color.blue;
			gameObject.GetComponent <Rigidbody2D> ().gravityScale = newGravity;

		} else {
			gameObject.GetComponent <Renderer> ().material.color = Color.white;
			gameObject.GetComponent <Rigidbody2D> ().gravityScale = oldGravity;
		}

	}

	void OnCollisionEnter2D (){
		if (dentro) {
			dentro = false;
			Debug.Log ("False");
		}
	}
	
}
