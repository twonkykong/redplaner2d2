using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlaneHealth : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private GameAnimator gameAnimator;
    [SerializeField] private Animator planeAnimator;

    [SerializeField] private PlaneMover planeMover;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    [SerializeField] private Color lostHealthColor;
    [SerializeField] private GameObject healthPrefab;
    [SerializeField] private Transform healthParent;

    [SerializeField] private int healthAmount;

    private Image[] _healthImages;

    private void Start()
    {
        _healthImages = new Image[healthAmount];

        for (int i = 0; i < healthAmount; i += 1)
        {
            Image newHealthImage = Instantiate(healthPrefab, healthParent).GetComponent<Image>();
            _healthImages[i] = newHealthImage;
        }
    }

    public void Damage()
    {
        if (healthAmount == 0) return;

        planeAnimator.SetTrigger("Damage");
        gameAnimator.ShakeCamera(0.05f);

        healthAmount -= 1;
        _healthImages[healthAmount].DOColor(lostHealthColor, 0.2f);
        planeMover.JumpBack();

        if (healthAmount == 0)
        {
            gameController.Fail();
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void AddHealth()
    {
        if (healthAmount == _healthImages.Length) return;

        _healthImages[healthAmount].DOColor(Color.white, 0.2f);
        healthAmount += 1;
    }
}
