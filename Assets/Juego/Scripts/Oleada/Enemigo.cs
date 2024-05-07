using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

[Serializable]
//Identifica el prefab correspondiente de tipo de enemigo
public enum TipoEnemigo{
    Koala, Ninja, GrassLandHornsBlue
}

[Serializable]
public class Enemigo
{
    [SerializeField]
    private GameObject obj;
    private OlaData data;
    public Enemigo(GameObject obj){
        Obj = obj;
    }
    public GameObject Obj { get => obj; set => obj = value; }
    public OlaData Data { get => data; set => data = value; }

    //public MMPath Camino { get => camino; set => camino = value; }


    public void Activar(){
        if(obj!=null){
            obj.SetActive(true);
        }
    }

    public void AsignarRuta(OlaData data){
        MMPath temp = obj.GetComponent<Ruta>();
        if(temp!=null)
        {
            temp.ReferenceMMPath = data.Ruta;
            //temp.Initialization();//No es necesario inicializar una ruta asignada
        }
    }
}

