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
        EventHandler.GolEvent += marcaGol;
    }
    private void OnDisable()
    {
        EventHandler.GolEvent -= marcaGol;
    }

    private void marcaGol(bool esGolIzquierda)
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
