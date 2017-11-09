using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Balancin : MonoBehaviour
{
    [SerializeField]
    int tiempoReset;

    float targetRotation;
    float currentRotation;
    float result;

    private void Start()
    {
        targetRotation = gameObject.GetComponentInParent<Transform>().rotation.z;
    }

    private void Update()
    {
        currentRotation = gameObject.GetComponentInParent<Transform>().rotation.z * 100;

        Debug.Log(result);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            Invoke("ResetBalancin", tiempoReset);
        }
    }

    void ResetBalancin()
    {
        result = 0 - currentRotation;

        gameObject.transform.parent.Rotate(new Vector3 (0, 0, result));

        gameObject.GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}