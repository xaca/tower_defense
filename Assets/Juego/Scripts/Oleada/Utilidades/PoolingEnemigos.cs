using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine.SceneManagement;
public class PoolingEnemigos : MonoBehaviour,IOLaDataEvent,IEnemigoEvent
{
    public static PoolingEnemigos Instance { get; private set; }
    [SerializeField]
    private List<OlaData> datos_inicio;
    private int indice;//Se usa para ubicar las copias de los enemigos
    [Tooltip("Espacio entre enemigos")]
    [SerializeField]
    private int espacio;
    [Tooltip("Contenedor del pooling de enemigos")]
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
        foreach(OlaData e in datos_inicio)
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
        ManejadorEventos.AddEventListener(OnEnemigoAction);
    }
    public void OnDisable()
    {
        ManejadorEventos.RemoveEventListener(OnOlaData);
        ManejadorEventos.RemoveEventListener(OnEnemigoAction);
    }

    public void OnEnemigoAction(EventosEnemigos e,GameObject go)
    {
        if(e == EventosEnemigos.FIN_DE_RUTA)
        {            
            //Se ubican por fuera del escenario
            go.transform.localPosition = new Vector3(-54,0,0);
        }

        if(e == EventosEnemigos.DESACTIVAR)
        {
            go.SetActive(false);
        }
    }

    public void OnOlaData(EnemigoData enemigo_data)
    {
        Ruta ruta;
        
        GameObject enemigo = ObtenerEnemigo(enemigo_data);
        ruta = enemigo.GetComponent<Ruta>();
        ruta.CambiarRuta(enemigo_data.Ruta);
        ruta.ReiniciarRuta();
        enemigo.SetActive(true);
        ManejadorEventos.TriggerEvent(EventosEnemigos.RUTA_ACTUALIZADA,enemigo);
        //enemigo.transform.localPosition = ruta.InicioRuta();
    }
}
