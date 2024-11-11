using System.Collections.Generic;
using UnityEngine;

public class UnitTurnManager : MonoBehaviour
{
    public static UnitTurnManager instance;
    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy( this);
        }
    }
    public void EnableMovementAndAttackForAllies()
    {
        if (GameManager.instance.allyUnits == null || GameManager.instance.allyUnits.Count == 0)
        {
            Debug.LogWarning("No hay unidades aliadas para habilitar el movimiento y ataque.");
            return;
        }

        foreach (GameObject allyUnit in GameManager.instance.allyUnits)
        {
            // Intenta obtener el componente UnitStateManager
            UnitStateManager unitStateManager = allyUnit.GetComponent<UnitStateManager>();

            if (unitStateManager != null)
            {
                unitStateManager.canMove = true;
                unitStateManager.canAttack = true;
                Debug.Log($"{allyUnit.name} ahora puede moverse y atacar.");
            }
        }
    }
}
