using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BuildEscalera : MonoBehaviour
{
    public Vector3 targetPosition
    {
        get
        {
            return transform.position + Vector3.up * altura;
        }
        set
        {
            altura = (value - transform.position).y;
        }
    }
    [SerializeField]
    private float altura = 0;

    [SerializeField]
    private BoxCollider2D bColl2D;

    [SerializeField]
    private SpriteRenderer sprite;

    public virtual void SetEscaleras()
    {
        if (bColl2D)
        {
            bColl2D.offset = new Vector2(0, altura / 2);
            bColl2D.size = new Vector2(bColl2D.size.x, altura);
        }

        if (sprite)
        {
            sprite.transform.position = transform.position + (Vector3.up * altura / 2);
            sprite.size = new Vector2(bColl2D.size.x, altura);
        }
    }
}
