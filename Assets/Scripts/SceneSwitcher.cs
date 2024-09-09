using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private Animation sceneTransitionAnimtion;

    public void SwitchToScene(string sceneName)
    {
        StartCoroutine(SceneSwitchCoroutine(sceneName));
    }

    private IEnumerator SceneSwitchCoroutine(string sceneName)
    {
        sceneTransitionAnimtion.Play("TransitionStart");
        yield return new WaitForSeconds(sceneTransitionAnimtion.GetClip("TransitionStart").length);
        SceneManager.LoadScene(sceneName);
    }
}
