using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magia_01 : MonoBehaviour {

	public GameObject viento1;
	public GameObject remol1;


	public Texture2D cursorTextureVie;
	public Texture2D cursorTextureRaf;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

	int magia = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		Camera camera = GetComponent<Camera> ();

		Vector3 p = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15));



		if (Input.GetKeyDown ("3")) {

			Cursor.SetCursor (cursorTextureVie, hotSpot, cursorMode);

			magia = 3;

		}

		if (Input.GetKeyDown ("4")) {

			Cursor.SetCursor (cursorTextureRaf, hotSpot, cursorMode);

			magia = 4;
		}


		if (Input.GetMouseButtonDown (0) && (magia == 3)) {

			Instantiate (viento1, new Vector3 (p.x, p.y, 0), Quaternion.identity);
			Cursor.SetCursor(null, Vector2.zero, cursorMode);
			magia = 0;
		}

		if (Input.GetMouseButtonDown (2)) {

				Instantiate (remol1, new Vector3 (p.x, p.y, 0), Quaternion.identity);
				Cursor.SetCursor(null, Vector2.zero, cursorMode);
		}





}
}
