using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController2D _controller;
    [SerializeField] private float _runSpeed = 40.0f;

    private float _horizontalMove = 0.0f;
    private bool _hasJumped = false;
    private bool _blockKnockback = false;
    public bool _blockInput = true;

    public bool IsKnockbackBlocked()
    {
        return _blockKnockback;
    }

    private void Update()
    {
        if (_blockInput) return;

        _horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;

        _animator.SetBool("IsRunning", _horizontalMove != 0);

        if (Input.GetButtonDown("Jump"))
        {
            _hasJumped = true;
        }
    }

    private void FixedUpdate()
    {
        _controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _hasJumped);

        _hasJumped = false;
    }

    public void BlockKnockback()
    {
        _blockKnockback = true;
        Invoke("UnblockKnockback", 0.4f);
    }

    private void UnblockKnockback()
    {
        _blockKnockback = false;
    }
}
