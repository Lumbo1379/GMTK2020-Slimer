using UnityEngine;

public class SetEnemyDirection : MonoBehaviour
{
    private enum _directions
    {
        Left,
        Right,
        Random
    }

    [SerializeField] private _directions _direction;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
            var slimeController = collider.gameObject.GetComponent<SlimeController>();
            slimeController.SetDirection(GetDirection(), _collider);
        }
    }

    private Vector2 GetDirection()  // TODO: Add random direction
    {
        if (_direction == _directions.Left)
        {
            return Vector2.left;
        }

        return Vector2.right;
    }
}
