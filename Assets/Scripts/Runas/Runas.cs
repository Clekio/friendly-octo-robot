using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runas : recognizerRunas
{
    private Vector3 thisDelta;
    private Vector3 LastDelta;      //Delta del frame anterior.

    private Vector3 mousePosition;
    private Vector3 LastMousePos;   //Posicion del mouse en el frame anterior.
    
    List<Vector2> m_pointList;

    [Header("Visual")]
    public GameObject particlePrefab;

    [Header("Magic")]
    public float MagicTimeToDestroy;

    public GameObject water;
    public GameObject thunder;
    public GameObject wind;
    public GameObject swirl;

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetMouseButtonDown(0))
        {
            //Resetear el los valores de la runa.
            m_pointList = new List<Vector2>();

            //se crean las particulas
            Instantiate(particlePrefab);
        }

        if (Input.GetMouseButton(0))
        {
            //Guardar las posiciones en las que se dibuja
            mousePosition = Input.mousePosition; //Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));

            thisDelta = (mousePosition - LastMousePos) / Time.deltaTime;

            if ((thisDelta - LastDelta).magnitude < 0.05f)
                m_pointList.Add(mousePosition);

            LastDelta = thisDelta;
            LastMousePos = mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Reconocer la Runa
            StartRecognizer(m_pointList);//, m_deltaList);

            //Crear la magia
            SpawnMagic(m_magicName, m_magicPosition, m_magicAngle, m_magicScale);
        }
    }

    public void SpawnMagic(string name, Vector2 position, float angle, Vector2 scale)
    {
        GameObject effect;

        if (name == "wind")
        {
            effect = Instantiate(wind, new Vector3(position.x, position.y, 0), Quaternion.identity) as GameObject;
            effect.transform.localScale = scale;
            effect.GetComponent<AreaEffector2D>().forceAngle = angle;
            Destroy(effect, MagicTimeToDestroy);
        }

        else if (name == "swirl")
        {
            effect = Instantiate(swirl, new Vector3(position.x, position.y, 0), Quaternion.identity) as GameObject;
            effect.transform.localScale = scale;
            Destroy(effect, MagicTimeToDestroy);
        }

        else if (name == "water")
        {
            effect = Instantiate(water, new Vector3(position.x, position.y, 0), Quaternion.Euler(90,90,0)) as GameObject;
            //effect.transform.localScale = scale;
            Destroy(effect, 11.0f);
        }

        else if (name == "thunder")
        {
            effect = Instantiate(thunder, new Vector3(position.x, position.y, 0), Quaternion.identity) as GameObject;
            //effect.transform.localScale = scale;
            Destroy(effect, 3.0f);
        }
    }

//#if UNITY_EDITOR
    private Rect windowRect = new Rect(3, 3, 100, 45);

    void OnGUI()
    {
        windowRect = GUI.Window("RunasDebug".GetHashCode(), windowRect, DrawWindowContents, "Magia Name");
    }

    void DrawWindowContents(int windowId)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("   " + m_magicName);
        GUILayout.EndHorizontal();

        GUI.DragWindow();
    }
//#endif
}
