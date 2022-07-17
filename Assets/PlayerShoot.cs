using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private AmmoDiceRoller _ammoDiceRoller;
    [SerializeField] private float _shotForce = 50.0f;
    [SerializeField] private float _minBulletOffset = -0.2f;
    [SerializeField] private float _maxBulletOffset = 0.2f;
    [SerializeField] private int _bulletsPerShot = 7;
    [SerializeField] private AudioSource _outOfAmmo;
    [SerializeField] private AudioSource _shoot;
    public bool _blockInput = true;

    private bool _hasShot = false;
    private int _ammo = 0;

    public void SetAmmo(int ammo)
    {
        _ammo = ammo;
    }

    private void Update()
    {
        if (_blockInput) return;

        var mousePosition = GetMouseWorldPosition();
        var aimDirection = (mousePosition - transform.position).normalized * MatchPlayerDirection();

        var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        angle = Mathf.Clamp(angle, -90, 90);

        transform.eulerAngles = new Vector3(0, 0, angle);

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (_ammo > 0)
            {
                _hasShot = true;
                _shoot.Play();
            }
            else
            {
                _outOfAmmo.Play();
            }
        }
    }

    private void FixedUpdate()
    {
        if (_hasShot)
        {
            _hasShot = false;

            _ammo -= 1;
            _ammoDiceRoller.MakeShot(_ammo);

            for (int i = 0; i < _bulletsPerShot; i++)
            {
                var bullet = Instantiate(_bullet, _shootPosition.position, _shootPosition.rotation);
                var bulletRigidBody = bullet.GetComponent<Rigidbody2D>();

                bulletRigidBody.AddForce((transform.right + Utils.GetRandomOffset(_minBulletOffset, _maxBulletOffset)) * MatchPlayerDirection() * _shotForce);
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;

        return worldPosition;
    }

    private int MatchPlayerDirection()
    {
        if (transform.parent.transform.localScale.x < 0)
        {
            return -1;
        }

        return 1;
    }
}
