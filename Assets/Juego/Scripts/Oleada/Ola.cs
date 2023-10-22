using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class Ola : MonoBehaviour
{
    private Oleadas oleadas_nivel;
    [SerializeField]
    private List<OlaData> data;
    private List<OlaGrupo> ola_grupos;
    private GameObject grupos;
    private int grupo_actual;
    [SerializeField]
    //Esta es la ruta que seguiran todos los enemigos de la ola
    [Header("Debug")]
    [MMInspectorButton("AgregarUnEnemigo")]
    /// a test button to spawn an object
    public bool PuedeAgregarButton;

    public Oleadas OleadasNivel { get => oleadas_nivel; set => oleadas_nivel = value; }


    public void Start(){
        
    }

    //TODO: Temporalmente, se crearan las instancias necesarias para
    //representar todos los enemigos, esta pendiente realizar
    //pooling de objetos
    public void AlistarGrupo(){
        grupos = Oleadas.Instance.ContenedorGrupos;
        grupos.transform.SetParent(this.transform);
        CrearGrupos();
    }

    public void CrearGrupos(){
        OlaGrupo temp;
        ola_grupos = new List<OlaGrupo>();

        foreach(OlaData d in data)
        {
            temp = grupos.AddComponent<OlaGrupo>();
            temp.CrearGrupo(d);
            temp.OlaActual = this;
            temp.Tiempo_salida = d.TiempoSalida;
            ola_grupos.Add(temp);
        }
         
    }

    public void EmpezarOla(){
        AlistarGrupo();
        DespacharGrupo();
    }

    public void DespacharGrupo(){
        if(grupo_actual<ola_grupos.Count)
        {
            OlaGrupo temp = ola_grupos[grupo_actual];
            temp.IniciarGrupo();
            grupo_actual++;
        }
        else{
            //Se despacharon todos los grupos de la ola,
            //Se pide la siguiente ola
            OleadasNivel.DespacharOla();
        }
    }

    //Metodo de prueba desde el inspector que agrega Koalas
    public void AgregarUnEnemigo(){
        //AgregarEnemigo(new OlaData(TipoEnemigo.Koala));
    }

}
