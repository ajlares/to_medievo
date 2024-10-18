using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private int internalValeu;
    [SerializeField] private bool isEmpty;

    public int InternalValeu
    {
        get
        {
            return internalValeu;
        }
    }

    public bool IsEmpty
    {
        get
        {
            return isEmpty;
        }
        set
        {
            isEmpty = value;
        }
    }
}
