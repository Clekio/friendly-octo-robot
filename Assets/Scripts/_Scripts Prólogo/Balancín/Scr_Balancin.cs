using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Balancin : MonoBehaviour
{
    [SerializeField]
    int tiempoReset;

    [SerializeField]
    float rotationSpeed;

    Transform target;
    Rigidbody2D rb;

    private void Start()
    {
        target = transform.parent;

        rb = gameObject.GetComponentInParent<Rigidbody2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.SetActive(false);

            Invoke("ResetBalancin", tiempoReset);
        }
    }

    void ResetBalancin()
    {
        transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, target.rotation, rotationSpeed);

        if (transform.parent.rotation == target.rotation)
        {

        }
    }
}