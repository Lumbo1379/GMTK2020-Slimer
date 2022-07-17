using UnityEngine;

public class WaveDiceRoller : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 3.0f;
    [SerializeField] private Vector3[] _faceRotations;
    [SerializeField] private WaveController _waveController;
    [SerializeField] private bool _reRoll = false;

    private float _timeToRoll = 0;
    private float _timeRolled = 0;
    private bool _stopRolling = true;

    private int _flipRight = 1;
    private int _flipUp = 1;
    private int _count;
    private Vector3 _targetRotation = new Vector3(-120, -180, 75);

    public bool IsReadyToChoose()
    {
        return !_stopRolling && _timeRolled >= _timeToRoll;
    }

    public void StopRolling(int side)
    {
        _stopRolling = true;
        _count = side;
        _targetRotation = _faceRotations[side];

        Invoke("Count", 1);
    }

    public void ReRoll()
    {
        _reRoll = false;
        _stopRolling = false;
        _timeRolled = 0;

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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(_targetRotation), _rotationSpeed * Time.deltaTime);
        }

        if (_reRoll)
        {
            ReRoll();
        }
    }

    private void ChooseRollingTime()
    {
        _timeToRoll = Random.Range(1.0f, 3.0f);

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

    private void Count()
    {
        _count -= 1;

        if (_count >= 0)
        {
            _targetRotation = _faceRotations[_count];
        }

        if (_count > 0)
        {
            Invoke("Count", 1);
        }
        else
        {
            _waveController.RerollAllDice();
        }
    }
}
