using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerRaicesVuelta2 : MonoBehaviour
{
    [SerializeField]
    Animator raicesVuelta;

    bool trigger2 = false;

    void Start()
    {
        raicesVuelta = GetComponent<Animator>();
    }

    void Update()
    {
        trigger2 = Scr_TriggerRaicesVuelta1.trigger2;
        raicesVuelta.SetBool("espinasDown", trigger2);
    }
}