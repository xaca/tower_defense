using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class AIActionPatrullarOla : AIActionMovePatrol2D, IEnemigoEvent
{
    protected override void Patrol(){
        if(this.transform.gameObject.name=="Koala(Clone)")
        {
            if(_character!=null)
            {
                //Debug.Log(_character.ConditionState.CurrentState);
            }
        }
        if(_mmPath!=null){
            base.Patrol(); 
        }
    }

    protected override void InitializePatrol()
    {
        
    }

    public void ReiniciarPatrulla(){
		_controller = this.gameObject.GetComponentInParent<TopDownController>();
        _character = this.gameObject.GetComponentInParent<Character>();
        _orientation2D = _character?.FindAbility<CharacterOrientation2D>();
        _characterMovement = _character?.FindAbility<CharacterMovement>();
        _health = _character?.CharacterHealth;
        // initialize the start position
        _startPosition = transform.position;
        // initialize the direction
        _direction = _orientation2D.IsFacingRight ? Vector2.right : Vector2.left;
        _initialScale = transform.localScale;
        _currentIndex = 0;
        _indexLastFrame = -1;
        _waitingDelay = 0;
        _initialized = true;
        _lastPatrolPointReachedAt = Time.time;
    }

    public void ActivarEnemigo(GameObject enemigo)
    {    
        Ruta temp;   
        temp = enemigo.GetComponent<Ruta>();
        _mmPath = temp;
        //_mmPath.Initialization();
        //enemigo.transform.position = temp.InicioRuta();
        ReiniciarPatrulla();
    }

    public void OnEnemigoAction(EventosEnemigos e,GameObject go)
    {
        if(e == EventosEnemigos.RUTA_ACTUALIZADA)
        {
            if(go.Equals(this.gameObject))
            {
                //Debug.Log("Actualizar ruta "+go);
                ActivarEnemigo(go);
            }
        }
    }
    public  void OnEnable()
    {
        base.OnEnable();
        ManejadorEventos.AddEventListener(OnEnemigoAction);
    }
    public void OnDisable()
    {
        base.OnDisable();
        ManejadorEventos.RemoveEventListener(OnEnemigoAction);
    }
}
