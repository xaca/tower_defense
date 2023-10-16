using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ola : MonoBehaviour
{
    [SerializeField]
    private List<OlaData> data;
    private List<Enemigo> enemigos;
    private int enemigo_actual;
    public const float TIEMPO_SALIDA_ENEMIGO = 10f;
    [SerializeField]
    //Esta es la ruta que seguiran todos los enemigos de la ola
    [Header("Debug")]
    [MMInspectorButton("Agregar Un Enemigo")]
    /// a test button to spawn an object
    public bool PuedeAgregarButton;

    public void Start(){
        //Debug.Log("Se asignan los caminos");
        AgregarEnemigos();
        StartCoroutine(DespacharEnemigo(TIEMPO_SALIDA_ENEMIGO));
    }

    private IEnumerator DespacharEnemigo(float tiempo)
    {
        Debug.Log("Despachar");
        yield return new WaitForSecondsRealtime(tiempo);
        ActivarEnemigo();
    }

    private void ActivarEnemigo()
    {
        Enemigo e;
        AIBrain brain;

        if(enemigo_actual<enemigos.Count)
        {
            e = enemigos[enemigo_actual];
            e.Obj.gameObject.SetActive(true);
            /*brain = e.Obj.GetComponent<AIBrain>();
            if(brain!=null)
            {
                brain.BrainActive = true;
            }*/
            if(++enemigo_actual < enemigos.Count)
            {
                StartCoroutine(DespacharEnemigo(TIEMPO_SALIDA_ENEMIGO));
            }
            else
            {
                //Fin de la ola
                //Despachar evento fin de ola, para que la oleado controle la 
                //siguiente ola
            }
        }        
    }

    //Crea la ola a partir de la información de configuración, nombre enemigo, 
    public void ConfigurarRuta(Enemigo enemigo,MMPath ruta)
    {
        MMPath temp = enemigo.Obj.GetComponent<Ruta>();
        if(temp!=null)
        {
            temp.ReferenceMMPath = ruta;
            temp.Initialization();
        }
        
    }

    public void AgregarEnemigo(OlaData e)
    {
        GameObject obj = e.Prefab;//GameObject.Find(e.Tipo.ToString());
        Enemigo enemigo;
        AIBrain brain;
        int cantidad = e.Cantidad;

        if(obj!=null)
        {
            for(int i=0;i<cantidad;i++)
            {
                
                //obj.gameObject.SetActive(false);
                GameObject newGameObject = (GameObject)Instantiate(obj);
                obj.gameObject.SetActive(false);
                newGameObject.transform.localScale = (newGameObject.transform.localScale * 0) + new Vector3(1,1,1);
                newGameObject.transform.localPosition = (newGameObject.transform.localPosition)+new Vector3(0,i,0);
                /*brain = newGameObject.GetComponent<AIBrain>();
                if(brain!=null)
                {   //No funciono, revisar ubicación de los scripts
                    //Debug.Log("Desactivar cerebro");
                    brain.BrainActive = false;
                }*/
                enemigo = new Enemigo(newGameObject);
                enemigos.Add(enemigo);
                ConfigurarRuta(enemigo,e.Ruta);
                SceneManager.MoveGameObjectToScene(newGameObject, this.gameObject.scene);
                newGameObject.transform.SetParent(this.gameObject.transform);          
            }
        }
    }

    public void AgregarEnemigos(List<OlaData> e){

    }

    //Crear la ola, con la información de OlaData
    public void AgregarEnemigos(){
        
        enemigos = new List<Enemigo>();

        foreach(OlaData d in data)
        {
            AgregarEnemigo(d);
        }
    }

    //Metodo de prueba desde el inspector que agrega Koalas
    public void AgregarUnEnemigo(){
        AgregarEnemigo(new OlaData(TipoEnemigo.Koala));
    }

}
