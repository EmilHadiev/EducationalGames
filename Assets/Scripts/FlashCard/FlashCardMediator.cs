using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class FlashCardMediator : MonoBehaviour, ILevelSelectorState
{
    [SerializeField] private Canvas _canvas;

    private void OnValidate()
    {
        _canvas ??= GetComponent<Canvas>();
    }

    public void Enter() => _canvas.enabled = true;

    public void Exit() => _canvas.enabled = false;
}