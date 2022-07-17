using UnityEngine;
using UnityEngine.Events;

public class DiceCounterController : MonoBehaviour
{
    [SerializeField] private DiceCounter[] _counters;

    [SerializeField] private int _count = 0;
    [SerializeField] private UnityEvent _onZeroEvent;
    private int _lastCount = 0;
    private bool _stopCounting = false;

    public void ChangeCount(int amount)
    {
        _count += amount;

        if (_count <= 0 && !_stopCounting)
        {
            _stopCounting = true;
            _onZeroEvent.Invoke();
        }
    }

    private void Update()
    {
        if (_lastCount != _count)
        {
            SetCount();
        }

        _lastCount = _count;
    }

    private void SetCount()
    {
        int _numberOfCounters = _counters.Length;
        int divider = (int)Mathf.Pow(10, _numberOfCounters - 1);
        int tempCount = _count;

        if (tempCount > (int)Mathf.Pow(10, _numberOfCounters) - 1)
        {
            tempCount = (int)Mathf.Pow(10, _numberOfCounters) - 1;
        }
        else if (tempCount < 0)
        {
            tempCount = 0;
        }

        for (int i = _numberOfCounters - 1; i >= 0; i--)
        {
            var counter = _counters[i];
            counter.SetTargetRotation(tempCount / divider);

            tempCount %= divider;
            divider /= 10;
        }
    }
}
