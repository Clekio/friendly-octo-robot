using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerRama3 : MonoBehaviour
{
    [SerializeField]
    Animator rama3;

    public static bool rama3Down = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        rama3Down = true;
    }

    void Update()
    {
        rama3.SetBool("rama3Down", rama3Down);
    }
}