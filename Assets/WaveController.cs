using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] private DiceRoller[] _dice;
    [SerializeField] private SlimeSpawner[] _spawners;
    [SerializeField] private WaveDiceRoller _waveDicerRoller;
    [SerializeField] private int[] _sideToSlimeType;
    [SerializeField] private ComboController[] _comboControllers;
    [SerializeField] private int _minCombo = 2;

    private int[] _sidesChosen = new int[6];
    public bool _pause = true;

    public void SetSideChosen(int diceNumber, int side)
    {
        _sidesChosen[diceNumber] = side;
    }

    public void RerollAllDice()
    {
        foreach (var dice in _dice)
        {
            dice.ReRoll();
        }
    }

    public void SpawnSlimes(int amount, int location)
    {
        _spawners[location].SpawnSlimes(amount, _sideToSlimeType[location]);
    }

    private void Start()
    {
        ResetSidesChosen();
    }

    private void Update()
    {
        if (_pause) return;

        if (IsAllSidesChosen())
        {
            for (int i = 0; i < 3; i++)
            {
                if (!_dice[i]._isActivated) continue;

                _spawners[_sidesChosen[i + 3] - 1].SpawnSlimes(_sidesChosen[i], _sideToSlimeType[_sidesChosen[i] - 1]);
            }

            CheckForCombos();
            ResetSidesChosen();
            _waveDicerRoller.ReRoll();
        }
    }

    private bool IsAllSidesChosen()
    {
        for (int i = 0; i < _sidesChosen.Length; i++)
        {
            if (!_dice[i]._isActivated) continue;

            if (_sidesChosen[i] == -1)
            {
                return false;
            }
        }

        return true;
    }

    private void ResetSidesChosen()
    {
        for (int i = 0; i < _sidesChosen.Length; i++)
        {
            _sidesChosen[i] = -1;
        }
    }

    private void CheckForCombos()
    {
        var combos = new Dictionary<int, int>
        {
            { 0, 0 },
            { 1, 0 },
            { 2, 0 },
            { 3, 0 },
            { 4, 0 },
            { 5, 0 },
        };

        for (int i = 0; i < _sidesChosen.Length; i++)
        {
            if (!_dice[i]._isActivated) continue;

            combos[_sidesChosen[i] - 1] += 1;
        }

        foreach (var combo in combos)
        {
            if (combo.Value >= _minCombo)
            {
                _comboControllers[combo.Key].SetCombo(combo.Value);
            }
        }
    }
}
