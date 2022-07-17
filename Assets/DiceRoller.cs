using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 3.0f;
    [SerializeField] private Vector3[] _faceRotations;
    [SerializeField] private WaveController _waveController;
    [SerializeField] private int _diceNumber;
    [SerializeField] private bool _reRoll = false;
    [SerializeField] private Animator _animator;
    public float _secondsToActivateAfter = 0;
    public bool _isActivated = false;

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

    public void StartActivation()
    {
        Invoke("Activate", _secondsToActivateAfter);
    }

    private void Activate()
    {
        ChooseRollingTime();
        _animator.SetTrigger("DoActivate");
        _isActivated = true;
    }

    public void StopRolling(int side)
    {
        _stopRolling = true;
        _targetRotation = _faceRotations[side - 1];

        _waveController.SetSideChosen(_diceNumber, side);
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
        if (!_isActivated) return;

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
}
