using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System.Collections;

public class PlaneSpeedScaler : MonoBehaviour
{
    [SerializeField] private UnityEvent onLaunch;

    [SerializeField] private RectTransform handleTransform;

    [SerializeField] private PlaneRotator planeRotator;
    [SerializeField] private PlaneMover planeMover;

    [SerializeField] private GameObject scalerPanel;

    [SerializeField] private float handleResistanceValue;
    [SerializeField] private float handleSensitivity;
    [SerializeField] private float lowerHandleCoord;

    private float _scalerValue;

    private bool _canScale;

    public void StartScaling()
    {
        scalerPanel.SetActive(true);

        _canScale = true;

        StartCoroutine(LaunchCoroutine());
    }

    private void Update()
    {
        if (!_canScale) return;

        if (handleTransform.anchoredPosition.y < 0)
        {
            handleTransform.anchoredPosition += Vector2.up * handleResistanceValue;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log(handleTransform.anchoredPosition);
                Debug.Log(touch.deltaPosition);
                Vector2 nextPos = handleTransform.anchoredPosition + Vector2.up * (touch.deltaPosition.y * handleSensitivity);
                Debug.Log(nextPos);
                if (nextPos.y >= lowerHandleCoord && nextPos.y <= 0)
                {
                    handleTransform.anchoredPosition = nextPos;
                }
            }
        }
    }

    private IEnumerator LaunchCoroutine()
    {
        yield return new WaitForSeconds(3f);

        _scalerValue = handleTransform.anchoredPosition.y / lowerHandleCoord;
        planeMover.Launch(_scalerValue);
        planeRotator.enabled = true;

        scalerPanel.SetActive(false);
        _canScale = false;

        onLaunch?.Invoke();
    }
}
