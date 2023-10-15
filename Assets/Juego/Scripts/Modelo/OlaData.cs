using System;
using UnityEngine;

[Serializable]
public class OlaData
{
    private TipoEnemigo tipo;
    [SerializeField]
    private int cantidad;
    [SerializeField]
    private GameObject prefab;

    public OlaData(TipoEnemigo tipo){
        Tipo = tipo;
    }

    public OlaData(TipoEnemigo tipo,int cantidad){
        Tipo = tipo;
        Cantidad = cantidad;
    }

    public OlaData(GameObject prefab,int cantidad){
        Prefab = prefab;
        Cantidad = cantidad;
    }

    public TipoEnemigo Tipo { get => tipo; set => tipo = value; }
    public int Cantidad { get => cantidad; set => cantidad = value; }
    public GameObject Prefab { get => prefab; set => prefab = value; }
}