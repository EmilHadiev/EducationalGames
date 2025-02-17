using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class GamePanelMediator : MonoBehaviour, IStateSwitcher, ILevelSelectorState
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private FieldGameMediator _fieldCanvas;
    [SerializeField] private FlashCardMediator _flashCardMediator;

    private readonly Dictionary<Type, ILevelSelectorState> _states = new Dictionary<Type, ILevelSelectorState>();

    private ILevelSelectorState _currentState;

    private void OnValidate()
    {
        _canvas ??= GetComponent<Canvas>();
        _playButton ??= GetComponentInChildren<Button>();
    }

    private void Awake()
    {
        _states.Add(typeof(GamePanelMediator), this);
        _states.Add(typeof(FieldGameMediator), _fieldCanvas);
        _states.Add(typeof(FlashCardMediator), _flashCardMediator);
    }

    private void Start()
    {
        Switch<GamePanelMediator>();
        OnClicked();
    }

    private void OnEnable() => _playButton.onClick.AddListener(OnClicked);

    private void OnDisable() => _playButton.onClick.RemoveListener(OnClicked);

    public void Switch<T>() where T : ILevelSelectorState
    {
        _currentState?.Exit();
        ILevelSelectorState state = _states[typeof(T)];
        _currentState = state;
    }

    public void Enter() => _canvas.enabled = true;

    public void Exit() { }

    private void OnClicked()
    {        
        _canvas.enabled = false;
        _currentState.Enter();
    }
}