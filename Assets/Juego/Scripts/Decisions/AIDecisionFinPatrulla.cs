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
            ManejadorEventos.TriggerEvent(EventosEnemigos.FIN_DE_RUTA,this.gameObject);
            StartCoroutine(DesactivarEnemigo());
        }
        return ruta.FinRuta();
    }

    public IEnumerator DesactivarEnemigo(){
        yield return new WaitForSeconds(1f);
        ManejadorEventos.TriggerEvent(EventosEnemigos.DESACTIVAR,this.gameObject);
    }
}
