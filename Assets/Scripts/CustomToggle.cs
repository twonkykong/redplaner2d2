using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class CustomToggle : MonoBehaviour
{
    [SerializeField] private Image toggleImage; 
    [SerializeField] private Transform handleTransform; 
    [SerializeField] private Color enabledColor, disabledColor; 

    [SerializeField] private UnityEvent<bool> onToggle; 

    public bool IsEnabled { get; private set; } = true; 

    private bool _isSwitching; 

    public void Switch()
    {
        if (_isSwitching) return;

        _isSwitching = true; 

        IsEnabled = !IsEnabled;

        handleTransform.DOLocalMoveX(-handleTransform.localPosition.x, 0.3f).OnComplete(() => _isSwitching = false);

        Color toggleColor = IsEnabled ? enabledColor : disabledColor;

        toggleImage.DOColor(toggleColor, 0.2f);

        onToggle?.Invoke(IsEnabled);
    }
}
