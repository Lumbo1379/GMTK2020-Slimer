using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _slimes;

    private Transform _particleContainer = null;
    private DiceCounterController _scoreController;
    private DiceCounterController _livesController;

    public void SpawnSlimes(int numberToSpawn, int _slimeType)
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            var slime = Instantiate(_slimes[_slimeType], transform);
            var slimeController = slime.GetComponent<SlimeController>();
            slimeController.SetParticlesContainer(_particleContainer);
            slimeController.SetCounterControllers(_scoreController, _livesController);
        }
    }

    private void Awake()
    {
        _particleContainer = GameObject.Find("Particles Container").transform;
        _scoreController = GameObject.Find("Score Counter Container").GetComponent<DiceCounterController>();
        _livesController = GameObject.Find("Lives Counter Container").GetComponent<DiceCounterController>();
    }
}
