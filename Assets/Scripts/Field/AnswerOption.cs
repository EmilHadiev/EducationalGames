using System;
using UnityEngine;

[Serializable]
public class AnswerOption
{
    [field: SerializeField] public string Answer { get; private set; }
    [field: SerializeField] public bool IsCorrect { get; private set; }
}