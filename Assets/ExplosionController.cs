using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] private AudioSource _soundEffect;

    public void DestroyExplosion()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        _soundEffect.Play();
    }
}
