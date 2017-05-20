using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runas : RunasRecognizer {

    private Vector3 LastMousePos;   //Posicion del mouse en el frame anterior.
    private Vector3 LastDelta;    //Delta del frame anterior.

    RunasTemplate m_Templates;

    List<Vector2> m_pointList;
    List<Vector2> m_deltaList;

    [Header("Line Renderer")]
    public GameObject linePrefab;
    Line activeLine;
    private GameObject lineGO;

    [Header("Magic")]
    public float MagicTimeToDestroy;

    public GameObject waterBall;
    public GameObject waterBubble;
    public GameObject viento;
    public GameObject remolino;

    // Use this for initialization
    void Start ()
    {
        m_Templates = new RunasTemplate();

        // Decir si se han cargado las Templates y cuantas son.
        Debug.Log("Templates Loaded: " + RunasTemplate.TemplateRunasList.Count);

        Debug.DrawLine(new Vector3(200, 200, 200), Vector3.zero, Color.green, 2, false);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 thisDelta = (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z)) - LastMousePos) / Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            //Resetear el los valores de la runa.
            m_pointList = new List<Vector2>();

            m_deltaList = new List<Vector2>();

            //Se crea una nueva linea y se coje la referencia al script
            lineGO = Instantiate(linePrefab);
            activeLine = lineGO.GetComponent<Line>();
        }

        if (Input.GetMouseButton(0))
        {
            //Guardar las posiciones en las que se dibuja
            Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));

            m_pointList.Add(p);
            m_deltaList.Add(thisDelta);

            if (activeLine != null)
                activeLine.UpdateLine(p);
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Reconocer la Runa
            StartRecognizer(m_pointList, m_deltaList);

            //Crear la magia
            SpawnMagic(m_magicName, m_magicPosition, m_magicAngle, m_magicScale);

            //Finalizar la interaccion con la linea.
            activeLine = null;
            Destroy(lineGO, MagicTimeToDestroy);
        }

        LastDelta = thisDelta;
        LastMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
    }

    public void SpawnMagic(string name, Vector2 position, float angle, Vector2 scale)
    {
        GameObject effect;

        if (name == "wind")
        {
            effect = Instantiate(viento, new Vector3(position.x, position.y, 0), Quaternion.identity) as GameObject;
            effect.transform.localScale = scale;
            effect.GetComponent<AreaEffector2D>().forceAngle = angle;
            Destroy(effect, MagicTimeToDestroy);
        }
    }


}
