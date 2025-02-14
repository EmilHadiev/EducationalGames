using System;
using UnityEngine;

public class FieldViewContainer : MonoBehaviour
{
    [SerializeField] private Transform _rawContainer1;
    [SerializeField] private Transform _rawContainer2;
    [SerializeField] private FieldView _fieldViewTemplate;

    private const int MaxFields = 10;

    private void Start()
    {
        CreateTemplates();
    }

    private void CreateTemplates()
    {
        for (int i = 0; i < MaxFields / 2; i++)
            CreateTemplate(_fieldViewTemplate, _rawContainer1, i + 1);

        for (int i = MaxFields / 2; i < MaxFields; i++)
            CreateTemplate(_fieldViewTemplate, _rawContainer2, i + 1);
    }

    private void CreateTemplate(FieldView fieldViewTemplate, Transform container, int index)
    {
        FieldView template = Instantiate(fieldViewTemplate, container);
        template.Initialize(index.ToString(), null);
    }
}
