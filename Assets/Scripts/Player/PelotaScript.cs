using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PelotaScript : MonoBehaviour
{
    private bool direccionSaqueX;
    private int direccionSaqueY;
    private new Rigidbody2D rigidbody2D;
    private float velocidadX;
    private float velocidadY;
    private Vector2 posicionInicial;
    [SerializeField]
    private Rigidbody2D bodyIzquierda = null;
    [SerializeField]
    private Rigidbody2D bodyDerecha = null;
    [SerializeField]
    private Rigidbody2D lineaGolDerecha = null;
    [SerializeField]
    private Rigidbody2D lineaGolIzquierda = null;
    private int milisStart;
    private bool esFreezeGol = false;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        posicionInicial = rigidbody2D.position;
        
    }
    private void OnEnable()
    {
        EventHandler.EmpezarJuegoEvent += EmpiezaPartido;
        EventHandler.MainMenuEvent += MenuPrincipal;
    }
    private void OnDisable()
    {
        EventHandler.EmpezarJuegoEvent -= EmpiezaPartido;
        EventHandler.MainMenuEvent -= MenuPrincipal;
    }
    private void decideSaque()
    {
        decideSaque((Environment.TickCount % 2) == 0);
    }
    private void decideSaque(bool direccionSaqueX)
    {
        direccionSaqueY = Environment.TickCount % 5;
        //Existen 2 delimitadores para el valor de velocidadY. Dividimos la porteria en 5 partes, donde la primera y la ultima son los extremos de la misma y el saque debe dirigirse entre ellas
        // Y == 1 Es la esquina superior y Y== 0 es la esquina inferior.
        if (direccionSaqueX)
        {
            velocidadX = 1f;
        }
        else
        {
            velocidadX = -1f;
        }
        switch (direccionSaqueY)
        {
            case 0:
            case 1:
                velocidadY = 0.8f;
                break;
            case 2:
                velocidadY = 0f;
                break;
            case 3:
            case 4:
                velocidadY = -0.8f;
                break;
        }
    }
    private void realizaSaque()
    {
        
        milisStart = Environment.TickCount;
        rigidbody2D.velocity = new Vector2(velocidadX, velocidadY) * Settings.velocidadMovimientoPelota;
    }
    // Start is called before the first frame update
    //void Start()
    //{
    //    decideSaque();
    //    realizaSaque();
    //}

    private void aplicaVelocidadPelota()
    {
        if (!esFreezeGol)
        {
            if (rigidbody2D.velocity.x > 0)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x + Settings.aceleracionLinealPelota, rigidbody2D.velocity.y);
            }
            else
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x - Settings.aceleracionLinealPelota, rigidbody2D.velocity.y);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            rigidbody2D.position = posicionInicial;
            decideSaque();
            realizaSaque();
        }
        
    }
    private IEnumerator rutinaGol(bool esGolIzquierda)
    {
        EventHandler.CallGolEvent(esGolIzquierda);
        rigidbody2D.position = posicionInicial;
        rigidbody2D.velocity = new Vector2(0, 0);
        esFreezeGol = true;
        yield return new WaitForSeconds(4f);
        decideSaque(esGolIzquierda);
        realizaSaque();
        esFreezeGol = false;
    }

    private void FixedUpdate()
    {
        if (Environment.TickCount > (milisStart + 500))
        {
            milisStart = Environment.TickCount;
            aplicaVelocidadPelota();
        }
    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos,
                float racketHeight)
    {
        // ascii art:
        // ||  1 <- at the top of the racket
        // ||
        // ||  0 <- at the middle of the racket
        // ||
        // || -1 <- at the bottom of the racket
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Note: 'col' holds the collision information. If the
        // Ball collided with a racket, then:
        //   col.gameObject is the racket
        //   col.transform.position is the racket's position
        //   col.collider is the racket's collider

        // Hit the left Racket?
        if (bodyIzquierda != null && bodyIzquierda.gameObject.name == col.gameObject.name)
        {
            // Calculate hit Factor
            float y = hitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(Math.Abs(rigidbody2D.velocity.x), y);
            
            // Set Velocity with dir * speed
            rigidbody2D.velocity = dir;
        }

        // Hit the right Racket?
        if (bodyDerecha != null && bodyDerecha.gameObject.name == col.gameObject.name)
        {
            // Calculate hit Factor
            float y = hitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(-Math.Abs(rigidbody2D.velocity.x), y);

            // Set Velocity with dir * speed
            rigidbody2D.velocity = dir;
        }
        if (lineaGolDerecha.gameObject.name == col.gameObject.name)
        {
            StartCoroutine(rutinaGol(false));
        }else if (lineaGolIzquierda.gameObject.name == col.gameObject.name)
        {
            StartCoroutine(rutinaGol(true));
        }
    }
    private void EmpiezaPartido(bool esPrimeraOpcion, bool esSegundaOpcion, bool esTerceraOpcion)
    {
        esFreezeGol = false;
        rigidbody2D.position = posicionInicial;
        decideSaque();
        realizaSaque();
    }

    private void MenuPrincipal()
    {
        esFreezeGol = true;
        rigidbody2D.position = posicionInicial;
        rigidbody2D.velocity = new Vector2(0, 0);
    }

}
