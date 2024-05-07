using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEngine.SceneManagement;
public class PoolingEnemigos : MonoBehaviour
{
    public static PoolingEnemigos Instance { get; private set; }
    [SerializeField]
    private GameObject prefabEnemigo;
    private List<Enemigo> enemigos;
    [MMFInspectorButton("ActivarEnemigo")]
    public bool crearEnemigo;
    public int indice;//Se usa para ubicar las copias de los enemigos
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    void Start()
    {
        enemigos = new List<Enemigo>();
    }

    public void AgregarEnemigos(OlaData e)
    {
        int cantidad = e.Cantidad;
        for(int i=0;i<cantidad;i++)
        {
            //Pendiente agregar la ruta al enemigo
            AgregarEnemigo(e);          
        }
        
    }

    public Enemigo AgregarEnemigo(OlaData data){
        
        GameObject go;
        Enemigo enemigo;
        go = Resources.Load(data.Tipo.ToString()) as GameObject;

        if(go!=null)
        {
            //GameObject newGameObject = (GameObject)Instantiate(obj);
            //obj.gameObject.SetActive(false);
            go.transform.localScale = (go.transform.localScale * 0) + new Vector3(1,1,1);
            go.transform.localPosition = (go.transform.localPosition)+new Vector3(0,indice++,0);
            
            enemigo = new Enemigo(go);
            enemigo.Data = data;
            enemigos.Add(enemigo);
            
            SceneManager.MoveGameObjectToScene(go, this.gameObject.scene);
            go.transform.SetParent(this.gameObject.transform); 
            
            return enemigo;
        }
        return null;
    }

    public Enemigo ObtenerEnemigo(OlaData data)
    {
        for (int i = 0; i < enemigos.Count; i++)
        {
            if (!enemigos[i].Obj.activeInHierarchy)
            {
                if(enemigos[i].Data.Tipo == data.Tipo)
                {
                    return enemigos[i];
                }
            }
        }

        return AgregarEnemigo(data);
    }

}
