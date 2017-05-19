using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerArbolRampa : MonoBehaviour
{
    public bool triggerArbolRampa;
    public static bool purificacionPosible = false;
    public Collider2D other;

    void OnTriggerEnter2D(Collider2D other)
    {
        triggerArbolRampa = true;
    }

    void Update()
    {
        bool arbolTirable = Scr_TriggerTentaculos.tirarArbolPosible;
        if (triggerArbolRampa == true && arbolTirable ==true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -30);
                triggerArbolRampa = false;
                purificacionPosible = true;
            }
        }
    }
}
