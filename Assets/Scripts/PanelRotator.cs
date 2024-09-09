using UnityEngine;

public class PanelRotator : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Collider2D[] hitColliders = Physics2D.OverlapPointAll(mainCamera.ScreenToWorldPoint(touch.position));

                foreach (Collider2D hitCol in hitColliders)
                {
                    if (hitCol.TryGetComponent(out Panel panel))
                    {
                        panel.Rotate();
                        audioSource.PlayOneShot(audioClip);
                    }
                }
            }
        }
    }
}
