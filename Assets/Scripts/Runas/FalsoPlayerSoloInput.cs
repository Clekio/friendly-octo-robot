using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class FalsoPlayerSoloInput : MonoBehaviour
{
    [SerializeField]
    public InputDevice input;

    [HideInInspector]
    public static FalsoPlayerSoloInput Instance;

    public Runas runas;

    private void Start()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);

        input = InputManager.ActiveDevice;

        //InputManager.OnDeviceAttached += inputDevice => input = inputDevice;
        //InputManager.OnDeviceDetached += inputDevice => input = InputManager.ActiveDevice;
        InputManager.OnActiveDeviceChanged += inputDevice => input = inputDevice;

        //InputManager.OnDeviceAttached += inputDevice => Debug.Log("Attached: " + inputDevice.Name);
        //InputManager.OnDeviceDetached += inputDevice => Debug.Log("Detached: " + inputDevice.Name);
        //InputManager.OnActiveDeviceChanged += inputDevice => Debug.Log("Switched: " + inputDevice.Name);
    }

    // Update is called once per frame
    void Update ()
    {
        //input = InputManager.ActiveDevice;

        if (input.LeftBumper.WasPressed)
        {
            runas.StartMagia(input.Name == "Keyboard/Mouse", transform.position);
        }
	}
}
