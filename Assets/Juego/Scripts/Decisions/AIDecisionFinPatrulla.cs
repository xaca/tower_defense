using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class AIDecisionFinPatrulla : AIDecision
{
    private Ruta ruta;
    public override void Initialization(){
        
        ruta = GetComponent<Ruta>();
        if(ruta==null)
        {
            throw new Exception("La ruta no se asigno como componente del Character");
        }
    }
    public override bool Decide(){
        if(ruta.FinRuta())
        {
            //Eventos enemigos debe enviar un gameobject para poder modificarlo
            ManejadorEventos.TriggerEvent(EventosEnemigos.FIN_DE_RUTA,this.gameObject);
        }
        return ruta.FinRuta();
    }
}
