using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ResetPosition : MonoBehaviour
{
    [SerializeField] int checkDistance; // Distancia en el eje vertical límite para el ojbeto antes de retornar a su posición inicial
    [SerializeField] float timeToReset; // Tiempo que está el objeto incativo antes de volver a su posición inical
    Vector3 startPosition;
    float timer;
    bool timerRunning;
    BoxController boxController;

    private void Awake()
    {
        startPosition = GetComponent<Transform>().position;
    }

    private void Update()
    {
        if (timerRunning == true)
            timer -= Time.deltaTime;

        if (boxController.moving == true)
        {
            timerRunning = false;
            ResetTimer(timeToReset);
        }

        if (startPosition.y < (startPosition.y - checkDistance))
        {
            timerRunning = true;
        }

        if (timer <= 0)
            ResetPosition();
    }

    public void ResetPosition()
    {
        gameObject.transform.position = startPosition;
    }

    public void ResetTimer(float newTime)
    {
        timer = newTime;
    }
}