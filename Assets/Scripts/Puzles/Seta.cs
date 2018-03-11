using UnityEngine;

public class Seta : MonoBehaviour
{
    public float FuerzaDelImpulso;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 speed = transform.up * FuerzaDelImpulso;
            Player.Instance.addSpeed(speed.x, speed.y);
        }
    }
}
