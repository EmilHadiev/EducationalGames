using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CanvasGroup))]
public class FieldAnswerContainer : MonoBehaviour
{
    [SerializeField] private QuestionView _questionView;
    [SerializeField] private AnswerView[] _answerViews;

    private CanvasGroup _group;
    private FieldData _data;

    private Tween _tweenHide;
    private Tween _tweenShow;

    private IScore _score;

    public event Action<FieldData, AnswerStatus> Selected;

    private bool _isWork;

    private void Awake() => _group = GetComponent<CanvasGroup>();

    private void OnEnable()
    {
        foreach (var answer in _answerViews)
            answer.Clicked += OnClicked;
    }

    private void OnDisable()
    {
        foreach (var answer in _answerViews)
            answer.Clicked -= OnClicked;
    }

    private void Start()
    {
        _tweenHide = _group.DOFade(0, Constants.DefaultDuration)
            .SetEase(Ease.Linear)
            .OnStart(HideStarted)
            .OnComplete(HideCompleted)
            .SetAutoKill(false);
        _tweenHide.Pause();

        _tweenShow = _group.DOFade(1, Constants.DefaultDuration)
            .SetEase(Ease.Linear)
            .OnComplete(ShowCompleted)
            .SetAutoKill(false);
        _tweenShow.Pause();
    }

    [Inject]
    private void Constructor(IScore score) => _score = score;

    public void Show() => _tweenShow.Restart();

    public void Hide() => _tweenHide.Restart();

    private void GroupRaycastToggle(bool isOn) => _group.blocksRaycasts = isOn;

    public void SetData(FieldData data)
    {
        ShuffleAnswers();

        _data = data;

        _questionView.SetDescription(_data.Description);

        int index = 0;
        foreach (var answer in _data.Answers)
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

    private void OnClicked(AnswerStatus answerStatus)
    {
        if (_isWork == false)
            return;

        if (answerStatus == AnswerStatus.Correct)
            _score.Add();

        _questionView.ShowResult(answerStatus);

        HideStarted();
        Selected?.Invoke(_data, answerStatus);
    }

    private void HideStarted()
    {        
        GroupRaycastToggle(false);
        WorkToggle(false);
    }

    private void HideCompleted() => _questionView.ResetResultView();

    private void ShowCompleted()
    {
        GroupRaycastToggle(true);
        WorkToggle(true);
    }

    private void WorkToggle(bool isWork) => _isWork = isWork;
}