using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class AIDecisionActivarEnemigo : AIDecision
{
    private GameObject enemigo;
    public override void Initialization(){
        enemigo = this.gameObject;
    }
    public override bool Decide(){
        
        return enemigo.activeSelf;
    }
}
