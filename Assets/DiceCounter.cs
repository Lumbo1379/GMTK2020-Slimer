using UnityEngine;

public class DiceCounter : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 3.0f;
    [SerializeField] private Vector3[] _faceRotations;
    private Vector3 _targetRotation = new Vector3(-120, -180, 75);


    public void SetTargetRotation(int side)
    {
        _targetRotation = _faceRotations[side];
    }

    private void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(_targetRotation), _rotationSpeed * Time.deltaTime);
    }
}
