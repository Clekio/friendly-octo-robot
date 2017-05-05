using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEffects : MonoBehaviour {

	public Scr_PlayerVictor player;

	public bool barro;
	public bool planta;
	public bool fuego;
	public bool seta;

	public bool setaDesactivable;

	public float plantSpeed;
	public float jumpMultiplier = 1.5f;

	public Transform plantPosition;

	bool plantActivated;
	bool setaActivated;

	float originalJump;
	float setaBounce;

	Color originalColor;

	// Use this for initialization
	void Start () {
		
		plantActivated = false;
		setaActivated = false;

		originalJump = player.MaxJumpHeight;
		setaBounce = originalJump * jumpMultiplier;

		originalColor = gameObject.GetComponent <Renderer> ().material.color;

	}
	
	// Update is called once per frame
	void Update () {
		if (plantActivated) {
			if (transform.position != plantPosition.position) {
				transform.position = Vector2.MoveTowards (transform.position, plantPosition.position, plantSpeed);
			} else {
				plantActivated = false;
			}
		}
		
	}

	void OnTriggerEnter2D (Collider2D col){
		if (col.tag == "Water") {
//			if (col.GetComponent <DynamicParticle> ().currentState == col.GetComponent <DynamicParticle> ().STATES.WATER) {
				ApplyEffect ();
//			}
		}

		if (col.tag == "Player" && setaActivated) {
			
			Debug.Log ("jump");

			player.MaxSetaImpulse = setaBounce;

			player.OnSetaOn ();

			if (setaDesactivable) {
				setaActivated = false;
				gameObject.GetComponent <Renderer> ().material.color = originalColor;
			}
		}
	}

	void ApplyEffect (){
		if (barro) {
			EfectoBarro ();
		}
		if (planta) {
			EfectoPlanta ();
		}
		if (fuego) {
			EfectoFuego ();
		}
		if (seta) {
			EfectoSeta ();
		}
	}

	void EfectoBarro (){
		gameObject.GetComponent <Renderer> ().material.color = Color.grey;
	}
	void EfectoPlanta (){
		gameObject.GetComponent <Renderer> ().material.color = Color.green;
		plantActivated = true;
	}
	void EfectoFuego (){
		gameObject.GetComponent <Renderer> ().material.color = Color.yellow;
	}
	void EfectoSeta (){
		gameObject.GetComponent <Renderer> ().material.color = Color.magenta;
		setaActivated = true;
	}

}
