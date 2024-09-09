using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float divisionValue;
    [SerializeField] private float followSpeed;

    private Transform _thisObjectTransform;

    private void Awake()
    {
        _thisObjectTransform = transform;
    }

    private void FixedUpdate()
    {
        float distance = Vector2.Distance(Vector2.zero, target.position);
        float targetDistance = distance / divisionValue;

        Vector3 direction = (Vector2)target.position.normalized;
        Vector3 targetPosition = direction * targetDistance;
        targetPosition.z = -10;

        _thisObjectTransform.position = Vector3.Slerp(_thisObjectTransform.position, targetPosition, followSpeed);
    }
}
