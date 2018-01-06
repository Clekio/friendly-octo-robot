using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveCaja : MonoBehaviour
{
    public LayerMask collisionMask;

    [SerializeField]
    private float maxDistance;

    [SerializeField]
    private Vector2 CollisionSize;

    private Vector3 center;

    [Header("Referencias")]
    [SerializeField]
    private Player player;
    [SerializeField]
    private Controller2D controller;
    
    void Update ()
    {
        if (player.input.Action4.WasPressed)//Input.GetKeyDown(KeyCode.E))
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
            if (player.input.Action4.WasReleased/*Input.GetKeyUp(KeyCode.E)*/ || Vector3.Distance(controller.box.transform.position, transform.position) > maxDistance)
            {
                controller.box = null;
            }
        }
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
