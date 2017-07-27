using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crecer : MonoBehaviour
{
    bool move = false;

    private void OnParticleCollision(GameObject other)
    {
        move = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (move)
        {
            transform.localScale = new Vector3(1, 4, 0);
            move = false;
        }
    }


}
