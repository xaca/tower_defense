using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class AIActionPatrullarOla : AIActionMovePatrol2D
{
    private float xr, yr;
    protected override void InitializePatrol(){
        base.InitializePatrol();
        //xr = Random.Range(0,1);
        //yr = Random.Range(0,1);
    }

    protected override void Patrol(){
        try{
            //Lanza excepción si el MMPath no se ha asignado
            if(_mmPath!=null){
                base.Patrol(); 
            }
            else{
                //Debug.Log("No hay ruta asignada");
            }
        }catch(NullReferenceException e){
            //Debug.Log("La ruta MMPath no se ha asignado");
            //Debug.LogError(e);
        }
    }
}
