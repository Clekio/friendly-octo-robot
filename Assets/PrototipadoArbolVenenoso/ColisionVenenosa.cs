using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColisionVenenosa : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Pull&Push") {
			Destroy (other.gameObject);
			Destroy (gameObject);
		} else {
			SceneManager.LoadScene ("Scn_PruebaArbolVenenoso");
		}
	}
}
