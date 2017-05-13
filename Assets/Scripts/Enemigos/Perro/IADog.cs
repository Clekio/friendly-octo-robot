using UnityEngine;
using System.Collections;

public class IADog : MonoBehaviour {


public Transform target;//set target from inspector instead of looking in Update
public float speed = 2f;
public bool close = false;


void Start () {

}

void Update(){

	//rotate to look at the player
	//transform.LookAt(target.position);
	//transform.Rotate(new Vector3(0,-90,0),Space.Self);//correcting the original rotation

	if (close == true) { 
		if (transform.position.x != target.position.x) {
			if (transform.position.x > target.position.x) {
					transform.Translate (new Vector3 (-speed * (Time.deltaTime), 0, 0));
					if (transform.position.x == target.position.x){
						close = false;
					}
			} else if (transform.position.x < target.position.x) {
				transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0)); 
					if (transform.position.x == target.position.x){
						close = false;
					}
			}
		}
	}
}

void OnTriggerEnter2D(Collider2D other) {
	if (other.tag == "Player") {
		close = true;
	}
}

void OnTriggerExit2D(Collider2D other) {
	if (other.tag == "Player") {
		close = false;
	}
}
}