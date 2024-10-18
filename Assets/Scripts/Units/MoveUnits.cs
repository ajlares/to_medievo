using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script para el movmiento de cada unidad 
public class MoveUnits : MonoBehaviour
{
    public GameObject unit;
    public GameObject finalPosition;
    public float speed;

    
    private void Update()
    {
        unit.transform.position = Vector3.MoveTowards(unit.transform.position, finalPosition.transform.position, speed * Time.deltaTime);
    }
}
