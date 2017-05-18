using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ObjetoTrepar : MonoBehaviour {

	public Transform upPoint;
	public Transform downPoint;

	void OnTriggerEnter2D (Collider2D col){
		if (col.CompareTag ("Player")) {
			Debug.Log ("Dentro");
			col.gameObject.GetComponent <Player> ().OnClimb (this,true);
		}
	}

	void OnTriggerExit2D (Collider2D col){
		if (col.CompareTag ("Player")) {
			col.gameObject.GetComponent <Player> ().OnClimb (this,false);
		}
	}
}
