using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;
public class Revivir : MonoBehaviour, MMEventListener<MMGameEvent>
{
    [SerializeField]
    private GameObject personaje;

    void OnEnable()
    {
        this.MMEventStartListening<MMGameEvent>();
    }
    void OnDisable()
    {
        this.MMEventStopListening<MMGameEvent>();
    }

    public void OnMMEvent(MMGameEvent e)
    {
        
        if(e.EventName == EnemigoEstados.MUERTO)
        {
            Debug.Log("El personaje murio, se va a revivir");
            StartCoroutine(RevivirPersonaje());
        }
    }

    IEnumerator RevivirPersonaje(){
        yield return new WaitForSeconds(2);
        GameManager.Instance.CurrentLives--;
    }
}
