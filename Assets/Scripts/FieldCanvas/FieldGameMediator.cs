using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class FieldGameMediator : MonoBehaviour, ILevelSelectorState
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TMP_Text _topicText;
    [SerializeField] private FieldViewContainer _fieldViewContainer;
    [SerializeField] private FieldAnswerContainer _answerContainer;

    private IScore _score;

    private void OnValidate() => _canvas ??= GetComponent<Canvas>();

    private void Awake()
    {
        _topicText.text = Constants.FieldGameTopicName;
        _fieldViewContainer.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _fieldViewContainer.Selected += OnFieldSelected;
        _answerContainer.Selected += OnAnswerSelected;
    }

    private void OnDisable()
    {
        _fieldViewContainer.Selected -= OnFieldSelected;
        _answerContainer.Selected -= OnAnswerSelected;
    }

    [Inject]
    private void Constructor(IScore scoreContainer) => _score = scoreContainer;

    public void Enter()
    {
        _canvas.enabled = true;
        RestartGame();
    }

    public void Exit() => _canvas.enabled = false;

    private void OnFieldSelected(FieldData data)
    {
        _fieldViewContainer.Hide();

        _answerContainer.Show();
        _answerContainer.SetData(data);
    }

    private void OnAnswerSelected(FieldData data, AnswerStatus answerStatus)
    {
        _answerContainer.Hide();
        _fieldViewContainer.Show();
        _fieldViewContainer.ShowAnswerStatus(data, answerStatus);
    }

    private void RestartGame()
    {
        _score.Reset();
        _fieldViewContainer.Restart();
    }
}