using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    [SerializeField] private Transform _resetPosition;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "ResetPlayer")
        {
            transform.position = _resetPosition.position;
        }
    }

    private void Update()
    {
        if (transform.position.y >= 20 || transform.position.y <= -20)
        {
            transform.position = _resetPosition.position;
        }
    }
}
