using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject[] _ui;
    [SerializeField] private CharacterController2D _characterController;
    [SerializeField] private WaveController _waveController;
    [SerializeField] private PlayerShoot _playerShoot;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private DiceRoller[] _waveDice;
    [SerializeField] private AudioSource _mainMusic;
    [SerializeField] private GameObject _gameOverUI;

    private bool _fadeMusicOut = false;

    public void HideUI()
    {
        foreach (var obj in _ui)
        {
            obj.SetActive(false);
            _characterController._blockInput = false;
            _waveController._pause = false;
            _playerShoot._blockInput = false;
            _playerMovement._blockInput = false;

            foreach (var dice in _waveDice)
            {
                dice.StartActivation();
            }
        }
    }

    private void Update()
    {
        if (_fadeMusicOut)
        {
            _mainMusic.volume -= Time.deltaTime;
        }
    }

    public void AllLivesLost()
    {
        _fadeMusicOut = true;

        _characterController._blockInput = true;
        _waveController._pause = true;
        _playerShoot._blockInput = true;
        _playerMovement._blockInput = true;

        _gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
