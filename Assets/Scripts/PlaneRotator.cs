using UnityEngine;

public class PlaneRotator : MonoBehaviour
{
    [SerializeField] private Transform planeTransform;
    [SerializeField] private float sensitivity;

    public bool IsEnabled { private get; set; } = true;

    private void Update()
    {
        if (!IsEnabled) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                planeTransform.Rotate(Vector3.forward * -touch.deltaPosition.x * sensitivity);
            }
        }
    }
}
