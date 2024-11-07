using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField] private string targetEnemy = "Red";  
    [SerializeField] private string targetUnit = "blue";    
    [SerializeField] private bool isUnit;
    [SerializeField] private bool isEnemy; 
    public BoxController boxController;  
    private bool isOccupied = false;
    private GameObject detectedEnemy;
    private GameObject detectedUnit;

    public bool IsOccupied => isOccupied;

    public bool IsUnit
    {
        get { return isUnit; }
        set { isUnit = value; }
    }

    public bool IsEnemy
    {
        get { return isEnemy; }
        set { isEnemy = value; }
    }

    public GameObject DetectedEnemy => detectedEnemy;
    public GameObject DetectedUnit => detectedUnit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetEnemy))
        {
            isOccupied = true;
            boxController.IsEmpty = false;  
            isEnemy = true; 
            isUnit = false; 
            detectedEnemy = other.gameObject; 
            Debug.Log($"¡Objeto detectado: {other.name} con tag {other.tag}!");
        }
        else if (other.CompareTag(targetUnit))
        {
            isOccupied = true;
            boxController.IsEmpty = false;  
            isUnit = true; 
            isEnemy = false; 
            detectedUnit = other.gameObject;
            Debug.Log($"¡Objeto detectado: {other.name} con tag {other.tag}!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetEnemy))
        {
            isOccupied = false;
            boxController.IsEmpty = true;  
            isEnemy = false; 
            detectedEnemy = null;
            Debug.Log($"¡Objeto salió: {other.name} con tag {other.tag}!");
        }
        else if (other.CompareTag(targetUnit))
        {
            isOccupied = false;
            boxController.IsEmpty = true; 
            isUnit = false; 
            Debug.Log($"¡Objeto salió: {other.name} con tag {other.tag}!");
        }
    }
}