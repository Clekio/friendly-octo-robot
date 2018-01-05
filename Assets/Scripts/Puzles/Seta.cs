using UnityEngine;

public class Seta : MonoBehaviour {

    public float FuerzaDelImpulso;

    private void OnTriggerStay2D (Collider2D other)
    {
       //Debug.Log(transform. * Vector3.forward);
       Rigidbody2D rb2d = other.gameObject.GetComponent<Rigidbody2D>();

        if (!rb2d.isKinematic)
            rb2d.velocity = new Vector2(rb2d.velocity.x, FuerzaDelImpulso);

        Aura p = other.gameObject.GetComponent<Aura>();
        if (p != null)
            p.ignoreJumpDepress = true;
    }
}
