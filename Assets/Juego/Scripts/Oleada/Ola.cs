using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public enum TipoEnemigo{
    Koala, Ninja, GrasslandsHornsBlue
}
public class Ola : MonoBehaviour
{
    
    [SerializeField]
    private List<OlaData> datos;
    //Datos de un subgrupo de enemigos de la ola
    private List<EnemigoData> enemigos;
    [Tooltip("Indica el grupo actual de la ola")]
    private int grupo_actual;
    private int contador_enemigos;
    [Header("Control Tiempo")]
    [Tooltip("Importante para que todos los enemigos no salgan al tiempo")]
    [SerializeField]
    private int tiempo_despacho;
    private float tiempo_total_ola;
    [Tooltip("Tiempo de espera entre olas")]
    [SerializeField]
    private float tiempo_entre_olas;
    public void EmpezarOla(){
        tiempo_entre_olas = tiempo_entre_olas>0?tiempo_entre_olas:5;
        tiempo_despacho = tiempo_despacho>0?tiempo_despacho:2;
        CalcularTiempoTotal();
        DespacharOla();
    }

    public void CalcularTiempoTotal(){
        float tiempo = 0;
        foreach(OlaData data in datos){
            tiempo+=data.TiempoSalida;
        }
        tiempo_total_ola = tiempo;
    }
    public void CrearEnemigos(OlaData datos_grupo){
        EnemigoData temp;
        enemigos = null;
        enemigos = new List<EnemigoData>();
        for(int i=0;i<datos_grupo.Cantidad;i++){
            //Se crea un enemigo con la información de la ola
            temp = new EnemigoData(datos_grupo.Tipo,datos_grupo.Ruta);
            enemigos.Add(temp);
        }
        contador_enemigos = 0;
    }

    public void DespacharEnemigos(){
        EnemigoData enemigo_data;
        if(contador_enemigos<enemigos.Count){
            enemigo_data = enemigos[contador_enemigos];
            contador_enemigos++;
            StartCoroutine(DespacharEnemigo(enemigo_data));
        }
        else{
            grupo_actual++;
            DespacharOla();
        }
    }
    IEnumerator EsperarTiempoSalida(float tiempo,OlaData data){
        yield return new WaitForSeconds(tiempo);
        CrearEnemigos(data);
        DespacharEnemigos();
    }
    public void DespacharOla(){
        Debug.Log("Despachar grupo");
        Debug.Log(grupo_actual+" "+datos.Count);
        OlaData data;

        if(grupo_actual<datos.Count)
        {
            data = datos[grupo_actual];
            StartCoroutine(EsperarTiempoSalida(data.TiempoSalida,data));
        }
        else{
            //Se despacharon todos los grupos de la ola,
            //Se pide la siguiente ola
            //if(Time.timeSinceLevelLoad>(tiempo_total_ola+tiempo_entre_olas)){}
            ManejadorEventos.TriggerEvent(EventosOla.OLA_TERMINADA,null);
        }
    }

    /*Se pide al pooling que despache los enemigos de la ola actual
      Se hace desde la hola, disparando un evento que el pooling escucha
      y se hace con una coroutina*/
    IEnumerator DespacharEnemigo(EnemigoData data){
        yield return new WaitForSeconds(tiempo_despacho);
        ManejadorEventos.TriggerEvent(EventosOla.DESPACHAR_ENEMIGO,data);
        DespacharEnemigos();//Se pide el siguiente enemigo
    }
        
}
