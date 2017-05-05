using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Scr_Wind1 : MonoBehaviour {

		void OnTriggerEnter2D (Collider2D element){
		

		element.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0f, 10f));

	}


}
