using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public enum EstadosGrupo{
    EN_COLA,SIN_EMPEZAR,EJECUCION,TERMINADO
}

[Serializable]
public class OlaGrupo : MonoBehaviour
{
    private Ola ola_actual;
    private EstadosGrupo estado;
    private int enemigo_actual;
    //Tiempo de salida entre enemigos de un grupo
    public const float TIEMPO_SALIDA_ENEMIGO = 1f;
    private float tiempo_salida;
    public float Tiempo_salida { get => tiempo_salida; set => tiempo_salida = value; }
    public Ola OlaActual { get => ola_actual; set => ola_actual = value; }
    private PoolingEnemigos pooling;
    private OlaData data;
    public void Start(){
        Debug.Log(estado);
        pooling = PoolingEnemigos.Instance;
    }

    public void IniciarGrupo(){
        //Crear la ola, con la información de OlaData
        estado = EstadosGrupo.SIN_EMPEZAR;
    }

    public void Update(){
        if(estado != EstadosGrupo.EN_COLA)
        {
            if(Time.timeSinceLevelLoad > Tiempo_salida && estado == EstadosGrupo.SIN_EMPEZAR)
            {
                EmpezarGrupo();
                estado = EstadosGrupo.EJECUCION;
            }
        }
    }

    public void CrearGrupo(OlaData data){
        //enemigos = new List<Enemigo>();
        this.data = data;
        /* No se crea el grupo desde el inicio, 
        solo se agrega la información de los enemigos,
        cuando se pide un enemigo de la ola es donde se crea, 
        sino se recicla
        */
        //pooling.AgregarEnemigos(data);
    }

    public void EmpezarGrupo(){
        StartCoroutine(DespacharEnemigo(TIEMPO_SALIDA_ENEMIGO));
    }

    private IEnumerator DespacharEnemigo(float tiempo)
    {
        yield return new WaitForSecondsRealtime(tiempo);
        ActivarEnemigo();
    }

    private void ActivarEnemigo()
    {
        Enemigo e;

        if(enemigo_actual<data.Cantidad)
        {
            e = pooling.ObtenerEnemigo(data);//Oleadas.Instance.BuscarEnemigoDisponible();
            
            if(e!=null)
            {
                e.Activar();
                e.AsignarRuta(data);

                if(++enemigo_actual < data.Cantidad)
                {
                    StartCoroutine(DespacharEnemigo(TIEMPO_SALIDA_ENEMIGO));
                }
                else
                {
                    //Fin de la ola
                    //Despachar evento fin de ola, para que la oleado controle la 
                    //siguiente ola
                    estado = EstadosGrupo.TERMINADO;
                    ola_actual.DespacharGrupo();
                    //Acá se indica que todos los enemigos de un grupo fueron
                    //despachados, se debe tener un contador en la ola
                }
            }

        }        
    }
}