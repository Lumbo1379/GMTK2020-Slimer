using UnityEngine;

public class AmmoDiceRoller : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 3.0f;
    [SerializeField] private Vector3[] _faceRotations;
    [SerializeField] private PlayerShoot _playerShoot;
    [SerializeField] private bool _reRoll = false;

    private float _timeToRoll = 0;
    private float _timeRolled = 0;
    private bool _stopRolling = false;

    private int _flipRight = 1;
    private int _flipUp = 1;
    private Vector3 _targetRotation;

    public bool IsReadyToChoose()
    {
        return !_stopRolling && _timeRolled >= _timeToRoll;
    }

    public void MakeShot(int ammo)
    {
        if (ammo > 0)
        {
            _targetRotation = _faceRotations[ammo - 1];
        }
        else
        {
            if (_stopRolling)
            {
                ReRoll();
            }
        }
    }

    public void StopRolling(int side)
    {
        _stopRolling = true;
        _targetRotation = _faceRotations[side - 1];
        _playerShoot.SetAmmo(side);
    }

    public void ReRoll()
    {
        _reRoll = false;
        _stopRolling = false;
        _timeRolled = 0;

        ChooseRollingTime();
    }


    private void Start()
    {
        ChooseRollingTime();
    }

    private void Update()
    {
        if (!_stopRolling)
        {
            _timeRolled += Time.deltaTime;

            transform.Rotate(_flipRight * Vector3.right * _rotationSpeed * Time.deltaTime);
            transform.Rotate(_flipUp * Vector3.up * _rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(_targetRotation), _rotationSpeed / 2 * Time.deltaTime);
        }

        if (_reRoll)
        {
            ReRoll();
        }
    }

    private void ChooseRollingTime()
    {
        _timeToRoll = 0.5f;

        if (Random.Range(0, 3) <= 1)
        {
            _flipRight = -1;
        }
        else
        {
            _flipRight = 1;
        }

        if (Random.Range(0, 3) <= 1)
        {
            _flipUp = -1;
        }
        else
        {
            _flipUp = 1;
        }
    }
}
