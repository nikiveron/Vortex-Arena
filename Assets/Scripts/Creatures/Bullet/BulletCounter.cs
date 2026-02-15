using UnityEngine;

public class BulletCounter : MonoBehaviour
{
    private int _count = 0;
    public int Count => _count;

    private void Awake()
    {
        ResetValue();
    }

    public void ResetValue()
    {
        UpdateValue(0);
    }

    public void AddValue(int points)
    {
        int newScore = _count + points;
        UpdateValue(newScore);
    }

    private void UpdateValue(int score)
    {
        NormalizeAndSet(score);
    }

    private void NormalizeAndSet(int score)
    {
        _count = Mathf.Clamp(score, 0, int.MaxValue);
    }
}