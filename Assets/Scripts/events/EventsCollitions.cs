using System.Collections;
using UnityEngine;

public class EventsCollitions : MonoBehaviour
{
    private void Start() 
    {
        StartCoroutine(movement());
    }

    IEnumerator movement()
    {
        if(transform.position.y > 1)
        {
           Vector3 _position = transform.position;
           _position.y = transform.position.y -0.25f;
           transform.position = _position;
           yield return new WaitForSeconds(.025f);
           StartCoroutine(movement());
        }
        yield return null;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("MapCube"))
        {
            other.gameObject.GetComponent<BoxController>().saveObject(gameObject);
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
