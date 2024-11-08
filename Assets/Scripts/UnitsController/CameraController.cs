using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 originalPosition; // Posición original de la cámara
    private Quaternion originalRotation; // Rotación original
    private Transform target; // Posición objetivo de la unidad seleccionada
    [SerializeField] private float smoothSpeed = 5f;

    private void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            // Movimiento suave hacia la posición del objetivo
            Vector3 desiredPosition = target.position;
            Quaternion desiredRotatation = target.rotation;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotatation, Time.deltaTime * smoothSpeed);
        }
    }

    public void MoveToTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void ResetCameraPosition()
    {
        target = null;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}

