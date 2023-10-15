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
    [SerializeField]
    //Esta es la ruta que seguiran todos los enemigos de la ola
    private MMPath ruta;
    [Header("Debug")]
    [MMInspectorButton("Agregar Un Enemigo")]
    /// a test button to spawn an object
    public bool PuedeAgregarButton;

    public void Start(){
        //Debug.Log("Se asignan los caminos");
        ruta = this.GetComponent<MMPath>();
        AgregarEnemigos();
        ConfigurarEnemigos();
    }

    //Crea la ola a partir de la información de configuración, nombre enemigo, 
    public void ConfigurarEnemigos(){
        MMPath temp;
        Debug.Log(enemigos.Count);
        //nueva = (MMPath)Instantiate(ruta);

        foreach(Enemigo e in enemigos){
            
            temp = e.Obj.GetComponent<Ruta>();
            //temp = e.Obj.AddComponent<MMPath>();
            
            if(temp!=null)
            {
                temp.ReferenceMMPath = ruta;
                temp.Initialization();
                e.Obj.SetActive(true);
            }
        }
        //ruta.gameObject.SetActive(false);
    }

    public void AgregarEnemigo(OlaData e)
    {
        GameObject obj = e.Prefab;//GameObject.Find(e.Tipo.ToString());
        int cantidad = e.Cantidad;

        if(obj!=null)
        {
            for(int i=0;i<cantidad;i++)
            {
                //bool initialStatus = typeOfObject.activeSelf;
                //typeOfObject.SetActive(false);
                obj.gameObject.SetActive(false);
                GameObject newGameObject = (GameObject)Instantiate(obj);
                obj.gameObject.SetActive(true);
                newGameObject.transform.localScale = (newGameObject.transform.localScale * 0) + new Vector3(1,1,1);
                enemigos.Add(new Enemigo(newGameObject));
                //typeOfObject.SetActive(initialStatus); 
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
