using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaAscensor : MonoBehaviour {

	public Animator anim;

	void OnTriggerEnter(Collider other){
		if(other.tag == "Pull&Push")
			anim.SetBool ("Corriente", false);
	}

	void OnTriggerExit(Collider other){

			Debug.Log (3);
			anim.SetBool ("Corriente", true);
	}
}
