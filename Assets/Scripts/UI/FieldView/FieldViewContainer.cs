using System.Collections.Generic;
using UnityEngine;

public class FieldViewContainer : MonoBehaviour
{
    [SerializeField] private FieldView _fieldViewTemplate;
    [SerializeField] private RawContainer[] _containers;
    [SerializeField] private List<FieldData> _data;

    private const int MaxFields = 10;

    private List<FieldView> _fieldViews = new List<FieldView>(MaxFields);

    private void Start()
    {
        CreateTemplates();
        SetData();
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
        var template = container.PutElement(fieldViewTemplate);
        _fieldViews.Add(template);
    }

    private void SetData()
    {
        for (int i = 0; i < _fieldViews.Count; i++)
            _fieldViews[i].Initialize((i + 1).ToString(), _data[i]);
    }
}