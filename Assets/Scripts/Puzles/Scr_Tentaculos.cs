using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Tentaculos : MonoBehaviour
{
    Vector3 movimientoTentaculos;
    bool tentaculos;
    Vector3 position;
    bool purificacion;

    public int VelocidadTentaculos1 = 4;
    public int VelocidadTentaculos2 = 7;
    public float TiempoDestruccion = 2;

    public GameObject destruir;

    void Start ()
    {
        position = transform.position;
	}
	
	void Update ()
    {
        Vector3 velocity = movimientoTentaculos * Time.deltaTime;
        transform.Translate(velocity);

        tentaculos = Scr_Menhir.activarTentaculos;

        purificacion = Scr_TriggerTentaculos.purificacionPosible;

        // SALIDA TENTACULOS

        if (position.x <= 0)
        {
            if (tentaculos == true)
            {
                movimientoTentaculos = new Vector3(VelocidadTentaculos1, 0, 0);
            }
        }
        else
        {
            if (tentaculos == true)
            {
                movimientoTentaculos = new Vector3(-VelocidadTentaculos1, 0, 0);
            }
        }

        // VUELTA TENTACULOS

        if (purificacion == true)
        {
            if (position.x <= 0)
            {
                movimientoTentaculos = new Vector3(-VelocidadTentaculos2, 0, 0);
            }
            else
            {
                movimientoTentaculos = new Vector3(VelocidadTentaculos2, 0, 0);
            }
            Destroy(destruir, TiempoDestruccion);
        }
    }
}