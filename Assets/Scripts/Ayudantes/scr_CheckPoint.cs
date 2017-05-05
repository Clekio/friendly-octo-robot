using UnityEngine;

public class scr_CheckPoint : MonoBehaviour {

    private Vector3 m_spawnPosition;

    private void Start()
    {
        m_spawnPosition = GetComponent<Transform>().position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            scr_Respawn.RespawnPosition = m_spawnPosition;

        Debug.Log("Spawn position: " + scr_Respawn.RespawnPosition);
    }
}
