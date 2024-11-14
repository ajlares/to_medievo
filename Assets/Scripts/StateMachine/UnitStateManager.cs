using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateManager : MonoBehaviour
{
    UnitBaseState currentState;
    UnitAttackState AttackState = new UnitAttackState();
    UnitIdleState IdleState = new UnitIdleState();
    UnitFinishState FinishState = new UnitFinishState();
    UnitMovementState MovementState = new UnitMovementState();
    UnitSelectedState SelectedState = new UnitSelectedState();

    private Material originalMaterial;
    private Material outlineMaterial;

    public bool canMove = true, canAttack = true;
    private CameraController cameraController;

    public GameObject tower;

    private Animator anim;  
    public Animator Anim => anim;

    void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
        currentState = IdleState;
        anim = GetComponent<Animator>();

        Renderer renderer = this.GetComponent<Renderer>();

        if (renderer != null)
        {
            originalMaterial = renderer.material;
            outlineMaterial = Resources.Load<Material>("OutlineMaterial");
        }
    }
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void ChangeState(UnitBaseState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);

        // Si cambiamos a un estado distinto de Idle, mover la cámara a la unidad
        if (!(newState is UnitIdleState))
        {
            Transform cameraPosition = transform.Find("CameraPosition");
            if (cameraPosition != null)
            {
                cameraController.MoveToTarget(cameraPosition);
            }
        }
        else
        {
            // Volver a la posición original en estado Idle
            cameraController.ResetCameraPosition();
        }
    }

    private void OnMouseDown()
    {
        if(!canAttack && !canMove)
        {
            UnitBaseState newState = new UnitFinishState();
            ChangeState(newState);
        }
        else
        {
            if(currentState != SelectedState)
            {
                UnitBaseState newState = new UnitSelectedState();
                ChangeState(newState);
            }
            else
            {
                Debug.Log("Ya esta selecionada"); 
            }
        }
        
    }
    private void OnMouseEnter()
    {
        ApplyOutline(this);
    }
    private void OnMouseExit()
    { 
        RemoveOutline(this);
        if(currentState == MovementState)
        {
            UnitBaseState newState = new UnitIdleState();
            ChangeState(newState);
        }
    }
    private void ApplyOutline(UnitStateManager unit)
    {
        Renderer renderer = unit.GetComponent<Renderer>();
        if (renderer != null && renderer.material != outlineMaterial)
        {
            renderer.material = outlineMaterial;
        }
    }

    private void RemoveOutline(UnitStateManager unit)
    {
        Renderer renderer = unit.GetComponent<Renderer>();
        if (renderer != null && renderer.material != originalMaterial)
        {
            renderer.material = originalMaterial;
        }
    }
}
