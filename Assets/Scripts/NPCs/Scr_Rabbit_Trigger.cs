using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Rabbit_Trigger : MonoBehaviour {

	public Transform NextPoint;
	public GameObject Rabbit;
	public bool RabbitNextPoint;
	public bool MakeInvisible;

	bool used = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D col){
		if (col.tag == "Player" || col.GetComponent <Scr_Rabbit_Mov> ()) {
			if (MakeInvisible) {
				Rabbit.GetComponent <Scr_Rabbit_Mov> ().MakeInvisible ();
			}
			if (col.tag == "Player" || (col.GetComponent <Scr_Rabbit_Mov> () && RabbitNextPoint)) {
				Rabbit.GetComponent <Scr_Rabbit_Mov> ().SaveNextPos (NextPoint);
				Rabbit.GetComponent <Scr_Rabbit_Mov> ().RabbitAnimator.SetBool ("PlayerClose", true);
			}
			if (col.tag == "Player" || (col.GetComponent <Scr_Rabbit_Mov> () && RabbitNextPoint)) {
				Destroy (gameObject);
			}
		}

	}
}
