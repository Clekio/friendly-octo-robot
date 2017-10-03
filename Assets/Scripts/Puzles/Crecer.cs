using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crecer : MonoBehaviour
{
    bool move = false;

    public Vector3 newScale;

    private void OnParticleCollision(GameObject other)
    {
        move = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (move)
        {
            transform.localScale = newScale;
            move = false;
        }
    }
}