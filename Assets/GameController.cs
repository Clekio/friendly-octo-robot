using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;
    public GameObject Aura;
    public GameObject camara;

    public bool CheckPointOrder;
    public Scr_PlayerCheckpoint lastCheckPoint;
    private List<Scr_PlayerCheckpoint> checkPoints;


    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
        DontDestroyOnLoad(Aura);
        DontDestroyOnLoad(camara);

        checkPoints = new List<Scr_PlayerCheckpoint>();
        checkPoints.Add(lastCheckPoint);//añadimos el primer checkPoint (la salida)
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameOver(Aura);
        }
    }

    public void GameOver(GameObject player)
    {
        //menu game over
        Respawn(player);
    }

    public void Respawn(GameObject player)
    {
        SceneManager.LoadScene("Playtest", LoadSceneMode.Single);

        if (!CheckPointOrder)
            player.transform.position = lastCheckPoint.spawnPos.position;
        else
        {
            //Debug.Log("checkPoints.Count== " + checkPoints.Count);
            if (checkPoints.Count == 1)
            {
                player.transform.position = checkPoints[0].spawnPos.position;
            }
            else if (checkPoints.Count > 1)
            {
                player.transform.position = lastCheckPoint.spawnPos.position;
            }
        }
        //PlayerHP.instance.HitPoints = PlayerHP.instance.MaxHitPoints;


        //RespawnControler.instance.RespawnAll();
        //RespawnControler.instance.RepositionEnemies();
    }

    public void AddCheckPoint(Scr_PlayerCheckpoint newCP)
    {
        bool valido = false;
        for (int i = 0; i < checkPoints.Count; i++)
        {
            if (newCP == checkPoints[i])
                return;
            if (newCP.order >= checkPoints[i].order)
            {
                valido = true;
                if (newCP.order > checkPoints[i].order)
                {
                    checkPoints.Remove(checkPoints[i]);
                }
            }

        }
        if (valido && CheckPointOrder)
        {
            checkPoints.Add(newCP);
            lastCheckPoint = newCP;
        }
        else if (!CheckPointOrder)
        {
            lastCheckPoint = newCP;
        }

        //RespawnControler.instance.ClearRespawnObjects();
        //RespawnControler.instance.ClearRepositionEnemies();
    }

}
