using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crecer : MonoBehaviour
{
    bool move = true;

    [Range(5f, 100f)]
    public float Duracion = 5f;

    public Vector3 newScale;

    private Vector3 oldScale;

    private void Start()
    {
        oldScale = transform.localScale;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (move)
        {
            StartCoroutine(DelayTimerOneShot(Duracion));
            move = false;
        }
    }
	
	//// Update is called once per frame
	//void Update ()
 //   {
 //       if (move)
 //       {
 //           transform.localScale = newScale;
 //           move = false;
 //       }
 //   }

    // Wait for an amount of time before doing something (one shot behavior)
    private IEnumerator DelayTimerOneShot(float delayLength)
    {
        transform.localScale = newScale;
        yield return new WaitForSeconds(delayLength);
        // Do something
        transform.localScale = oldScale;

        move = true;
    }
}