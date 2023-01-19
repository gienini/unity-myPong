using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartidoManagerScript : MonoBehaviour
{
    [SerializeField] GameObject playerIzquierda = null;
    [SerializeField] GameObject playerDerecha = null;
    private void OnEnable()
    {
        EventHandler.EmpezarJuegoEvent += EmpiezaPartido;
    }
    private void OnDisable()
    {
        EventHandler.EmpezarJuegoEvent -= EmpiezaPartido;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EmpiezaPartido(bool esPrimeraOpcion, bool esSegundaOpcion, bool esTerceraOpcion)
    {
        playerIzquierda.GetComponent<PlayerScript>().enabled = false;
        playerIzquierda.GetComponent<IAScript>().enabled = false;
        playerDerecha.GetComponent<PlayerScript>().enabled = false;
        playerDerecha.GetComponent<IAScript>().enabled = false;
        //Primera = pvp, Segunda = pvIA, Tercera = IAvIA
        if (esPrimeraOpcion)
        {
            playerIzquierda.GetComponent<PlayerScript>().enabled = true;
            playerDerecha.GetComponent<PlayerScript>().enabled = true;
        }else if (esSegundaOpcion)
        {
            playerIzquierda.GetComponent<PlayerScript>().enabled = true;
            playerDerecha.GetComponent<IAScript>().enabled = true;
        }else if (esTerceraOpcion)
        {
            playerIzquierda.GetComponent<IAScript>().enabled = true;
            playerDerecha.GetComponent<IAScript>().enabled = true;
        }

    }
}
