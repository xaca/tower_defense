using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class AIDecisionEnemigoRespawn : AIDecision, MMEventListener<MMGameEvent>
{
    private bool enemigo_respawned;

    public override void Initialization()
    {
       
    }

    public virtual void OnMMEvent(MMGameEvent e)
    {
        if(e.EventName == EnemigoEstados.RESPANW)
        {
            enemigo_respawned = true;
        }
    }

    void OnEnable()
    {
        this.MMEventStartListening<MMGameEvent>();
    }
    void OnDisable()
    {
        this.MMEventStopListening<MMGameEvent>();
    }


    public override bool Decide()
    {
        return enemigo_respawned;
    }
}
