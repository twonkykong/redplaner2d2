using UnityEngine;
using DG.Tweening;

public class Panel : MonoBehaviour
{
    [SerializeField] private float rotationDuration;

    private Transform _thisObjectTransform;
    private bool _isRotating;

    private void Awake()
    {
        _thisObjectTransform = transform;
        _thisObjectTransform.DORotate(Vector3.forward * (Random.Range(-2, 2) * 90), 0.2f);
    }

    public void Rotate()
    {
        if (_isRotating) return;

        _thisObjectTransform.DORotate(_thisObjectTransform.eulerAngles + (Vector3.forward * 90), rotationDuration).SetEase(Ease.OutQuad).OnComplete(() => _isRotating = false);
        _isRotating = true;
    }
}
