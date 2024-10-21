using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public WalkState walkState;
    public UnitController unitController;

    private void Start()
    {
        walkState = new WalkState(this);
        unitController = new UnitController(this);
        unitController.Initialize(walkState);
    }
}