using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler
{
    public static event Action<bool> GolEvent;

    public static void CallGolEvent(bool esGolIzquierda)
    {
        if (GolEvent != null)
        {
            GolEvent(esGolIzquierda);
        }
    }

    public static event Action<bool, bool, bool> EmpezarJuegoEvent;

    public static void CallEmpezarJuegoEvent(bool esPrimeraOpcion, bool esSegundaOpcion, bool esTerceraOpcion)
    {
        if (EmpezarJuegoEvent != null)
        {
            EmpezarJuegoEvent(esPrimeraOpcion, esSegundaOpcion, esTerceraOpcion);
        }
    }

    public static event Action MainMenuEvent;
    public static void CallMainMenuEvent()
    {
        if (MainMenuEvent != null)
        {
            MainMenuEvent();
        }
    }
}
