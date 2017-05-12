using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerArbolRampa : MonoBehaviour
{
    public bool triggerArbolRampa;
    public static bool purificacionPosible;

    void OnTriggerEnter2D(Collider2D other)
    {
        triggerArbolRampa = true;
    }

    void Update()
    {
        if (triggerArbolRampa == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -5);
                triggerArbolRampa = false;
                purificacionPosible = true;
            }
        }
    }
}
