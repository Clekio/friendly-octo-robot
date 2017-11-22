using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// En este script se almacenarán los datos de eventos y de partida del juego entero. Es un script persistente que se
// encuentra en un objeto vacío de la 1a escena del juego y que jamás se destruye hasta que se sale de este.

// Los datos de este script se almacenan en una archivo local, por lo que se guardan una vez se cierra el juego.

// Nombre del archivo: gameControl.dat

public class Scr_GameControl : MonoBehaviour
{
    public static Scr_GameControl control;

    public bool evento1;                         // Cuando se ha activado se hace TRUE
    public bool evento2;

    private void Awake()
    {
        if (control == null)                     // Comprabación duplicados
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }

        else if (control != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        evento1 = GameObject.Find("TroncoZona1").GetComponentInChildren<Scr_TroncoZona1Prologo>().evento1;
        evento2 = GameObject.Find("Escudo").GetComponent<Scr_EscudoPrologo>().evento2;
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameControl.dat");

        ControlData data = new ControlData();
        data.evento1 = evento1;
        data.evento2 = evento2;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gameControl.dat", FileMode.Open);

        ControlData data = (ControlData)bf.Deserialize(file);
        file.Close();

        evento1 = data.evento1;
        evento2 = data.evento2;
    }
}

[Serializable]
class ControlData
{
    public bool evento1;
    public bool evento2;
}