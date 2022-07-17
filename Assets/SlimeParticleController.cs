using UnityEngine;

public class SlimeParticleController : MonoBehaviour
{
    [SerializeField] private float _despawnTime = 15.0f;

    private void Start()
    {
        Invoke("DespawnParticle", _despawnTime);
    }

    private void DespawnParticle()
    {
        Destroy(gameObject);
    }
}
