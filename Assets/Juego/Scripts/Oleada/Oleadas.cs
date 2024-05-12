using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oleadas : MonoBehaviour, IOlaMessageEvent
{
    private int indice_ola;
    void Start()
    {
        ReproducirOla();
    }

    public void ReproducirOla(){
        GameObject ola;
        
        if(indice_ola<this.transform.childCount)
        {
            ola = this.transform.GetChild(indice_ola).gameObject;
            if(ola!=null){
                ola.GetComponent<Ola>().EmpezarOla();
                indice_ola++;
            }
        }
        else{
            //Se terminan las olas, fin del nivel
            ManejadorEventos.TriggerEvent(EventosOla.FIN_OLEADAS,null);
        }
    }

    public void OlaMessage(EventosOla evt)
    {
        if(evt == EventosOla.OLA_TERMINADA)
        {
            ReproducirOla();
        }
    }
    
    public void OnEnable(){
        ManejadorEventos.AddEventListener(OlaMessage);
    }

    public void OnDisable(){
        ManejadorEventos.RemoveEventListener(OlaMessage);
    }
    
}
