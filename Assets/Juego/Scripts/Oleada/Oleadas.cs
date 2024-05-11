using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oleadas : MonoBehaviour
{
    [SerializeField]
    private List<Ola> olas;
    private int ola_actual;


    public void Start(){
        //DespacharOla();
    }

    public void DespacharOla(){
        Ola ola;
        if(ola_actual < olas.Count)
        {
            
            ola_actual++; //Acá se acualiza el contador de olas
            Debug.Log("Ola "+ola_actual+"/"+olas.Count);
        }
        else{
            //Se termina la partida indicar que gano y mostrar ventana fin de nivel
            Debug.Log("Fin de oleadas");
        }

    }

}
