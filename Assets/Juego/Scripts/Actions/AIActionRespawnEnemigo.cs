using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using Unity.VisualScripting;
using UnityEngine;

public class AIActionRespawnEnemigo : AIAction
{
    //[SerializeField]
    private GameObject enemigo;
    private Vector3 inicio;
    private MMPath path;
    
    public override void Initialization (){
        enemigo = this.transform.parent.gameObject;
        //if(enemigo!=null){
            //inicio = enemigo.transform.position;
            path = GetComponent<MMPath>();
            Debug.Log("Path: "+path.PathElements.Count);
            //inicio = path.PathElements[0].PathElementPosition;
        //}
    }

    public override void PerformAction()
	{
        VolverAlInicio();
    }

    public void VolverAlInicio(){
        
        //c.RespawnAt(inicio,Character.FacingDirections.North);
        //c.transform.position = inicio;
        //Character character = this.GetComponentInParent<Character>();
        //character.RespawnAt(inicio,Character.FacingDirections.East);
        path.Initialization();
        MMEventManager.TriggerEvent(new MMGameEvent(EnemigoEstados.RESPANW));
        /*character.Freeze();*/
        enemigo.SetActive(false);
                
    }

}
