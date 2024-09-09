using System.Collections;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D planeRb;
    [SerializeField] private Collider2D planeCollider;
    [SerializeField] private PlaneMover planeMover;
    [SerializeField] private PlaneRotator planeRotator;

    [SerializeField] private Timer timer;
    [SerializeField] private GameAnimator gameAnimator;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip winClip;
    [SerializeField] private AudioClip failClip;

    [SerializeField] private SceneSwitcher sceneSwitcher;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject failPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gamePanel;

    [SerializeField] private int currentLevelIndex;

    public void Pause()
    {
        DOTween.PauseAll();

        timer.IsEnabled = false;

        gamePanel.SetActive(false);
        pausePanel.SetActive(true);
        gameAnimator.FadeScreenIn();

        planeMover.IsMoving = false;
        planeRotator.IsEnabled = false;
    }

    public void Unpause()
    {
        DOTween.PlayAll();

        timer.IsEnabled = true;

        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
        gameAnimator.FadeScreenOut();

        planeMover.IsMoving = true;
        planeRotator.IsEnabled = true;
    }

    public void Win()
    {
        PlayerPrefs.SetInt("Level" + (currentLevelIndex + 1), 1);

        audioSource.PlayOneShot(winClip);
        EndGame();
        StartCoroutine(EnableEndPanel(winPanel));
    }

    public void Fail()
    {
        gameAnimator.ShakeCamera(0.2f);
        audioSource.PlayOneShot(failClip);
        EndGame();
        StartCoroutine(EnableEndPanel(failPanel));
    }

    private void EndGame()
    {
        Pause();
        planeMover.enabled = false;
        planeCollider.enabled = false;
        planeRb.velocity = Vector2.zero;

        pausePanel.SetActive(false);

    }

    private IEnumerator EnableEndPanel(GameObject panel)
    {
        yield return new WaitForSeconds(1f);
        panel.SetActive(true);
    }

    public void Menu()
    {
        sceneSwitcher.SwitchToScene("Menu");
    }

    public void NextLevel()
    {
        sceneSwitcher.SwitchToScene("Level" + (currentLevelIndex + 1));
    }

    public void Restart()
    {
        sceneSwitcher.SwitchToScene("Level" + currentLevelIndex);
    }
}