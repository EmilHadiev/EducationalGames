using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clicker : MonoBehaviour, IPointerClickHandler
{
    public Action Clicked;

    public void OnPointerClick(PointerEventData eventData) => Clicked?.Invoke();
}