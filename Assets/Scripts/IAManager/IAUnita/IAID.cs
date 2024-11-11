using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class IAID : MonoBehaviour
{
    [SerializeField] private int iD;
    //id valeus
    //1 red
    //2 blue
    //3 obstacle
    //4 metheorite
    public int ID
    {
        get
        {
            return iD;
        }
        set
        {
            ID = value;
        }
    }
}
