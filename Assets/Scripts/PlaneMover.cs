using UnityEngine;
using DG.Tweening;

public class PlaneMover : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    [SerializeField] private float speedIncreaseValue;
    [SerializeField] private float speedDecreaseValue;
    [SerializeField] private float planeStartSpeedMin, planeStartSpeedMax;

    [SerializeField] private Rigidbody2D planeRb;
    [SerializeField] private Transform planeTransform;

    [SerializeField] private AudioSource audioSource;

    private Vector2 _lastVelocity;

    public bool IsEnabled { private get; set; }

    private float _currentSpeed;

    private bool _isMoving;
    public bool IsMoving
    {
        set
        {
            if (IsEnabled)
            {
                _isMoving = value;
                audioSource.mute = !value;

                if (!_isMoving)
                {
                    _lastVelocity = planeRb.velocity;
                    planeRb.velocity = Vector2.zero;
                }
                else planeRb.velocity = _lastVelocity;
            }
        }
    }

    public void AddFuel()
    {
        _currentSpeed += speedIncreaseValue;
    }

    public void Launch(float scalerValue)
    {
        float startSpeed = scalerValue * planeStartSpeedMax;
        startSpeed = Mathf.Max(startSpeed, planeStartSpeedMin);

        IsEnabled = true;
        _isMoving = true;

        audioSource.mute = false;

        _currentSpeed = 0.15f;

        DOTween.To(() => _currentSpeed, x => _currentSpeed = x, startSpeed, 0.5f);
    }

    public void JumpBack()
    {
        IsEnabled = false;

        planeTransform.DOKill();
        planeTransform.DOMove(planeTransform.position + planeTransform.right * 0.1f, 0.5f).SetEase(Ease.OutCirc).OnComplete(() =>
        {
            IsEnabled = true;
        });
    }

    private void FixedUpdate()
    {
        if (!IsEnabled) return;
        if (!_isMoving) return;

        planeRb.velocity = -(Vector2)planeTransform.right.normalized * _currentSpeed;
        _currentSpeed -= speedDecreaseValue;

        if (planeRb.velocity.magnitude <= 0.1f)
        {
            gameController.Fail();
        }
    }
}
