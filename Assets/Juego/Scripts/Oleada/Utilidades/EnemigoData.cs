using UnityEngine;
using MoreMountains.Tools;
public enum EstadosEnemigo{
    FIN_RUTA, MUERTE
}
public class EnemigoData: MonoBehaviour{
    private int resistencia;
    private float velocidad;
    private int poder;
    private TipoEnemigo tipo;
    private MMPath ruta;

    public EnemigoData(TipoEnemigo tipo, MMPath ruta){
        this.Tipo = tipo;
        this.Ruta = ruta;
    }
    public EnemigoData(int resistencia, int velocidad, int poder, TipoEnemigo tipo, Ruta ruta){
        this.Resistencia = resistencia;
        this.Velocidad = velocidad;
        this.Poder = poder;
        this.Tipo = tipo;
        this.Ruta = ruta;
    }

    public int Resistencia { get => resistencia; set => resistencia = value; }
    public float Velocidad { get => velocidad; set => velocidad = value; }
    public int Poder { get => poder; set => poder = value; }
    public TipoEnemigo Tipo { get => tipo; set => tipo = value; }
    public MMPath Ruta { get => ruta; set => ruta = value; }
}