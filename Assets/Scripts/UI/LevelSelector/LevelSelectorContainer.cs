using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorContainer : MonoBehaviour
{
    [SerializeField] private LevelSelector _levelSelectorTemplate;
    [SerializeField] private Transform _container;

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
            CreateTemplate(_levelSelectorTemplate);
    }

    private void CreateTemplate(LevelSelector levelSelectorTemplate)
    {
        LevelSelector levelSelector = Instantiate(levelSelectorTemplate, _container);
        _selectors.Add(levelSelector);
    }

    private void OnClicked()
    {
        for (int i = 0; i < MaxSize; i++)
            _selectors[i].BackgroundToggle(false);
    }
}