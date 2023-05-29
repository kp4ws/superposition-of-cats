using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public float foregroundSpeed = 1f;
    public float parallaxFactor = 0.5f;

    private Transform cameraTransform;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    private void Start() 
    {
        cameraTransform = Camera.main.transform;
        initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 cameraMovement = cameraTransform.position - initialPosition;
        Vector3 parallaxMovement = new Vector3(cameraMovement.x * parallaxFactor, 0f, 0f);
        targetPosition = initialPosition + parallaxMovement;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, foregroundSpeed * Time.deltaTime);
    }

    public void Respawn()
    {
        transform.position = initialPosition;
    }
}