using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Bird_Mov : MonoBehaviour {

	public Vector3 NextPoint; 
	public Animator BirdAnimator;
	public float speed;
	public SpriteRenderer Sprite;
	public bool IsRaven;

	// Use this for initialization
	void Start () {
		IsRaven = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (IsRaven) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, NextPoint, step);
		}
			
		if (transform.position == NextPoint) {
			NextPoint = transform.position;
		}

		if (BirdAnimator.GetCurrentAnimatorStateInfo(0).IsName("RavenFly")){
			IsRaven = true;
		}
			
	}

	public void MakeInvisible (){
		Sprite.enabled = !Sprite.enabled;
	}

	public void SaveNextPos (Transform pos){
		NextPoint = pos.position;
	}
}
