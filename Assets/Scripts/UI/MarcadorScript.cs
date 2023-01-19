using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarcadorScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI marcadorText = null;
    private int contadorIzquierda = 0;
    private int contadorDerecha = 0;
    private void Start()
    {
        actualizaMarcador();
    }
    private void OnEnable()
    {
        EventHandler.GolEvent += MarcaGol;
        EventHandler.EmpezarJuegoEvent += EmpezarJuego;
    }
    private void OnDisable()
    {
        EventHandler.GolEvent -= MarcaGol;
        EventHandler.EmpezarJuegoEvent -= EmpezarJuego;
    }
    private void EmpezarJuego(bool esPrimeraOpcion, bool esSegundaOpcion, bool esTerceraOpcion)
    {
        contadorIzquierda = 0;
        contadorDerecha = 0;
        actualizaMarcador();
    }
    private void MarcaGol(bool esGolIzquierda)
    {
        if (esGolIzquierda)
        {
            contadorIzquierda += 1;
        }else
        {
            contadorDerecha += 1;
        }
        actualizaMarcador();
    }
    private void actualizaMarcador()
    {
        marcadorText.text = contadorIzquierda + " - " + contadorDerecha;
    }
}
