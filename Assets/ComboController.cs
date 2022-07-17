using UnityEngine;

public class ComboController : MonoBehaviour
{
    [SerializeField] private WaveController _waveController;
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    [SerializeField] private int _spawner;
    [SerializeField] private AnimationClip _comboClip;

    private int _combo = 0;
    private Animator _animator;

    public void DoCombo()
    {
        _waveController.SpawnSlimes(_combo * _spawner * 2, _spawner);
    }

    public void SetCombo(int combo)
    {
        _text.text = $"x{combo} combo";
        _animator.SetTrigger("DoCombo");
        _combo = combo;
        Invoke("DoCombo", _comboClip.length);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
