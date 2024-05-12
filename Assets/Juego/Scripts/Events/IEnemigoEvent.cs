using UnityEngine;
interface IEnemigoEvent
{
    public void OnEnemigoAction(GameObject go);

    public void OnEnable();

    public void OnDisable();
}