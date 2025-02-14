using TMPro;
using UnityEngine;
using Zenject;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    private IScore _score;

    private void OnEnable() => _score.Changed += OnScoreChanged;

    private void OnDisable() => _score.Changed -= OnScoreChanged;

    [Inject]
    private void Constructor(IScore score)
    {
        _score = score;
    }

    private void OnScoreChanged(int point) => _scoreText.text = point.ToString();
}