using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crecer : MonoBehaviour
{
    bool move = true;

    [Range(5f, 60f)]
    public float Duracion = 5f;

    public Vector3 newScale;

    public Collider2D Coll2D;

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

    // Wait for an amount of time before doing something (one shot behavior)
    private IEnumerator DelayTimerOneShot(float delayLength)
    {
        transform.localScale = newScale;
        if (Coll2D)
            Coll2D.enabled = true;

        yield return new WaitForSeconds(delayLength);

        // Do something
        transform.localScale = oldScale;
        if(Coll2D)
            Coll2D.enabled = false;

        move = true;
    }
}