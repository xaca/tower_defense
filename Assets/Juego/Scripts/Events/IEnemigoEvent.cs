using UnityEngine;
interface IEnemigoEvent
{
    public void OnEnemigoAction(EventosEnemigos e,GameObject go);

    public void OnEnable();

    public void OnDisable();
}