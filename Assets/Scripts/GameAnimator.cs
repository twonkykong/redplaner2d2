using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameAnimator : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Image screenFader;
    [SerializeField] private Transform cameraTransform;

    public void ShakeCamera(float value)
    {
        cameraTransform.DOKill();
        cameraTransform.DOShakePosition(0.3f, value, 20);
    }

    public void ScaleCamera(float size)
    {
        DOTween.To(() => mainCamera.orthographicSize, x => mainCamera.orthographicSize = x, size, 0.3f).SetEase(Ease.OutSine);
    }

    public void FadeScreenIn()
    {
        FadeScreen(0.4f, 0.2f);
    }

    public void FadeScreenOut()
    {
        FadeScreen(0f, 0.35f);
    }

    private void FadeScreen(float value, float duration)
    {
        screenFader.DOFade(value, duration);
    }
}