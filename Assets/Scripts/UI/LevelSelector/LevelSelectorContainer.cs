using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorContainer : MonoBehaviour
{
    [SerializeField] private LevelSelector _levelSelectorTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private LevelSelectorData[] _selectorsData;

    private const int MaxSize = 3;

    private List<LevelSelector> _selectors;

    private void Awake()
    {
        _selectors = new List<LevelSelector>(MaxSize);
        CreateTemplates();
    }

    private void OnEnable()
    {
        for (int i = 0; i < MaxSize; i++)
            _selectors[i].Clicked += OnClicked;
    }

    private void OnDisable()
    {
        for (int i = 0; i < MaxSize; i++)
            _selectors[i].Clicked -= OnClicked;
    }

    private void CreateTemplates()
    {
        for (int i = 0; i < MaxSize; i++)
            CreateTemplate(_levelSelectorTemplate, _selectorsData[i]);
    }

    private void CreateTemplate(LevelSelector levelSelectorTemplate, LevelSelectorData data)
    {
        LevelSelector levelSelector = Instantiate(levelSelectorTemplate, _container);
        levelSelector.Initialize(data);
        _selectors.Add(levelSelector);
    }

    private void OnClicked()
    {
        for (int i = 0; i < MaxSize; i++)
            _selectors[i].BackgroundToggle(false);
    }
}