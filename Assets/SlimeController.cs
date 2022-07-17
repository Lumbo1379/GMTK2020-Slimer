using UnityEngine;
using UnityEngine.Events;

public class SlimeController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _jumpForce = 200.0f;
    [SerializeField] private float _jumpDelay = 0.5f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private GameObject _slimeParticle;
    [SerializeField] private float _minSlimeParticleOffset = -0.2f;
    [SerializeField] private float _maxSlimeParticleOffset = 0.2f;
    [SerializeField] private int _numberOfDeathParticles = 4;
    [SerializeField] private float _splatForce = 50.0f;
    [SerializeField] private UnityEvent _onDieEvent = new UnityEvent();
    [SerializeField] private GameObject _dieSound;

    private float _groundedRadius = 0.1f;
    private bool _blockJump = false;
    private Vector2 _direction = Vector2.left;
    private Collider2D _lastLevelCollider = null;
    private bool _isDead = false;
    private Transform _particlesContainer;
    private DiceCounterController _scoreController;
    private DiceCounterController _livesController;

    public void SetCounterControllers(DiceCounterController score, DiceCounterController lives)
    {
        _scoreController = score;
        _livesController = lives;
    }

    public DiceCounterController GetScoreController()
    {
        return _scoreController;
    }

    public DiceCounterController GetLivesController()
    {
        return _livesController;
    }

    public Vector2 GetDirection()
    {
        return _direction;
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public bool IsDead()
    {
        return _isDead;
    }

    public void DoSlimeJump()
    {
        _blockJump = false;

        var direction = (Vector2.up + _direction).normalized;

        _rigidBody.AddForce(direction * _jumpForce);
    }

    public void SetDirection(Vector2 direction, Collider2D levelCollider)
    {
        if (_lastLevelCollider == levelCollider) return;

        _spriteRenderer.flipX = direction != Vector2.left;

        _direction = direction;
    }

    public void SetParticlesContainer(Transform particlesContainer)
    {
        _particlesContainer = particlesContainer;
    }

    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, _groundedRadius, _groundMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                if (!_blockJump)
                {
                    _blockJump = true;

                    Invoke("StartDoMove", _jumpDelay);
                }

                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Bullet")
        {
            if (_isDead) return;

            Destroy(collider.gameObject);

            var bulletRigidBody = collider.gameObject.GetComponent<Rigidbody2D>();

            KillSlime(bulletRigidBody.velocity.normalized);
        }
        else if (collider.tag == "LoseLife")
        {
            _livesController.ChangeCount(-1);
        }
    }

    private void StartDoMove()
    {
        _animator.SetTrigger("DoMove");
    }

    public void KillSlime(Vector2 splatDirection)
    {
        if (_isDead) return;

        _isDead = true;
        Instantiate(_dieSound, transform.position, transform.rotation);

        for (int i = 0; i < _numberOfDeathParticles; i++)
        {
            var particle = Instantiate(_slimeParticle, _particlesContainer);

            particle.transform.position = transform.position;

            var particleRigidBody = particle.GetComponent<Rigidbody2D>();
            particleRigidBody.velocity = splatDirection + (Vector2)Utils.GetRandomOffset(_minSlimeParticleOffset, _maxSlimeParticleOffset) * _splatForce;
        }

        _onDieEvent.Invoke();

        _scoreController.ChangeCount(1);
        Destroy(gameObject);
    }
}
