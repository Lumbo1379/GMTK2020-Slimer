using UnityEngine;

public class PurpleSlimeExploder : MonoBehaviour
{
    [SerializeField] private float _deathRadius = 2.0f;
    [SerializeField] private float _knockBackRadius = 5.0f;
    [SerializeField] private float _knockForce = 100.0f;
    [SerializeField] private float _maxKnockForce = 200.0f;
    [SerializeField] private LayerMask _effectMask;
    [SerializeField] private GameObject _explosion;

    private Animator _cameraShake;

    private void Awake()
    {
        _cameraShake = Camera.main.gameObject.GetComponent<Animator>();
    }

    public void Explode()
    {
        _cameraShake.SetTrigger("DoShake");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _deathRadius, _effectMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            var collider = colliders[i];
            if (collider.gameObject != gameObject)
            {
                if (collider.tag == "Enemy")
                {
                    var slimeController = collider.gameObject.GetComponent<SlimeController>();
                    var direction = (collider.transform.position - transform.position).normalized;

                    slimeController.KillSlime(direction);
                }
            }
        }

        colliders = Physics2D.OverlapCircleAll(transform.position, _knockBackRadius, _effectMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            var collider = colliders[i];
            if (collider.gameObject != gameObject)
            {
                if (collider.tag == "Enemy")
                {
                    var slimeController = collider.gameObject.GetComponent<SlimeController>();

                    if (slimeController.IsDead()) continue;

                    ApplyKnockback(collider);
                }
                else if (collider.tag == "Player")
                {
                    var playerMovement = collider.gameObject.GetComponent<PlayerMovement>();

                    if (playerMovement.IsKnockbackBlocked()) continue;
                    playerMovement.BlockKnockback();

                    ApplyKnockback(collider);
                }
            }
        }

        Instantiate(_explosion, transform.position, transform.rotation);
    }

    private void ApplyKnockback(Collider2D collider)
    {
        var direction = (collider.transform.position - transform.position).normalized;
        var distance = (collider.transform.position - transform.position).magnitude;

        float force = Mathf.Clamp(_knockForce / distance, 0, _maxKnockForce);

        var rigidBody = collider.gameObject.GetComponent<Rigidbody2D>();
        rigidBody.AddForce(direction * force);
    }
}
