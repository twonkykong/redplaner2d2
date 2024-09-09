using UnityEngine;
using DG.Tweening;

public class PlaneCollisionChecker : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    [SerializeField] private PlaneMover planeMover;
    [SerializeField] private PlaneHealth planeHealth;

    private Transform _thisObjectTransform;

    private void Awake()
    {
        _thisObjectTransform = transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Panel"))
        {
            planeHealth.Damage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fuel"))
        {
            planeMover.AddFuel();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Health"))
        {
            planeHealth.AddHealth();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Win"))
        {
            gameController.Win();

            _thisObjectTransform.right = (Vector2)(_thisObjectTransform.position - collision.transform.position);
            _thisObjectTransform.DOMove(new Vector3(collision.transform.position.x, collision.transform.position.y, _thisObjectTransform.position.z), 0.3f);
        }
    }
}
