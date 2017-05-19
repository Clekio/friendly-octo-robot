using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Rabbit_Mov : MonoBehaviour {

	public Vector3 NextPoint; 
	public Animator RabbitAnimator;
	public float speed;
	public SpriteRenderer Sprite;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (RabbitAnimator.GetBool ("PlayerClose")) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, NextPoint, step);
		}

		if (transform.position == NextPoint) {
			RabbitAnimator.SetBool ("PlayerClose", false);
			NextPoint = transform.position;
		}
	}

	public void MakeInvisible (){
		Sprite.enabled = !Sprite.enabled;
	}

	public void SaveNextPos (Transform pos){
		NextPoint = pos.position;
	}
}
