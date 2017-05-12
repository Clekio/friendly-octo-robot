using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour {
	public Camera cam;
	public string cameraName;

	void Start () {
		if (!cam) {
			cam = GameObject.Find (cameraName).GetComponent <Camera> ();
		}
	}

	void Update () {
		Vector3 pn = cam.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 15));
		transform.position = new Vector3 (pn.x, pn.y, 0);
		Debug.Log (transform.position);
	}
}
