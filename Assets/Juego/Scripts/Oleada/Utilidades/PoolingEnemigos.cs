using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine.SceneManagement;
public class PoolingEnemigos : MonoBehaviour,IOLaDataEvent
{
    public static PoolingEnemigos Instance { get; private set; }
    [SerializeField]
    private List<OlaData> prueba;
    private int indice;//Se usa para ubicar las copias de los enemigos
    [Tooltip("Espacio entre enemigos")]
    [SerializeField]
    private int espacio;
    [SerializeField]
    private GameObject grupo;
    [MMFInspectorButton("ActivarEnemigo")]
    private bool activarEnemigo;
  
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

    public void Start(){
        espacio = 10;
        foreach(OlaData e in prueba)
        {
            AgregarEnemigos(e);
        }
    }

    public void AgregarEnemigos(OlaData e)
    {
        int cantidad = e.Cantidad;
        EnemigoData temp;

        for(int i=0;i<cantidad;i++)
        {
            temp = new EnemigoData(e.Tipo,e.Ruta);
            AgregarEnemigo(temp);          
        }
        
    }

    /*
        @param data: Información del enemigo a crear, no tiene que estar completa, obligatorio tener, tipo y cantidad;
    */
    public GameObject AgregarEnemigo(EnemigoData data){
        
        GameObject go;
        go = Instantiate(Resources.Load(data.Tipo.ToString()) as GameObject);
        MMPath ruta = data.Ruta;
        Vector3 pos_inicial = (ruta==null)?Vector3.zero:data.Ruta.CurrentPoint();

        if(go!=null)
        {
            //GameObject newGameObject = (GameObject)Instantiate(obj);
            go.SetActive(false);
            go.transform.localScale = (go.transform.localScale * 0) + new Vector3(1,1,1);
            //go.transform.localPosition = (go.transform.localPosition)-new Vector3(indice++*espacio,0,0);
            go.transform.localPosition = pos_inicial-new Vector3(indice++*espacio,0,0);
            SceneManager.MoveGameObjectToScene(go, grupo.gameObject.scene);
            go.transform.SetParent(grupo.gameObject.transform); 
            go.transform.localScale = new Vector3(3f,3f,1);
            //Temporal, se debe crear con una ruta para probar, pero esta
            //gestion no corresponde al pooling
            go.GetComponent<AIBrain>().BrainActive = false;
            return go;
        }
        return null;
    }
    
    public GameObject ObtenerEnemigo(EnemigoData data)
    {
        string tipo;
        bool activo;
        
        foreach(Transform child in grupo.transform)
        {
            //Se agrega el (Clone) para que coincida con el nombre del prefab duplicado
            tipo = child.gameObject.name;
            activo = child.gameObject.activeInHierarchy;
            
            if (!activo && tipo.Equals(data.Tipo.ToString()+"(Clone)"))
            {
                return child.gameObject;
            }
        }

        return AgregarEnemigo(data);
    }

    public void OnEnable()
    {
        ManejadorEventos.AddEventListener(OnOlaData);
    }
    public void OnDisable()
    {
        ManejadorEventos.RemoveEventListener(OnOlaData);
    }

    public void OnOlaData(EnemigoData enemigo_data)
    {
        //Se recibe la información de la ola
        //Se guarda en la lista de datos
        GameObject enemigo = ObtenerEnemigo(enemigo_data);
        enemigo.GetComponent<AIBrain>().BrainActive = true;
        enemigo.SetActive(true);
        enemigo.GetComponent<Ruta>().ReferenceMMPath = enemigo_data.Ruta;
        Debug.Log("Recibiendo datos de la ola");
    }
}
