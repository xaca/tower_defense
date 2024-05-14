using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EventosEnemigos{
    FIN_DE_RUTA, DESACTIVAR, RUTA_ACTUALIZADA
}
public enum EventosOla{
    DESPACHAR_ENEMIGO, OLA_TERMINADA, FIN_OLEADAS
}
public class ManejadorEventos : MonoBehaviour
{
    public delegate void OlaAction(EnemigoData data);
    private static event OlaAction eventos_ola;
    public delegate void EnemigoAction(EventosEnemigos e,GameObject go);
    private static event EnemigoAction eventos_enemigos;
    public delegate void OlaMessage(EventosOla evt);
    private static event OlaMessage eventos_mensajes;
    public static void TriggerEvent(EventosOla e,EnemigoData data)
    {
        if(e == EventosOla.DESPACHAR_ENEMIGO)
        {
            eventos_ola?.Invoke(data);
        }

        if(e == EventosOla.OLA_TERMINADA ||e == EventosOla.FIN_OLEADAS)
        {
            eventos_mensajes?.Invoke(e);
        }

    }
    public static void TriggerEvent(EventosEnemigos e, GameObject go)
    {
        eventos_enemigos?.Invoke(e,go);
    }
    
    public static void AddEventListener(OlaAction listener)
    {
        eventos_ola += listener;
    }

    public static void RemoveEventListener(OlaAction listener)
    {
        eventos_ola -= listener;
    }

    public static void AddEventListener(EnemigoAction listener)
    {
        eventos_enemigos += listener;
    }

    public static void RemoveEventListener(EnemigoAction listener)
    {
        eventos_enemigos -= listener;
    }

    public static void AddEventListener(OlaMessage listener)
    {
        eventos_mensajes += listener;
    }

    public static void RemoveEventListener(OlaMessage listener)
    {
        eventos_mensajes -= listener;
    }
       
}
