using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaAscensor : MonoBehaviour {

	public Animator anim;

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Pull&Push")
			anim.SetBool ("Corriente", false);
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Pull&Push")
			anim.SetBool ("Corriente", true);
	}
}
