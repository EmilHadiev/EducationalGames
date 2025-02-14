using System;
using UnityEngine;

[Serializable]
public struct AnswerOption
{
    [field: SerializeField] public string Answer { get; private set; }
    [field: SerializeField] public bool IsCorrect { get; private set; }
}