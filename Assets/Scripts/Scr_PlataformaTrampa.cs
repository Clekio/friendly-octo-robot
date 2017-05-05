using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlataformaTrampa : MonoBehaviour {

	public GameObject plat;
	public float time2Drop;
	public float time2Destroy;

	float timerDrop;
	bool permiso;
	Rigidbody2D rb2D;
	
	void Start () {
		timerDrop = 0;
		permiso = false;
		rb2D = plat.GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		if (permiso == true)
		{
			timerDrop += Time.deltaTime;
			if (timerDrop > time2Drop)
			{
				rb2D.constraints = RigidbodyConstraints2D.None;
				Destroy(plat, time2Destroy);
			}
		}
	}

	void OnTriggerStay2D (Collider2D col)
    {
        if (col.tag == "Player")
        {
            permiso = true;
        }
    }

    void OnTriggerExit2D (Collider2D col)
    {
        if (col.tag == "Player")
        {
            permiso = false;
            timerDrop = 0;
        }
    }
}
