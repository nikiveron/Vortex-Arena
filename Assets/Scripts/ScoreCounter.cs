using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private UnityEvent<int> _onStateChanged;
    private static int _score = 0;
    public static int Score => _score;

    private void OnEnable()
    {
        EnemyEvents.OnEnemyKilled += AddScore;
    }

    private void Awake()
    {
        ResetScore();
    }

    public void ResetScore()
    {
        UpdateAndDisplay(0);
    }

    public void AddScore(int points)
    {
        int newScore = _score + points;
        UpdateAndDisplay(newScore);
    }

    private void UpdateAndDisplay(int score)
    {
        NormalizeAndSet(score);
        _onStateChanged.Invoke(score);
    }

    private void NormalizeAndSet(int score)
    {
        _score = Mathf.Clamp(score, 0, int.MaxValue);
    }

    private void OnDisable()
    {
        EnemyEvents.OnEnemyKilled -= AddScore;
    }
}
