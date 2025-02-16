using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FieldViewContainer : MonoBehaviour
{
    [SerializeField] private FieldView _fieldViewTemplate;
    [SerializeField] private RawContainer[] _containers;
    [SerializeField] private List<FieldData> _data;

    private CanvasGroup _group;
    private Tween _tweenShow;
    private Tween _tweenHide;

    private const int MaxFields = 10;

    private List<FieldView> _fieldViews = new List<FieldView>(MaxFields);

    public event Action<FieldData> Selected;

    private void Awake()
    {
        _group = GetComponent<CanvasGroup>();

        CreateTemplates();
        ShuffleData();
        SetData();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _fieldViews.Count; i++)
            _fieldViews[i].Clicked += OnClicked;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _fieldViews.Count; i++)
            _fieldViews[i].Clicked -= OnClicked;
    }

    private void Start()
    {
        _tweenHide = _group.DOFade(0, Constants.DefaultDuration)
            .SetEase(Ease.Linear)
            .OnStart(HideStarted)
            .SetAutoKill(false);
        _tweenHide.Pause();

        _tweenShow = _group.DOFade(1, Constants.DefaultDuration)
            .SetEase(Ease.Linear)
            .OnComplete(ShowCompleted)
            .SetAutoKill(false);
        _tweenShow.Pause();
    }

    public void Show() => _tweenShow.Restart();

    public void Hide() => _tweenHide.Restart();

    private void GroupRaycastToggle(bool isOn)
    {
        _group.blocksRaycasts = isOn;     
    }

    private void CreateTemplates()
    {
        for (int i = 0; i < MaxFields / 2; i++)
            CreateTemplate(_fieldViewTemplate, _containers[0]);

        for (int i = MaxFields / 2; i < MaxFields; i++)
            CreateTemplate(_fieldViewTemplate, _containers[1]);
    }

    private void CreateTemplate(FieldView fieldViewTemplate, RawContainer container)
    {
        var template = Instantiate(fieldViewTemplate, container.transform);
        _fieldViews.Add(template);
    }

    private void SetData()
    {
        for (int i = 0; i < _fieldViews.Count; i++)
            _fieldViews[i].Initialize((i + 1).ToString(), _data[i]);
    }

    private void ShuffleData()
    {
        Shuffler shuffler = new Shuffler();
        _data = shuffler.Shuffle(_data).ToList();
    }

    private void FieldToggle(bool isOn)
    {
        for (int i = 0; i < _fieldViews.Count; i++)
            _fieldViews[i].WorkToggle(isOn);
    }

    private void OnClicked(FieldData fieldData)
    {
        FieldToggle(false);
        GroupRaycastToggle(false);
        Selected?.Invoke(fieldData);
    }

    private void ShowCompleted()
    {
        FieldToggle(true);
        GroupRaycastToggle(true);
    }

    private void HideStarted()
    {
        FieldToggle(false);
        GroupRaycastToggle(false);
    }

    public void ShowAnswerStatus(FieldData data, bool isCorrect)
    {
        for (int i = 0; i < _fieldViews.Count; i++)
            if (_fieldViews[i].TrySetAnswerStatus(data, isCorrect))
                return;
    }
}