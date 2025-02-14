using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public abstract class AnswerView : MonoBehaviour, IPointerClickHandler
{
    protected AnswerOption Data;
    protected IScore _score;

    public event Action<bool> Clicked;

    public void SetData(AnswerOption data)
    {
        Data = data;
        PrintData();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(Data.IsCorrect);
        Clicked?.Invoke(Data.IsCorrect);
    }

    protected abstract void PrintData();

    [Inject]
    private void Constructor(IScore score) => _score = score;
}