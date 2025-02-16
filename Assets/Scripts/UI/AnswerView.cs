using System;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AnswerView : MonoBehaviour, IPointerClickHandler
{
    protected AnswerOption Data;

    public event Action<AnswerStatus> Clicked;

    public void SetData(AnswerOption data)
    {
        Data = data;
        PrintData();
    }

    public void OnPointerClick(PointerEventData eventData) => Clicked.Invoke(Data.IsCorrect);

    protected abstract void PrintData();
}