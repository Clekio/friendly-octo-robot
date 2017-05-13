using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Scr_Controller2D))]

public class Scr_Player : MonoBehaviour {

    [Header("Configuración")]
    public float VelocidadMovimiento = 6;
    public float AlturaSalto = 4;
    public float TiempoEnAire = .4f;
    public float TiempoPlaneando = 4;
    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;

    Animator anim;

    float oldTiempoEnAire;
    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;
    Scr_Controller2D controller;

    [HideInInspector]
    public bool takeBox;

    void Start()
    {
        controller = GetComponent<Scr_Controller2D>();
        oldTiempoEnAire = TiempoEnAire;
        takeBox = false;
        anim = GetComponent<Animator> ();

        /*gravity = -(2 * AlturaSalto) / Mathf.Pow(TiempoEnAire, 2);
        jumpVelocity = Mathf.Abs(gravity) * TiempoEnAire;*/
    }


    void Update()
    {

        gravity = -(2 * AlturaSalto) / Mathf.Pow(TiempoEnAire, 2);
        jumpVelocity = Mathf.Abs(gravity) * TiempoEnAire;

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
        }

        // Para planear

        if (Input.GetKey(KeyCode.Space) && velocity.y < 0)
        {
            TiempoEnAire = TiempoPlaneando;
        }
        else
        {
            TiempoEnAire = oldTiempoEnAire;
        }

        if (Input.GetKey(KeyCode.E))
        {
            takeBox = true;

        }
        else
        {
            takeBox = false;
        }

        float targetVelocityX = input.x * VelocidadMovimiento;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

}
