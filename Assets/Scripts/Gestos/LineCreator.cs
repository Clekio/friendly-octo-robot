using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : MonoBehaviour {

    public GameObject linePrefab;

    Line activeLine;
    private GameObject lineGO;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lineGO = Instantiate(linePrefab);
            activeLine = lineGO.GetComponent<Line>();
        }

        if (Input.GetMouseButtonUp(0))
        {
            activeLine = null;
            GameObject.Destroy(lineGO, 2.0f);
        }

        if (activeLine != null)
        {
            //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
            activeLine.UpdateLine(mousePos);
        }
    }
}
