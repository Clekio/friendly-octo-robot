using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxController))]
public class Scr_ResetPosition : MonoBehaviour
{
    public enum ResetMode
    {
        ExitTrigger,
        EnterTrigger,
        Heigh,
        Time
    }

    Scr_BoxTrigger boxTrigger;      // Script trigger asociado en el editor
    BoxController boxController;    // Script controlador de la caja
    bool inside;                    // Determina si el player está dentro o fuera del trigger asociado
    Vector3 initialPosition;        // Guarda la posición en la que compienza el objeto
    bool resetDone;                 // Se hace true una vez se ha hecho un reset
    float initialTime;              // Guarda el tiempo de reset original para cualquiera de los modos que admite una variable de tiempo
    bool startTime;                 // Se hace true una vez el jugador ha movido la caja por primera vez
    BoxCollider2D colliderCaja;     // Collider del objeto
    ParticleSystem trail;           // Sistema de partículas retorno de la caja
    bool moving;                    // Se hace true durante el retorno del objeto a su posición inicial
    SpriteRenderer sprite;          // Sprite de la caja que está en un objeto hijo
    float distance;                 // Distancia entre la posición actual y la incial

    [Header("Select Reset Mode")]
    [Tooltip("Solo se tendrán en cuenta los parámetros que afecten al modo activo")]
    [SerializeField] ResetMode resetMode;

    [Header("Custom Parameters: All Modes")]
    [Tooltip("En true los resets son infinitos, en false solo se produce uno")]
    [SerializeField] bool infiniteResets;
    [Tooltip("Trigger que puede ser utilizado para cualquiera de los dos primeros modos")]
    [SerializeField] GameObject trigger;
    [Tooltip("Con la tecla Ñ se hace un reset automático para las rondas de testeo en caso de error")]
    [SerializeField] bool ManualReset;
    [Tooltip("Velocidad de vuelta del objeto a su posición inicial")]
    [SerializeField] float speed;
    [Tooltip("Tiempo que tarda en empezar la vuelta una vez se ha producido el reset")]
    [SerializeField] float delay;

    [Header("Custom Parameters: Exit Trigger Mode")]
    [Tooltip("Tiempo que tarda en hacer el reset una vez se sale del trigger")]
    [SerializeField] float timeAfterExit;
    [Tooltip("Si se resetea o no el tiempo al volver a entrar en el trigger")]
    [SerializeField] bool resetIfEnter;

    [Header("Custom Parameters: Enter Trigger Mode")]
    [Tooltip("Tiempo que tarda en hacer el reset una vez se entra en el trigger")]
    [SerializeField] float timeAfterEnter;
    [Tooltip("Si se resetea o no el tiempo salir del trigger")]
    [SerializeField] bool resetIfExit;

    [Header("Custom Parameters: Heigh Mode")]
    [Tooltip("Distancia en el eje Y a partir de la cual se hace reset")]
    [SerializeField] int heighDistance;
    [Tooltip("Margen en el eje Y a partir del cual se empieza a comprobar")]
    [SerializeField] int heighMargin;
    [Tooltip("Tiempo que tarda en hacer el reset una vez se supera la altura especificada")]
    [SerializeField] float heighTime;
    [Tooltip("Si se resetea o no el tiempo al ser movido por el player")]
    [SerializeField] bool heighModeReset;

    [Header("Custom Parameters: Time Mode")]
    [Tooltip("Tiempo que ha de pasar para que se haga el reset")]
    [SerializeField] float time;
    [Tooltip("Si se resetea o no el tiempo al ser movido por el player")]
    [SerializeField] bool timeModeReset;

    private void Awake()
    {
        initialPosition = transform.position;

        boxTrigger = trigger.GetComponent<Scr_BoxTrigger>();
        boxController = GetComponent<BoxController>();

        colliderCaja = GetComponent<BoxCollider2D>();
        trail = GetComponentInChildren<ParticleSystem>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        switch (resetMode)
        {
            case ResetMode.ExitTrigger:
                boxTrigger.insideStart = true;
                initialTime = timeAfterExit;
                break;
            case ResetMode.EnterTrigger:
                boxTrigger.insideStart = false;
                initialTime = timeAfterEnter;
                break;
            case ResetMode.Heigh:
                initialTime = heighTime;
                break;
            case ResetMode.Time:
                initialTime = time;
                break;
        }
    }

    private void Update()
    {
        inside = boxTrigger.inside;

        distance = Vector3.Distance(transform.position, initialPosition);

        if (infiniteResets || (!infiniteResets && !resetDone))
        {
            switch (resetMode)
            {
                case ResetMode.ExitTrigger:
                    if (inside == false)
                    {
                        timeAfterExit -= Time.deltaTime;

                        if (timeAfterExit <= 0)
                        {
                            Reset();
                        }
                    }

                    else if (resetIfEnter)
                    {
                        timeAfterExit = initialTime;
                    }
                    break;
                case ResetMode.EnterTrigger:
                    if (inside == true)
                    {
                        timeAfterEnter -= Time.deltaTime;

                        if (timeAfterEnter <= 0)
                        {
                            Reset();
                        }
                    }

                    else if (resetIfExit)
                    {
                        timeAfterEnter = initialTime;
                    }
                    break;
                case ResetMode.Heigh:
                    if (gameObject.transform.position.y <= (initialPosition.y - heighMargin) - heighDistance)
                    {
                        heighTime -= Time.deltaTime;

                        if (heighTime <= 0)
                        {
                            Reset();
                        }

                        else if (heighModeReset && boxController.moving)
                        {
                            heighTime = initialTime;
                        }
                    }
                    break;
                case ResetMode.Time:
                    if (boxController.moving)
                    {
                        startTime = true;
                    }

                    if (startTime)
                    {
                        time -= Time.deltaTime;

                        if (time <= 0)
                        {
                            Reset();

                            startTime = false;
                        }

                        else if (boxController.moving)
                        {
                            time = initialTime;
                        }
                    }
                    break;
            }
        }

        if (moving)
        {
            if (distance > .5)
            {
                transform.position = Vector3.Lerp(transform.position, initialPosition, speed * Time.deltaTime);
            }

            else
            {
                transform.position = initialPosition;

                moving = false;

                colliderCaja.enabled = true;
                boxController.enabled = true;
                sprite.enabled = true;
                trail.Stop();
            }
        }
    }

    private void Reset()
    {
        resetDone = true;

        colliderCaja.enabled = false;
        boxController.enabled = false;
        sprite.enabled = false;
        trail.Play();

        Invoke("StartAnimation", delay);

        switch (resetMode)
        {
            case ResetMode.ExitTrigger:
                timeAfterExit = initialTime;
                break;
            case ResetMode.EnterTrigger:
                timeAfterEnter = initialTime;
                break;
            case ResetMode.Heigh:
                heighTime = initialTime;
                break;
            case ResetMode.Time:
                time = initialTime;
                break;
        }
    }

    void StartAnimation()
    {
        moving = true;
    }
}