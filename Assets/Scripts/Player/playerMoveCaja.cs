﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveCaja : MonoBehaviour
{
    public LayerMask collisionMask;

    [SerializeField]
    private float maxDistance;

    [SerializeField]
    private Vector2 CollisionSize;

    [SerializeField]
    private Rect grabColl;

    private Vector3 center;

    [Header("Referencias")]
    [SerializeField]
    private Player player;
    [SerializeField]
    private Controller2D controller;
    
    void Update ()
    {
        if (player.input.Action4.WasPressed)
        {
            Collider2D hit = Physics2D.OverlapBox(transform.TransformPoint(grabColl.position), grabColl.size, 0, collisionMask);
            if (hit)
            {
                controller.box = hit.GetComponent<BoxController>();
            }
        }
        if (controller.box)
        {
            if (player.input.Action4.WasReleased || Vector3.Distance(controller.box.transform.position, transform.position) > maxDistance)
            {
                controller.box = null;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Colors.DarkCyan;
        Gizmos.DrawWireCube(transform.TransformPoint(grabColl.position), grabColl.size);
        Gizmos.color = Colors.Cyan;
        Gizmos.DrawWireSphere(transform.position + Vector3.up, maxDistance);
    }
}
