using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private List<Canvas> otrosCanvas = null;
    private List<Canvas> mainMenuCanvas = null;
    private TextMeshProUGUI opcionesTextMP;
    private Scene escenaActual;
    private bool esMostrarMenu = true;
    private bool esPrimeraOpcion = true;
    private bool esSegundaOpcion = false;
    private bool esTerceraOpcion = false;
    private void OnEnable()
    {
        EventHandler.MainMenuEvent += MainMenu;
    }
    private void OnDisable()
    {
        EventHandler.MainMenuEvent -= MainMenu;
    }
    // Start is called before the first frame update
    void Start()
    {
        EventHandler.CallMainMenuEvent();
        Canvas[] tmpCanvas = transform.parent.gameObject.GetComponentsInChildren<Canvas>();
        otrosCanvas = new List<Canvas>();
        mainMenuCanvas = new List<Canvas>();
        foreach (Canvas c in tmpCanvas)
        {
            if (c.gameObject.name != gameObject.name)
            {
                otrosCanvas.Add(c);
                c.enabled = false;
            }else
            {
                mainMenuCanvas.Add(c);
                c.enabled = true;
                if (c.gameObject.name == "CanvasMainMenu")
                {
                    foreach (TextMeshProUGUI tmpReg in c.GetComponentsInChildren<TextMeshProUGUI>())
                    {
                        if (tmpReg.gameObject.name == "Opciones")
                        {
                            opcionesTextMP = tmpReg;
                        }
                    }
                }
            }
        }
    }
    private void toggleOpcionMenu(bool esAbajo)
    {
        if (esAbajo)
        {
            if (esPrimeraOpcion)
            {
                esPrimeraOpcion = false;
                esSegundaOpcion = true;
            }else if (esSegundaOpcion)
            {
                esSegundaOpcion = false;
                esTerceraOpcion = true;
            }else if (esTerceraOpcion)
            {
                esTerceraOpcion = false;
                esPrimeraOpcion = true;
            }
        } else
        {
            if (esPrimeraOpcion)
            {
                esPrimeraOpcion = false;
                esTerceraOpcion = true;
            }
            else if (esSegundaOpcion)
            {
                esSegundaOpcion = false;
                esPrimeraOpcion = true;
            }
            else if (esTerceraOpcion)
            {
                esTerceraOpcion = false;
                esSegundaOpcion = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (esMostrarMenu)
        {
            setMostrarOtrosCanvas(false);
            refrescaOpcionSeleccionada();
            if (Input.GetButtonDown("Jump"))
            {
                StartCoroutine(PreEmpezarJuego());
            }
            if (Input.GetButtonDown("Vertical"))
            {
                bool esAbajo = Input.GetAxisRaw("Vertical") != 1;
                toggleOpcionMenu(esAbajo);
            }
        }
        else
        {
            if (Input.GetButtonDown("Cancel"))
            {
                EventHandler.CallMainMenuEvent();
            }
            setMostrarOtrosCanvas(true);
        }
    }

    private void MainMenu()
    {
        if (escenaActual.name != null)
        {
            esMostrarMenu = true;
            SceneManager.UnloadSceneAsync(escenaActual);
        }
    }

    private IEnumerator PreEmpezarJuego()
    {
        esMostrarMenu = false;
        yield return SceneManager.LoadSceneAsync(Settings.Escena1, LoadSceneMode.Additive);

        //Buscamos la escena que se acaba de cargar asincronamente
        escenaActual = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

        //Seteamos la nueva escena como la activa
        SceneManager.SetActiveScene(escenaActual);
        EventHandler.CallEmpezarJuegoEvent(esPrimeraOpcion, esSegundaOpcion, esTerceraOpcion);
    }

    private void setMostrarOtrosCanvas(bool esMostrarOtrosCanvas)
    {
        foreach(Canvas c in otrosCanvas)
        {
            c.enabled = esMostrarOtrosCanvas;
        }
        GetComponent<Canvas>().enabled = !esMostrarOtrosCanvas;
    }

    private void refrescaOpcionSeleccionada()
    {
        string texto = "";
        if (esPrimeraOpcion)
        {
            texto += ">";
        }else
        {
            texto += "  ";
        }
        texto += "Player vs Player\n";
        if (esSegundaOpcion)
        {
            texto += ">";
        }
        else
        {
            texto += "  ";
        }
        texto += "Player vs IA\n";
        if (esTerceraOpcion)
        {
            texto += ">";
        }
        else
        {
            texto += "  ";
        }
        texto += "IA vs IA";
        opcionesTextMP.text = texto;
    }
}
