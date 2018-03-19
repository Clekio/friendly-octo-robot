using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour {

	public Transform personaje;
	public Transform plataforma;

	void OnCollisionEnter2D (Collision2D other){
		personaje.SetParent (plataforma);
	}

	void OnCollisionExit2D (Collision2D other){
		if(other.gameObject.tag == "Player")
		personaje.parent = null;
	}
}
