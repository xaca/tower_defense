using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class Ruta : MMPath
{
    //Para que esta clase funcione, la ruta debe tener cycle option only once
    public bool FinRuta(){
        return _endReached;
    }
    public void CambiarRuta(MMPath nueva_ruta){
        ReferenceMMPath = nueva_ruta;
    }
    public void ReiniciarRuta(){
        Initialization();
    }
    public Vector3 InicioRuta(){
        return _initialPosition;
    }
    /*
    Pendiente que la ruta se pueda mover independientemente
    protected virtual void OnDrawGizmos()
    {	
        #if UNITY_EDITOR
        //MMDebug.DrawGizmoPoint(this.transform.position,.2f,Color.yellow);
        #endif
    }*/
}
