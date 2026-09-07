using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;             
    [SerializeField] private Vector2 offset = new Vector2(2f, 0f); 
    [SerializeField] private float smoothTime = 0.15f;

    private Vector3 velocity = Vector3.zero;
    private float fixedZ;

    private void Start()
    {
        fixedZ = transform.position.z; 
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, fixedZ);
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
    }
}