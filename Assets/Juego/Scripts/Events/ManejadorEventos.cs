using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum EventosOla{
    DESPACHAR_ENEMIGO, OLA_TERMINADA
}
public class ManejadorEventos : MonoBehaviour
{
    public delegate void OlaAction(EnemigoData data);
    private static event OlaAction eventos;
    
    public static void TriggerEvent(EventosOla e,EnemigoData data)
    {
        Debug.Log("Evento: "+e);

        if(e == EventosOla.DESPACHAR_ENEMIGO)
        {
            eventos?.Invoke(data);
        }
    }

    public static void AddEventListener(OlaAction listener)
    {
        eventos += listener;
    }

    public static void RemoveEventListener(OlaAction listener)
    {
        eventos -= listener;
    }
       
}
