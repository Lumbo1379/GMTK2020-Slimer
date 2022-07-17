using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private CapsuleCollider2D _killHitBox;

    private bool _beingDespawned = false;

    private void Start()
    {
        Invoke("DespawnBullet", 20);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if (collision.gameObject.tag == "Ground")
        // {
        //     _killHitBox.tag = "Untagged";
        // }
    }

    private void FixedUpdate()
    {
        if (_rigidBody.velocity.magnitude <= 2)
        {
            if (_beingDespawned) return;

            _beingDespawned = true;
            _killHitBox.tag = "Untagged";

            Invoke("DespawnBullet", 3);
        }
        else
        {
            if (!_beingDespawned) return;

            _beingDespawned = false;
            _killHitBox.tag = "Bullet";
            CancelInvoke("DespawnBullet");
        }
    }

    private void DespawnBullet()
    {
        Destroy(gameObject);
    }
}
