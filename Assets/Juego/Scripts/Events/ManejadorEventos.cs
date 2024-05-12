using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EventosEnemigos{
    FIN_DE_RUTA
}
public enum EventosOla{
    DESPACHAR_ENEMIGO, OLA_TERMINADA
}
public class ManejadorEventos : MonoBehaviour
{
    public delegate void OlaAction(EnemigoData data);
    private static event OlaAction eventos_ola;
    public delegate void EnemigoAction(GameObject go);
    private static event EnemigoAction eventos_enemigos;

    public static void TriggerEvent(EventosOla e,EnemigoData data)
    {
        if(e == EventosOla.DESPACHAR_ENEMIGO)
        {
            eventos_ola?.Invoke(data);
        }

        if(e == EventosOla.OLA_TERMINADA)
        {
            Debug.Log("Ola terminada, fin de nivel");
        }
    }
    public static void TriggerEvent(EventosEnemigos e, GameObject go)
    {
        if(e == EventosEnemigos.FIN_DE_RUTA)
        {
            eventos_enemigos?.Invoke(go);
        }
        
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
       
}
