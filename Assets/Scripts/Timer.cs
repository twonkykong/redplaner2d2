using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameController gameController;

    [SerializeField] private int timeLimit;

    public bool IsEnabled { private get; set; } = true;
    public string FormattedTime
    {
        get
        {
            var time = TimeSpan.FromSeconds(timeLimit);
            string formattedTime = time.ToString(@"mm\:ss");

            return formattedTime;
        }
    }

    private void Start()
    {
        timerText.text = FormattedTime;
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => IsEnabled == true);
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => IsEnabled == true);

            timeLimit -= 1;
            timerText.text = FormattedTime;

            if (timeLimit == 0)
            {
                gameController.Fail();
            }

            yield return new WaitForEndOfFrame();
        }
    }

    public void Disable()
    {
        StopAllCoroutines();
    }
}