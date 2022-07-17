using UnityEngine;

public class RedSlimeExploder : MonoBehaviour
{
    [SerializeField] private GameObject _littleSlime;
    [SerializeField] private int _slimesToSpawn = 3;

    public void SpawnSlimes()
    {
        var thisSlimeController = gameObject.GetComponent<SlimeController>();
        for (int i = 0; i < _slimesToSpawn; i++)
        {
            var slime = Instantiate(_littleSlime, transform.position, transform.rotation);
            var slimeController = slime.GetComponent<SlimeController>();

            slimeController.SetCounterControllers(thisSlimeController.GetScoreController(), thisSlimeController.GetLivesController());
            slimeController.SetDirection(thisSlimeController.GetDirection());
        }
    }
}
