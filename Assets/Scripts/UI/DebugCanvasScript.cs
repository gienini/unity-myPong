using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugCanvasScript : MonoBehaviour
{
    
    private Rigidbody2D bodyPelota;
    private Rigidbody2D bodyPalaIzquierda;
    private Rigidbody2D bodyPalaDerecha;
    [SerializeField]
    private TextMeshProUGUI velocidadPelotaXText = null;
    [SerializeField]
    private TextMeshProUGUI velocidadPelotaYText = null;
    [SerializeField]
    private TextMeshProUGUI velocidadPalaIzquierdaXText = null;
    [SerializeField]
    private TextMeshProUGUI velocidadPalaIzquierdaYText = null;
    [SerializeField]
    private TextMeshProUGUI velocidadPalaDerechaXText = null;
    [SerializeField]
    private TextMeshProUGUI velocidadPalaDerechaYText = null;

    private void Start()
    {
        
    }
    private void Update()
    {
        buscaYAsignaObjetos();


        if (bodyPelota != null)
        {
            velocidadPelotaXText.text = "PELOTA-VelocidadX:" + bodyPelota.velocity.x;
            velocidadPelotaYText.text = "PELOTA-VelocidadY:" + bodyPelota.velocity.y;
        }
        if (bodyPalaDerecha != null)
        {
            velocidadPalaDerechaXText.text = "PALAD-PosicionX:" + bodyPalaDerecha.transform.position.x;
            velocidadPalaDerechaYText.text = "PALAD-PosicionY:" + bodyPalaDerecha.transform.position.y;
        }
        if (bodyPalaIzquierda != null)
        {
            velocidadPalaIzquierdaXText.text = "PALAI-PosicionX:" + bodyPalaIzquierda.transform.position.x;
            velocidadPalaIzquierdaYText.text = "PALAI-PosicionY:" + bodyPalaIzquierda.transform.position.y;
        }
    }

    private void buscaYAsignaObjetos()
    {

        if (bodyPelota == null && FindObjectOfType<PelotaScript>() != null)
        {
            bodyPelota = FindObjectOfType<PelotaScript>().gameObject.GetComponent<Rigidbody2D>();
        }
        if (bodyPalaIzquierda == null || bodyPalaDerecha == null)
        {
            GameObject[] palas = GameObject.FindGameObjectsWithTag(Settings.TagPalas);
            if (palas.Length > 0)
            {
                if (palas[0].GetComponent<Rigidbody2D>().transform.position.x > palas[1].GetComponent<Rigidbody2D>().transform.position.x)
                {
                    bodyPalaDerecha = palas[0].GetComponent<Rigidbody2D>();
                    bodyPalaIzquierda = palas[1].GetComponent<Rigidbody2D>();
                }
                else
                {
                    bodyPalaDerecha = palas[1].GetComponent<Rigidbody2D>();
                    bodyPalaIzquierda = palas[0].GetComponent<Rigidbody2D>();
                }
            }

        }
    }
}
