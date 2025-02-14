using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FieldViewContainer : MonoBehaviour
{
    [SerializeField] private FieldView _fieldViewTemplate;
    [SerializeField] private RawContainer[] _containers;
    [SerializeField] private List<FieldData> _data;

    private const int MaxFields = 10;

    private IScore _score;

    private List<FieldView> _fieldViews = new List<FieldView>(MaxFields);

    public event Action<FieldData> Selected;

    private void Awake()
    {
        CreateTemplates();
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

    [Inject]
    private void Constructor(IScore score)
    {
        _score = score;
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

    private void OnClicked(FieldData fieldData)
    {
        StopOtherElements();
        _score.Add(Constants.Point);
        Selected?.Invoke(fieldData);
    }

    private void StopOtherElements()
    {
        for (int i = 0; i < _fieldViews.Count; i++)
            _fieldViews[i].StopWork();
    }
}