using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerTentaculos : MonoBehaviour
{
    public bool triggerTentaculos = false;
    public static bool purificacionPosible = false;
    public static bool tirarArbolPosible = false;
    public Animator tentaculos;

    void OnTriggerEnter2D(Collider2D other)
    {
        triggerTentaculos = true;
    }

    void Update()
    {
        if (triggerTentaculos == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -30);
                triggerTentaculos = false;
                purificacionPosible = true;
                tirarArbolPosible = true;
            }
        }
    }
}
