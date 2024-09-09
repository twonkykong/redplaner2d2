using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private SceneSwitcher sceneSwitcher;

    [SerializeField] private TextMeshProUGUI levelIndexText;
    [SerializeField] private Button buttonComponent;
    [SerializeField] private int levelIndex;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("Level" + levelIndex, 0) == 1 || levelIndex == 1) buttonComponent.interactable = true;
    }

    private void Start()
    {
        levelIndexText.text = levelIndex.ToString();
    }

    public void Play()
    {
        sceneSwitcher.SwitchToScene("Level" + levelIndex);
    }
}
