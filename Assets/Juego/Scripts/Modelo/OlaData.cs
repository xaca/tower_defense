using System;
using UnityEngine;

[Serializable]
public class OlaData
{
    [SerializeField]
    private TipoEnemigo tipo;
    [SerializeField]
    private int cantidad;

    public OlaData(TipoEnemigo tipo){
        Tipo = tipo;
    }

    public OlaData(TipoEnemigo tipo,int cantidad){
        Tipo = tipo;
        Cantidad = cantidad;
    }

    public TipoEnemigo Tipo { get => tipo; set => tipo = value; }
    public int Cantidad { get => cantidad; set => cantidad = value; }
}