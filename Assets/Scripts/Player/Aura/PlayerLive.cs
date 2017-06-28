using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLive : MonoBehaviour {

    public Vector3 RespawnPosition;
    public float WaitTimeToRespawn = 2f;

    private Player scrPlayer;

    private static float m_respawnTimeLeft = 0.0f;

    private void Awake()
    {
        scrPlayer = GetComponent<Player>();
        RespawnPosition = transform.position;
    }

    public IEnumerator killAndRespawn()
    {
        Debug.Log("hey");
        scrPlayer.Death = true;
        yield return new WaitForSeconds(WaitTimeToRespawn);

        transform.position = RespawnPosition;
        scrPlayer.Death = false;
    }
}
