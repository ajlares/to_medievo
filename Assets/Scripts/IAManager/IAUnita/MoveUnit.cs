using UnityEngine;

public class MoveUnit : MonoBehaviour
{
    [SerializeField] private GameObject target;
    public void choiceTarget(int cubecount)
    {
        Debug.Log("c mueve");
        target = GameManager.instance.tilemap[cubecount];
    }
    private void Update()
    {

    }
}
