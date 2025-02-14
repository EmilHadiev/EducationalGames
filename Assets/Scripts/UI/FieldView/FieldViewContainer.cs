using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FieldViewContainer : MonoBehaviour
{
    [SerializeField] private FieldQuestionAnswerContainer _questionAnswerContainer;
    [SerializeField] private FieldView _fieldViewTemplate;
    [SerializeField] private RawContainer[] _containers;
    [SerializeField] private List<FieldData> _data;

    private const int MaxFields = 10;

    private List<FieldView> _fieldViews = new List<FieldView>(MaxFields);

    public event Action<FieldData> Selected;

    private void Awake()
    {
        CreateTemplates();
        ShuffleData();
        SetData();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _fieldViews.Count; i++)
            _fieldViews[i].Clicked += OnClicked;

        _questionAnswerContainer.Selected += OnSelected;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _fieldViews.Count; i++)
            _fieldViews[i].Clicked -= OnClicked;

        _questionAnswerContainer.Selected -= OnSelected;
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

    private void OnClicked(FieldData fieldData)
    {
        //StopOtherElements();
        Selected?.Invoke(fieldData);
    }

    private void StopOtherElements()
    {
        for (int i = 0; i < _fieldViews.Count; i++)
            _fieldViews[i].StopWork();
    }

    private void StartOtherElements()
    {
        for (int i = 0; i < _fieldViews.Count; i++)
            _fieldViews[i].StartWork();
    }

    private void OnSelected()
    {
        StartOtherElements();
    }
}