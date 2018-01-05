using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveCaja : MonoBehaviour
{
    public Controller2D controller;

    public LayerMask collisionMask;

    [SerializeField]
    private float maxDistance;

    [SerializeField]
    private Vector2 CollisionSize;

    private Vector3 center;
    
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            center = transform.position + (Vector3.up * (transform.localScale.y / 2));
            Collider2D hit = Physics2D.OverlapBox(center, new Vector2(CollisionSize.x, CollisionSize.y), 0, collisionMask);
            if (hit)
            {
                controller.box = hit.GetComponent<BoxController>();
            }
        }
        if (controller.box)
        {
            if (Input.GetKeyUp(KeyCode.E) || Vector3.Distance(controller.box.transform.position, transform.position) > maxDistance)
            {
                controller.box = null;
            }
        }
    }

    private bool canMoveBox()
    {
        bool b = true;

        if (controller == null)
            b = false;
        else if (!Input.GetKey(KeyCode.P))
            b = false;
        else if (Vector3.Distance(controller.transform.position, transform.position) > 1f)
            b = false;

        return b;
    }

    private void OnDrawGizmosSelected()
    {
        center = transform.position + (Vector3.up * (transform.localScale.y / 2));
        Gizmos.color = Colors.DarkCyan;
        Gizmos.DrawWireCube(center, new Vector3(CollisionSize.x, CollisionSize.y, 1));
        Gizmos.color = Colors.Cyan;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
