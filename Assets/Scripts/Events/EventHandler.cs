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
}
