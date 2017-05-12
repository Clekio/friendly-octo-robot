using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D col){
		if (col.GetComponent <BubbleInsider> ()) {
			Debug.Log ("bubble");
			BubbleInsider insider = col.GetComponent <BubbleInsider> ();
			insider.dentro = true;
			DestroyObject (gameObject);
		} else {
			DestroyObject (gameObject);
		}
	}

}
