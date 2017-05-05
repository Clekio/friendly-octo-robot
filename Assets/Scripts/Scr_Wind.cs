using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Scr_Wind : MonoBehaviour {

		void OnTriggerEnter2D (Collider2D element){
		

		element.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (10f, 0f));

	}


}
