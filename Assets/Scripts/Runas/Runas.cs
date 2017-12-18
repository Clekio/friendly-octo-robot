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

    [Header("Puntero")]
    [SerializeField]
    private float velocidadPuntero;
    private Vector2 posPuntero;
    private GameObject magicParticles;

    bool drawing = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            posPuntero = Player.reference.transform.position + new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * 3;

            if ((thisDelta - LastDelta).magnitude < 0.05f)
                m_pointList = new List<Vector2>();
            LastMousePos = posPuntero;

            magicParticles = Instantiate(particlePrefab, (Vector3)posPuntero + Vector3.forward * 20, Quaternion.identity);
        }
        else if (Input.GetKey(KeyCode.L))
        {
            posPuntero += new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * velocidadPuntero * Time.deltaTime;

            m_pointList.Add(Camera.main.WorldToScreenPoint(posPuntero));

            magicParticles.transform.position = (Vector3)posPuntero + Vector3.forward*20;
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            //Reconocer la Runa
            StartRecognizer(m_pointList);

            Destroy(magicParticles, 3);
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    //Resetear el los valores de la runa.
        //    if ((thisDelta - LastDelta).magnitude < 0.05f)
        //        m_pointList = new List<Vector2>();
        //    LastMousePos = Input.mousePosition;
        //    //se crean las particulas
        //    Instantiate(particlePrefab);
        //    drawing = true;
        //}

        //if (Input.GetMouseButton(0))
        //{
        //    //Guardar las posiciones en las que se dibuja
        //    mousePosition = Input.mousePosition; //Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));

        //    thisDelta = (mousePosition - LastMousePos) / Time.deltaTime;

        //    //if ((thisDelta - LastDelta).magnitude < 0.05f)
        //    m_pointList.Add(mousePosition);

        //    LastDelta = thisDelta;
        //    LastMousePos = mousePosition;
        //}

        //if (!Input.GetMouseButton(0) && drawing)
        //{
        //    //Reconocer la Runa
        //    StartRecognizer(m_pointList);//, m_deltaList);

        //    //Crear la magia
        //    SpawnMagic(m_magicName, m_magicPosition, m_magicAngle, m_magicScale);
        //    drawing = false;
        //}
    }

    public override void SpawnMagic(string name, Vector2 position, float angle, Vector2 scale)
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
            effect = Instantiate(water, new Vector3(position.x, position.y, 0), Quaternion.Euler(90, 90, 0)) as GameObject;
            //effect.transform.localScale = scale;
            Destroy(effect, 11.0f);
        }

        else if (name == "thunder")
        {
            effect = Instantiate(thunder, new Vector3(position.x, position.y, 0), Quaternion.identity) as GameObject;
            //effect.transform.localScale = scale;
            Destroy(effect, 3.0f);
        }

        m_magicName = "Error";
    }

    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(posPuntero, 0.25f);

        if (optimacedPointList != null)
            DebugLine(optimacedPointList, Colors.Red);

        if (normalizedPointList != null)
            DebugLine(normalizedPointList, Colors.Green);

        if (globalPointList != null)
            DebugLine(globalPointList, Colors.Pink);

        if (simplifyPointList != null)
        {
            Vector2 vP = new Vector2(0, 0);
            for (int i = 0; i < simplifyPointList.Count; i++)
            {
                Debug.DrawLine(vP, simplifyPointList[i] + vP, Colors.Blue);
                vP = vP + simplifyPointList[i];
            }
        }
    }
    #endif
}
