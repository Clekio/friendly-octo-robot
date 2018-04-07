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
            Destroy(this.gameObject);

        input = InputManager.ActiveDevice;
        
        Debug.Log(input.Name);
        
        InputManager.OnDeviceAttached += inputDevice => input = inputDevice;
        InputManager.OnDeviceDetached += inputDevice => input = InputManager.ActiveDevice;
        InputManager.OnActiveDeviceChanged += inputDevice => input = inputDevice;
    }

    // Update is called once per frame
    void Update ()
    {
        if (input.LeftBumper.WasPressed)
        {
            runas.StartMagia(input.Name == "Keyboard/Mouse", transform.position);
        }
	}
}
