using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CanvasGroup))]
public class FieldQuestionAnswerContainer : MonoBehaviour
{
    [SerializeField] private QuestionView _questionView;
    [SerializeField] private AnswerView[] _answerViews;
    [SerializeField] private FieldViewContainer _container;

    private CanvasGroup _containerGroup;
    private CanvasGroup _currentGroup;

    private Sequence _selectedSequence;
    private Sequence _repliedSequence;

    private IScore _score;

    public event Action Selected;

    private void OnEnable()
    {
        _container.Selected += OnSelected;

        foreach (var answer in _answerViews)
            answer.Clicked += OnClicked;
    }

    private void OnDisable()
    {
        _container.Selected -= OnSelected;

        foreach (var answer in _answerViews)
            answer.Clicked -= OnClicked;
    }

    private void Start()
    {
        _containerGroup = _container.GetComponent<CanvasGroup>();
        _currentGroup = GetComponent<CanvasGroup>();

        InitializeSelectedSequence();
        InitializeRepliedSequence();
    }

    private void InitializeRepliedSequence()
    {
        _repliedSequence = DOTween.Sequence();
        _repliedSequence.Pause();
        _repliedSequence.Append(_currentGroup.DOFade(0, Constants.DefaultDuration).OnComplete(() => _currentGroup.gameObject.SetActive(false)));
        _repliedSequence.Append(_containerGroup.DOFade(1, Constants.DefaultDuration).OnComplete(() => _container.gameObject.SetActive(true)));
    }

    private void InitializeSelectedSequence()
    {
        _selectedSequence = DOTween.Sequence();
        _selectedSequence.Pause();
        _selectedSequence.Append(_containerGroup.DOFade(0, Constants.DefaultDuration).OnComplete(() => _container.gameObject.SetActive(false)));
        _selectedSequence.Append(_currentGroup.DOFade(1, Constants.DefaultDuration));
    }

    [Inject]
    private void Constructor(IScore score) => _score = score;

    private void OnSelected(FieldData data)
    {
        _currentGroup.blocksRaycasts = true;

        ShuffleAnswers();
        SetData(data);
        EnableView();

        _selectedSequence.Play();
    }

    private void SetData(FieldData data)
    {
        _questionView.SetDescription(data.Description);

        int index = 0;
        foreach (var answer in data.Answers)
        {
            _answerViews[index].SetData(answer);
            index++;
        }
    }

    private void ShuffleAnswers()
    {
        Shuffler shuffler = new Shuffler();
        _answerViews = shuffler.Shuffle(_answerViews).ToArray();
    }

    private void EnableView()
    {
        _questionView.gameObject.SetActive(true);

        foreach (var answer in _answerViews)
            answer.gameObject.SetActive(true);
    }

    private void OnClicked(bool isCorrectAnswer)
    {
        if (isCorrectAnswer)
            _score.Add(Constants.Point);

        _currentGroup.blocksRaycasts = false;
        _repliedSequence.Play();
        Selected?.Invoke();
    }
}