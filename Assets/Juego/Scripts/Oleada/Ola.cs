using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class Ola : MonoBehaviour
{
    [SerializeField]
    private List<OlaData> data;
    private GameObject grupos;

    [SerializeField]
    //Esta es la ruta que seguiran todos los enemigos de la ola
    [Header("Debug")]
    [MMInspectorButton("AgregarUnEnemigo")]
    /// a test button to spawn an object
    public bool PuedeAgregarButton;

    public void Start(){
        grupos = new GameObject();
        grupos.transform.SetParent(this.transform);
        CrearGrupos();
    }

    public void CrearGrupos(){
        OlaGrupo temp;

        foreach(OlaData d in data)
        {
            temp = grupos.AddComponent<OlaGrupo>();
            temp.CrearGrupo(d);
            temp.Tiempo_salida = d.TiempoSalida;
        }
    }

    //Metodo de prueba desde el inspector que agrega Koalas
    public void AgregarUnEnemigo(){
        //AgregarEnemigo(new OlaData(TipoEnemigo.Koala));
    }

}
