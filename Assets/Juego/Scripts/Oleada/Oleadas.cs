using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oleadas : MonoBehaviour
{
    public static Oleadas Instance { get; private set; }
    
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public void Start(){
        //Debug.Log("Se asignan los caminos");
    }

    public Enemigo BuscarEnemigoDisponible(TipoEnemigo tipo)
    {
        return null;
    }
}
