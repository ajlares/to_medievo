using UnityEngine;

public class MoveUnit : MonoBehaviour
{
    [SerializeField] private GameObject target;
    public void choiceTarget(int cubecount)
    {
        target = GameManager.instance.tilemap[cubecount];
        Debug.Log("c mueve");
    }
    private void Update()
    {

    }
}
