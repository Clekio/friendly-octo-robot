using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Bird_Trigger : MonoBehaviour {

	public Transform NextPoint;
	public GameObject Bird;
	public bool BirdNextPoint;
	public bool MakeInvisible;

	bool used = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D col){
		if (BirdNextPoint) {
			if (col.GetComponent <Scr_Bird_Mov> ()) {
				if (MakeInvisible) {
					Bird.GetComponent <Scr_Bird_Mov> ().MakeInvisible ();
				}
				Bird.GetComponent <Scr_Bird_Mov> ().SaveNextPos (NextPoint);
				Bird.GetComponent <Scr_Bird_Mov> ().BirdAnimator.SetBool ("PlayerClose", true);
				Destroy (gameObject);
			}

		} else {
			if (col.tag == "Player") {
				if (MakeInvisible) {
					Bird.GetComponent <Scr_Bird_Mov> ().MakeInvisible ();
				}
				Bird.GetComponent <Scr_Bird_Mov> ().SaveNextPos (NextPoint);
				Bird.GetComponent <Scr_Bird_Mov> ().BirdAnimator.SetBool ("PlayerClose", true);
				Destroy (gameObject);
			} 
		}
	}
}
