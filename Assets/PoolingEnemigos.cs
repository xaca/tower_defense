using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
public class PoolingEnemigos : MonoBehaviour
{
    public static PoolingEnemigos instancia;
    [SerializeField]
    private GameObject prefabEnemigo;
    [SerializeField]
    private int cantidadInicial = 10;
    private List<GameObject> enemigos;
    [MMFInspectorButton("ActivarEnemigo")]
    public bool crearEnemigo;
    private void Awake()
    {
        instancia = this;
    }

    void Start()
    {
        enemigos = new List<GameObject>();
        for (int i = 0; i < cantidadInicial; i++)
        {
            GameObject enemigo = Instantiate(prefabEnemigo, transform);
            enemigo.SetActive(false);
            enemigos.Add(enemigo);
        }
    }

    public GameObject ObtenerEnemigo()
    {
        for (int i = 0; i < enemigos.Count; i++)
        {
            if (!enemigos[i].activeInHierarchy)
            {
                return enemigos[i];
            }
        }

        GameObject enemigo = Instantiate(prefabEnemigo, transform);
        enemigo.SetActive(false);
        enemigos.Add(enemigo);
        return enemigo;
    }

    public void ActivarEnemigo()
    {
        GameObject enemigo = ObtenerEnemigo();
        enemigo.SetActive(true);
    }
}
