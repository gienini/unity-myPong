using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float yInput = 0f;
    private float yInputAnterior= 0f;
    private new Rigidbody2D rigidbody2D;
    private float velocidadMovimientoLocal = Settings.velocidadMovimientoPlayer;
    private bool esDireccionArriba = true;
    private int milis;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (yInput != Input.GetAxisRaw("Vertical"))
        {
            milis = Environment.TickCount;
        }
        yInput = Input.GetAxisRaw("Vertical");
        
    }
    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (yInput != 0f)
        {
            //Estaba en movimiento
            if (yInput == yInputAnterior && milis < (Environment.TickCount+200))
            {
                //Se sigue apretando el boton durante 0,2s = Acelera un 20%
                milis = Environment.TickCount;
                velocidadMovimientoLocal = velocidadMovimientoLocal * Settings.aceleracionLinealPlayer;
            }
            else
            {
                //Se dejo de apretar = La velocidad vuelve a valor de serie
                velocidadMovimientoLocal = Settings.velocidadMovimientoPlayer;
            }

        }
        yInputAnterior = yInput;
        // Se calcula el movimiento, deltaTime es el tiempo que tardara en ejecutarse el ciclo de update
        Vector2 move = new Vector2(0f, yInput * velocidadMovimientoLocal * Time.deltaTime);
        // Se aplica el movimiento desde posicion actual, se le suma el recorrido calculado
        rigidbody2D.MovePosition(rigidbody2D.position + move);
    }
}
