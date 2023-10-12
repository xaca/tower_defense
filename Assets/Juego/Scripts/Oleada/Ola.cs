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
    [MMInspectorButton("AgregarUnEnemigo")]
    /// a test button to spawn an object
    public bool PuedeAgregarButton;

    public void Start(){
        //Debug.Log("Se asignan los caminos");
        ConfigurarEnemigos();
    }

    //Crea la ola a partir de la información de configuración, nombre enemigo, 
    public void ConfigurarEnemigos(){
        MMPath temp;
        //Debug.Log(enemigos.Count);
        foreach(Enemigo e in enemigos){
            
            temp = e.Obj.GetComponent<MMPath>();
            //Debug.Log(temp);
            if(temp!=null)
            {
                temp.ReferenceMMPath = ruta;
                temp.Initialization();
            }
        }
    }

    public void AgregarEnemigo(OlaData e)
    {
        GameObject obj = GameObject.Find(e.Tipo.ToString());
        int cantidad = e.Cantidad;

        if(obj!=null)
        {
            for(int i=0;i<cantidad;i++)
            {
                //bool initialStatus = typeOfObject.activeSelf;
                //typeOfObject.SetActive(false);
                GameObject newGameObject = (GameObject)Instantiate(obj);
                newGameObject.transform.localScale = (newGameObject.transform.localScale * 0) + new Vector3(1,1,1);
                enemigos.Add(new Enemigo(newGameObject));
                //typeOfObject.SetActive(initialStatus);            
                SceneManager.MoveGameObjectToScene(newGameObject, this.gameObject.scene);
            }
        }
    }

    public void AgregarEnemigos(List<OlaData> e){

    }

    public void AgregarUnEnemigo(){
        AgregarEnemigo(new OlaData(TipoEnemigo.Koala));
    }
}
